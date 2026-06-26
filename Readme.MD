# Trainee Management .NET API

## Tech Stack
- Framework : .NET 10 / ASP.NET Core
- Database : Entity Framework Core In Memory Database
- API Documentation : Swagger
- Language : C#
 

## Backend Setup Steps

1. Clone the repository
    ```sh
   git clone https://github.com/jaydeep-zeus-3704/trainee_management_dotnet.git 
   ```

2. Navigate to the project Directory
    ```sh
   cd trainee_management_dotnet
   ```

3. Install all the packages from .csproj file
    ```sh
   dotnet restore
   ```


4. Run the application
    ```sh
   dotnet watch run
   ```
5. The API will now be running on `http://localhost:5136`   


## MySQL setup

1. Install the community version of MySQL
    ```sh
   https://dev.mysql.com/downloads/installer/ 
   ```

2. Create database trainee_management in MySQL
    ```sh
   create database trainee_management
   ```

3. Configure the connection string in appsettings.json
    ```sh
   "ConnectionStrings":
      {
      "Database":"server=localhost;port=3306;database=trainee_management;User=<your_mysql_username>;Password=<your_mysql_password>;"
      }
   ```

4. Create migration
    ```sh
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## JWT setup

1. Install required packages
    ```sh
    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 10.0.9
    dotnet add package System.IdentityModel.Tokens.Jwt --version 8.19.1
   ```


2. Configure JWT settings in appsettings.json
    ```sh
    "Jwt":{
      "Key":"<your_jwt_secret_key>",
      "Issuer":"TraineeManagementApi",
      "Audience":"TraineeManagementClient"
    }
   ``` 
   








## Usage
 Once the application is running you can access the Swagger UI in your browser to test the endpoints

- Swagger UI : `http://localhost:5136/swagger/index.html`


## API List
- Health
  ```sh
  GET /api/health 
  ```
- Auth
  ```sh
  POST   /api/auth/login 
  ```
- Trainee
  ```sh
  GET    /api/trainees?pageNumber=1&pageSize=10&search=amit&status=Active 
  GET    /api/trainees/{id} 
  POST   /api/trainees/create 
  PUT    /api/trainees/{id}/update
  DELETE /api/trainees/{id}/delete 
  ```
- Mentor
  ```sh
  GET    /api/mentors 
  GET    /api/mentors/{id} 
  POST   /api/mentors/create
  PUT    /api/mentors/{id}/update 
  DELETE /api/mentors/{id}/delete 
  ```
- Learning Tasks
  ```sh
  GET    /api/learning-tasks/all 
  GET    /api/learning-tasks/{id} 
  POST   /api/learning-tasks/create 
  PUT    /api/learning-tasks/{id}/update 
  DELETE /api/learning-tasks/{id}/delete
  ```
- Task Assignments
  ```sh
  POST   /api/task-assignments/create 
  GET    /api/task-assignments 
  GET    /api/task-assignments/{id} 
  PUT    /api/task-assignments/{id}/status 
  ```
- Submissions
  ```sh
  POST   /api/submissions/create 
  GET    /api/submissions 
  GET    /api/submissions/{id} 
  ```
- Reviews
  ```sh
  POST   /api/reviews/create 
  GET    /api/reviews 
  GET    /api/reviews/{id} 
  ```


 

 

## Sample Response
The base URL for all endpoints is `http://localhost:5136`.
 
#### Health Controller
####  **`GET` `/api/Health`** :
Example Request ( Curl )
####
 ```bash
curl -X 'GET' \
'http://localhost:5136/api/Health' \
-H 'accept: */*'
```

Example Response
####
 ```json
{
"status": "running",
"application": "trainee_management_api",
"timestamp": "2026-06-08T11:17:41.986949Z"
}
```
 

#### Trainee Controller
####  **`GET` `/api/trainee`** :
 Example Request ( Curl )
####
 ```bash
 curl -X 'GET' \
 'http://localhost:5136/api/trainee?search='Reactjs'&&status='active' 
 -H 'accept: */*'
```

 
 Example Response
####
 ```json
{
  "traineesList": [
    {
      "id": 1,
      "firstName": "Jaydeep",
      "lastName": "Patil",
      "email": "jaydeeppatil3704@gmail.com",
      "techStack": "Reactjs,Redux,Dotnet,Csharp",
      "status": "active",
      "createdDate": "2026-06-08T00:00:00+05:30"
    },
    {
      "id": 2,
      "firstName": "Pratham",
      "lastName": "Patil",
      "email": "pratham@gmail.com",
      "techStack": "Reactjs",
      "status": "active",
      "createdDate": "2026-06-08T00:00:00+05:30"
    }
  ],
  "message": "Trainee List Fetched Sucessfully!"
}
````
 

####  POST `/api/trainee/create`

📌 Example Request (curl)

```bash
 curl -X 'POST' \
    'http://localhost:5136/api/trainee/create' \
    -H 'accept: */*' \
    -H 'Authorization: Bearer <your-jwt-token>' \
    -H 'Content-Type: application/json' \
    -d '{
      "firstName": "pratham",
      "lastName": "patil",
      "email": "pratham@example.com",
      "techStack": "React",
      "status": "ACTIVE"
    }'
```

 Example Response
####
 ```json
{
  "message": "Trainee Added to Database"
}
````
####  GET `/api/trainee/{id}`

📌 Example Request (curl)

```bash
 curl -X 'GET' \
  'http://localhost:5136/api/trainee/2' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiamF5ZGVlcDM3MDQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJqYXlkZWVwcGF0aWwzNzA0QGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFETUlOIiwiVXNlcklkIjoiMSIsImV4cCI6MTc4MTY4MDQ4MCwiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.uUqY7j1U08ncQMMQsG6iqkfwqu21kWsU02w8EtXhnv0'
```

 Example Response
####
 ```json
{
  "trainee": {
    "id": 2,
    "firstName": "Jane",
    "lastName": "Smith",
    "email": "jane.smith2@example.com",
    "techStack": "Java, Spring",
    "status": "ACTIVE",
    "createdDate": "2026-06-15T13:22:56"
  },
  "message": "Trainee with id 2 returned sucessfully"
}
````
####  DELETE `/api/trainee/{id}/delete`

📌 Example Request (curl)

```bash
 curl -X 'DELETE' \
  'http://localhost:5136/api/trainee/6/delete' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiamF5ZGVlcDM3MDQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJqYXlkZWVwcGF0aWwzNzA0QGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFETUlOIiwiVXNlcklkIjoiMSIsImV4cCI6MTc4MTY4MDQ4MCwiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.uUqY7j1U08ncQMMQsG6iqkfwqu21kWsU02w8EtXhnv0'
```
