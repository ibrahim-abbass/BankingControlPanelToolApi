# Banking Control Panel Tool API

This repository contains the code for the Banking Control Panel Tool API. This API is designed to provide functionality for managing banking control panel tools.

## Authorization

This application utilizes the Identity Framework for user registration and authentication.

### Identity Framework Features

- User Registration
- User Login
- Password Hashing
- Role-based Authorization

## Roles

Two roles have been added during model creation:

- Admin: Has access to administrative functions.
- User: Regular user role with restricted access.

## Controllers

### Authorization Controller

The Authorization Controller in this API handles user authentication and registration. It provides endpoints for user login and registration.

### Endpoints
- `/Authentication/register`: POST request to register a new user, new user must have new name and email, (name , email) are primary keys.
- `/Authentication/login`: POST request to authenticate a  user and retrun a token.

## Client Controller

The Client Controller in this API handles CRUD operations for managing clients in the banking system.

### Endpoints
- `/Client/clients`: GET Retrieve all clients, users with role admin and user can access it.
- `/Client/filter?filterValue=xxx&filterBy=yyy&pageNumber=1&pageSize=10`: GET Filter clients based on the filter value and filter by, in case filter by is null it will filter on all column, users with role admin and user can access it.
- `/Client/sort?sortBy=id&isAsc=true`: GET Sort clients, but default it will be sort by ascending, users with role admin and user can access it.
- `/Client`: POST add client , jsut user with admin role can create a client.
- `/Client/{id}` PATCH update client based on the client id, jsut user with admin role can create a client.
- `/Client/{id}` Delete delete client based on the client id, jsut user with admin role can create a client.

## History Controller

The History Controller in this API provides a service for retrieving suggestions.

### Endpoint
- `/History/suggestions?top=n`: GET request to retrieve top n suggestions for history, by default n is 3, , jsut user with admin role can create a client.
  
## Getting Started

To get started with this API, follow these steps:

1. Clone this repository.
2. Set up your development environment.
3. Install the necessary dependencies.
4. Run the API.
