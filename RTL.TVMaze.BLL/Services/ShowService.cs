using Newtonsoft.Json;
using RTL.TVMaze.BLL.Models;
using RTL.TVMaze.BLL.Repositories;
using RTL.TVMaze.Generic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RTL.TVMaze.BLL.Services
{
    public class ShowService
    {
        private IShowRepository ShowRepository;
        private ICastCreditRepository CastCreditRepository;
        private IPersonRepository PersonRepository;
        private IDatabaseRepository DatabaseRepository;
        private HttpClient HttpClient;

        public ShowService(IShowRepository showRepository,
            IHttpClientFactory clientFactory,
            ICastCreditRepository castCreditRepository,
            IPersonRepository personRepository,
            IDatabaseRepository databaseRepository)
        {
            ShowRepository = showRepository;
            HttpClient = clientFactory.CreateClient();
            DatabaseRepository = databaseRepository;
            CastCreditRepository = castCreditRepository;
            PersonRepository = personRepository;
        }

        public async Task<List<ShowDto>> GetShows(int skip, int top)
        {
            await SyncShowData();

            var shows = ShowRepository.GetShows(skip, top).ToList();

            var resultList = new List<ShowDto>();
            foreach (var show in shows)
            {
                if (show.Cast == null || !show.Cast.Any())
                {
                    show.Cast = await SyncShowCast(show.Id);
                }

                resultList.Add(new ShowDto(show));
            }

            // saving affected rows in the foreach above, we do this here to avoid multi threading issues with dbContext
            var affectedRows = await DatabaseRepository.SaveChanges();

            return resultList;
        }

        // Syncs the database with the cast of a show, also returns the cast so it can be used right away
        public async Task<IEnumerable<CastCredit>> SyncShowCast(int showId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://api.tvmaze.com/shows/" + showId + "/cast");
            request.Headers.Add("Accept", "application/json");

            var response = await HttpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var castDtoList = JsonConvert.DeserializeObject<IEnumerable<CastCreditDto>>(json);

                var castList = new List<CastCredit>();

                foreach (var castCreditDto in castDtoList)
                {
                    if (castCreditDto.Person == null) { continue; } // skip if there is no person

                    var castCredit = new CastCredit
                    {
                        ShowId = showId,
                        PersonId = castCreditDto.Person.Id,
                        Person = new Person
                        {
                            Id = castCreditDto.Person.Id,
                            Birthday = castCreditDto.Person.Birthday,
                            Name = castCreditDto.Person.Name,
                        }
                    };

                    // adding this as a new instance to avoid tracking by entity framework
                    await CastCreditRepository.AddCastCredit(
                        new CastCredit
                        {
                            ShowId = showId,
                            PersonId = castCreditDto.Person.Id,
                        });

                    var personExists = await PersonRepository.PersonExists(castCredit.PersonId);
                    if (!personExists)
                    {
                        await PersonRepository.AddPerson(castCredit.Person);
                    }
                    castList.Add(castCredit);
                }

                return castList;
            }

            // something went wrong at this point, returning empty list
            return new List<CastCredit>();
        }

        public async Task<bool> SyncShowData()
        {
            // we get the highest id in our db to calculate a starting point
            var highestShowId = await ShowRepository.GetHighestShowId();
            var done = false;
            double pageNumber = (double)(highestShowId) / 250;
            // note, the documentation of the TVMaze Api tells us to floor highestShowId) / 250
            // but this will give us the last page number we requested, what we want is the next page number to request so we use ceiling
            int pageNum = highestShowId < 1 ? 0 : (int)(Math.Ceiling(pageNumber));
            while (!done)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "http://api.tvmaze.com/shows?page=" + pageNum);
                request.Headers.Add("Accept", "application/json");

                var response = await HttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var shows = JsonConvert.DeserializeObject<IEnumerable<Show>>(json);
                    await ShowRepository.AddShows(shows);
                }
                else
                {
                    done = true;
                }
                pageNum++;
            }
            var affectedRows = await DatabaseRepository.SaveChanges();
            return affectedRows > 0;
        }
    }
}
