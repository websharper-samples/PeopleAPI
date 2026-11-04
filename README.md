# People API

A small CRUD REST API built with [WebSharper](https://websharper.com) sitelets on
ASP.NET Core. It maintains an in-memory list of people and exposes it over JSON.

This is the back end for the [PeopleClient](../PeopleClient) sample — together
they form a client/server pair, and the client references this project to share
its model (`PersonData`, `EndPoint`, `ApiEndPoint`, …).

> This is a server application (a REST API) and therefore is **not** deployed to
> GitHub Pages. CI builds it on every push.

## Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/people` | List all people |
| GET | `/api/people/{id}` | Get one person |
| POST | `/api/people` | Create a person (JSON body) |
| PUT | `/api/people` | Update a person (JSON body) |
| DELETE | `/api/people/{id}` | Delete a person |

## Running locally

Start the API first:

```
dotnet run --project src
```

It listens on <http://localhost:5585>; try <http://localhost:5585/api/people>.

Then run the [PeopleClient](../PeopleClient) sample, which talks to this API.

CORS origins are configured in `src/appsettings.json` (the client's
`http://localhost:5479` is included).

## Building

```
dotnet build src/PeopleApi.fsproj -c Release
```
