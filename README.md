# A simple WebSharper-based REST API

This simple micro-service provides a CRUD interface for a database of people.

To keep things simple, the data is stored in a simple dictionary.

Here is an example HTTP session demonstrating the implemented API:

```
Request:                              | Response:
--------------------------------------+-----------------------------------------
GET /api/person/42                    | 200 OK
                                      |
                                      | { "result": "success",
                                      |   "firstName": "Alan",
                                      |   "lastName": "Turing",
                                      |   "born": "1912-06-23",
                                      |   "died": "1954-06-07" }
--------------------------------------+-----------------------------------------
POST /api/person                      | 200 OK
                                      |
{ "firstName": "Noam",                | { "result": "success",
  "lastName": "Chomsky",              |   "id": 43 }
  "born": "1928-12-07" }              |
--------------------------------------+-----------------------------------------
PUT /api/person/44                    | 404 Not Found
                                      |
{ "firstName": "Bertrand",            | { "result": "error",
  "lastName": "Russell",              |   "message": "User id does not exist" }
  "born": "1872-05-18",               |
  "died": "1970-02-02" }              |
--------------------------------------+-----------------------------------------
DELETE /api/person/43                 | 200 OK
                                      |
                                      | { "result": "success" }
```

## Code walkthrough

The API is implemented in [App.fs](PeopleDb/App.fs).

- The module `Model` contains the types used by our application:
    - `EndPoint` and `ApiEndPoint` together describe the endpoints of the API. They describe the accepted URLs and JSON bodies.
    - `PersonData`, `Id` and `Result` describe the JSON data accepted and returned by the API.

- The module `Backend` contains the back-end of the application:
    - A global dictionary which plays the role of the database.
    - Functions to create, get, edit and delete a person from this dictionary.

- The module `Site` ties everything together. It creates a `MultiPage` site which parses `EndPoint`s, calls the corresponding `Backend` functions, and returns the resulting value as JSON content.
