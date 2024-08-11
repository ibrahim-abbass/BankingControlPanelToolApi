# Banking Control Panel Tool API

This repository contains the code for the Banking Control Panel Tool API. This API is designed to provide functionality for managing banking control panel tools.

## Authorization Controller
The Authorization Controller in this API handles user authentication and registration. It provides endpoints for user login and registration.
### Endpoints
- `/Authentication/register`: POST request to authenticate a user and generate a token.
- `/Authentication/register`: POST request to register a new user.
 
## Client Controller
The Client Controller in this API handles CRUD operations for managing clients in the banking system.
### Endpoints
- `/Client/clients`: GET Retrieve all clients.
- `/Client/filter?filterValue=xxx&filterBy=yyy&pageNumber=1&pageSize=10`: GET Filter clients based on the filter value and filter by, in case filter by is null it will filter on all column.
- `/Client/sort?sortBy=id&isAsc=true`: GET Sort clients
- `/Client`: POST add client
- `/Client/{id}` PATCH update client based on the client id
- `/Client/{id}` Delete delete client based on the client id

## History Controller
The History Controller in this API provides a service for retrieving suggestions.
### Endpoint
- `/History/suggestions?top=n`: GET request to retrieve top n suggestions for history, by default n is 3.
  
## Getting Started

To get started with this API, follow these steps:

1. Clone this repository.
2. Set up your development environment.
3. Install the necessary dependencies.
4. Run the API.
