# Trainee Management .NET API

## Tech Stack
- Framework : .NET 10 / ASP.NET Core
- Database : Entity Framework Core In Memory Database
- API Documentation : Swagger
- Language : C#

## API Endpoints


|Method|   Endpoint | Description | 
|--------| --------| -------- | 
|`get`| `/api/health`  | Check API Status  | 
|`get`|`/api/Trainee` | Fetch all trainees from db| 
|`get`|`/api/Trainee?={searchQuery}` | Fetch trainees based on searchQuery| 
|`get`|`/api/Trainee/{id}` | Fech a particular trainee by ID | 
|`post`|`/api/Trainee` | Post trainee  | 
|`put`|`/api/Trainee/{id}` | Update trainee  | 
|`delete`|`/api/Trainee/{id}` | Delete trainee  | 

## Getting Started

1. Clone the repository
    ```sh
   https://github.com/jaydeep-zeus-3704/trainee_management_dotnet.git 
   ```

2. Navigate to the project Directory
    ```sh
   cd trainee_management_dotnet
   ```
3. Run the application
    ```sh
   dotnet watch run
   ```
4. The API will now be running on `http://localhost:5136`   

## Usage
#### Once the application is running you can access th eSwagger UI in your browser to test the endpoints

- Swagger UI : `http://localhost:5136/swagger/index.html`



 
## Features
- **CRUD Operations**: Full support for creating, reading, updating, and deleting trainees.
- **Search**: Search for trainees by their first name, last name, email, or technology stack.
- **Health Check**: An endpoint to monitor the API's status.
- **API Documentation**: Integrated Swagger UI for easy API exploration and testing.
- **Validation**: Server-side validation for trainee data.
 

## API Endpoints
The base URL for all endpoints is `http://localhost:5136`.
 
### Health Controller
- **`GET /api/Health`**
    - **Description**: Checks the health and status of the application.
    - **Success Response (200 OK)**:
      ```json
      {
        "status": "running",
        "application": "trainee_management_api",
        "timestamp": "2023-10-27T10:00:00.000Z"
      }
      ```
 
### Trainee Controller
 
- **POST /api/Trainee**
    - **Description**: Creates a new trainee.
    - **Request Body**:
      ```json
      {
        "firstName": "Jane",
        "lastName": "Doe",
        "email": "jane.doe@example.com",
        "techStack": "Angular, Node.js",
        "status": "Active"
      }
      ```
    - **Success Response (201 Created)**:
      ```json
      {
        "message": "Trainee Added to Database"
      }
      ```
 
- **`GET /api/Trainee`**
    - **Description**: Retrieves a list of all trainees. Supports filtering via a search term.
    - **Query Parameters**:
        - `searchTerm` (string): Filters trainees where the term matches first name, last name, email, or tech stack.
    - **Success Response (200 OK)**:
      ```json
      {
        "traineesList": [
          {
            "id": 1,
            "firstName": "John",
            "lastName": "Doe",
            "email": "john.doe@example.com",
            "techStack": ".NET, React",
            "status": "Active",
            "createdDate": "2023-10-27T00:00:00"
          }
        ],
        "message": "Trainee List Fetched Sucessfully!"
      }
      ```
 
- **`GET /api/Trainee/{id}`**
    - **Description**: Retrieves details for a specific trainee by their ID.
    - **Success Response (200 OK)**:
      ```json
      {
        "trainee": {
          "id": 1,
          "firstName": "John",
          "lastName": "Doe",
          "email": "john.doe@example.com",
          "techStack": ".NET, React",
          "status": "Active",
          "createdDate": "2023-10-27T00:00:00"
        },
        "message": "Trainee with id 1 returned sucessfully"
      }
      ```
 
- **`PUT /api/Trainee/{id}`**
    - **Description**: Updates the details of an existing trainee.
    - **Request Body**:
      ```json
      {
        "firstName": "John",
        "lastName": "Smith",
        "email": "john.smith@example.com",
        "techStack": "Vue, Python",
        "status": "Completed"
      }
      ```
    - **Success Response (200 OK)**:
      ```json
      {
        "message": "Trainee Updated Sucessfully"
      }
 