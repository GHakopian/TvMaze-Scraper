# TvMaze Api Demo

This is a demo project for RTL, it Scrapes data from the TVMaze  Api http://www.tvmaze.com/api#show-cast and provides a new Api to view shows with respective cast.

<h2>Getting started</h2>
<p>1 Clone or download the project files</p>
<p>2 Open the project in visual studio and run the database migrations in the Package Manager Console</p>
<pre>update-database</pre>
<p>Make sure that RTL.TVMaze.Dal.EFCore is the target project (default project) when running the migrations</p>
<p>3 Start the Api project (RTL.TVMaze.Api) this will open the swagger UI.</p>

<h2>Project Structure</h2>
<p>This project is D.D.D inspired, it uses repository pattern and layered architecture.</p>
<p>- Data Access Layer (DAL): Provides database access and manipulation by implementing BLL abstractions</p>
<p>- Business logic layer (BLL): Handles business logic and provides abstractions for DAL</p>
<p>- Data layer (Generic): provides generic types like entities to be used by other layers</p>
<p>These layers are then brought together in the Api Web Application project.</p>

