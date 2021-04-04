# Portfolio Project #3: Favorites Survey

## Description

Favorites Survey is a voting web application with real-time statistics page, it uses a RabbitMQ queue and a worker service to precompute the stats and caches it via Redis.

>Disclaimer: The purpose of these projects is to showcase my knowledge of technologies, libraries and concepts in a simple application, it is expected to be a small-scaled application, the focus should be on the use cases of said technologies, libraries and concepts.

## Technologies and Libraries

The following technologies/libraries/concepts were used:

Server:

* Language - C#
* Framework - [`ASP.NET 5 (Web API)`](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five)
* CORS
* Database - [`MS SQL`](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
* ORM - [`EntityFramework Core`](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)
* Logging - [`NLog`](https://www.nuget.org/packages/NLog/)
* Real-Time Communication - [`SignalR`](https://www.nuget.org/packages/Microsoft.AspNetCore.SignalR/)
* Validation and Errors - Custom Implementation
* Mapper - Custom Implementation
* CQRS - [`MediatR`](https://www.nuget.org/packages/MediatR/)
* Worker Service - .NET Worker Service
* Caching - [`Redis`](https://redis.io/)
* Queue - [`RabbitMQ`](https://www.rabbitmq.com/)
* Containerization - [`Docker`](https://www.docker.com/)

Client:

* SPA Framework/Library - [`Vue`](https://vuejs.org/) and [`React`](https://reactjs.org/) (Hooks)
* Charts - [`c3.js`](https://c3js.org/)
* HttpClient - [`axios`](https://www.npmjs.com/package/axios)
* UI Library - [`Vuetify`](https://vuetifyjs.com/en/) (Vue) and [`Material-UI`](https://material-ui.com/) (React)
* State Management - [`Vuex`](https://vuex.vuejs.org/) (Vue) and [`Redux`](https://redux.js.org/) (React)
* Real-Time Communication - [`SignalR client`](https://www.npmjs.com/package/@microsoft/signalr)
* Routing - [`vue-router`](https://router.vuejs.org/) and [`react-router`](https://reactrouter.com/)
* Containerization - [`Docker`](https://www.docker.com/)


## Running the Project

You need to install the following:

* [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
* [Node and NPM](https://nodejs.org/en/download/)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

Clone Repo:
```
> git clone https://github.com/itsdaniellucas/favorites-survey

or using GitHub CLI
> gh repo clone itsdaniellucas/favorites-survey
```

Run everything via Docker:

Before anything, you need to launch `Docker Desktop` first, then run the following

```
> cd favorites-survey\src\server-dotnet\FavoritesSurvey
> docker-compose build
> docker-compose up
```

Endpoint:
```
Vue
http://localhost:12000

or React
http://localhost:12001
```

Default Users:
|   Username    |   Password    |   Description                 |
|---------------|---------------|-------------------------------|
|   (anonymous) |   (anonymous) |   Vote and view statistics    |

## License

MIT