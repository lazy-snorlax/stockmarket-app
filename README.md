# Stock Market Application
This is a stockmarket application for testing features with ASP.Net Core. This project was developed on an Ubuntu OS using Visual Studio Code and Docker containers for local development.

This is from the tutorial series by Teddy Smith on Youtube. Playlist found [here](https://www.youtube.com/playlist?list=PL82C6-O4XrHfrGOCPmKmwTO7M0avXyQKc)

## Front End - React
This service will fire up on http://localhost:8080. This was generated from the Vite init command.

## MS SQL Database setup
This is run in its own Docker container, as SQL Server is not compatible with Ubuntu at the time of this projects creation (I could not get it working natively, so pivoted to using a Docker container with a shared volume).

This service should start on http://localhost:1433. If localhost doesn't work, try the IP address used by the docker container. If there are any issues with data persisting, check the volumes in the docker-compose.yml file. Same for the sa password. The database management tool used was BeeKeeper for Ubuntu.

### Running migrations
To run database migrations, bash into the api container using 
```
docker compose exec api bash
```
and run the following:
```
dotnet ef migrations add <name_of_migration> # creates the migration
dotnet ef database update # executes the migration
```
If errors occur, double check the connection string in appsettings.json, and confirm the database is reachable from a Database IDE (Management Studio, BeeKeeper, etc)