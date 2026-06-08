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
#### Once the application is running you can access the Swagger UI in your browser to test the endpoints

- Swagger UI : `http://localhost:5136/swagger/index.html`



 

 

## Sample Response
The base URL for all endpoints is `http://localhost:5136`.
 
## Health Controller
##  **`GET` `/api/Health`** :
### Example Request ( Curl )
####
 ```bash
curl -X 'GET' \
'http://localhost:5136/api/Health' \
-H 'accept: */*'
```

### Example Response
####
 ```json
{
"status": "running",
"application": "trainee_management_api",
"timestamp": "2026-06-08T11:17:41.986949Z"
}
```
 

## Trainee Controller
##  **`GET` `/api/Trainee`** :
### Example Request ( Curl )
####
 ```bash
 curl -X 'GET' \
 'http://localhost:5136/api/Trainee' \
 -H 'accept: */*'
```

 
### Example Response
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


 
##  **`GET` `/api/Trainee?={searchTerm}`** :
### Example Request ( Curl )
####
 ```bash
curl -X 'GET' \
  'http://localhost:5136/api/Trainee?searchTerm=react' \
  -H 'accept: */*'
```

 
### Example Response
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

##  **`GET` `/api/Trainee/{id}`** :
### Example Request ( Curl )
####
 ```bash
  curl -X 'GET' \
  'http://localhost:5136/api/Trainee/1' \
  -H 'accept: */*'
```

 
### Example Response
####
 ```json
{
  "trainee": {
    "id": 1,
    "firstName": "Rakesh",
    "lastName": "Sharma",
    "email": "rakesh@gmail.com",
    "techStack": "Nextjs, Angular",
    "status": "active",
    "createdDate": "2026-06-08T00:00:00+05:30"
  },
  "message": "Trainee with id 1 returned sucessfully"
}
````

##  **`POST` `/api/Trainee`** :
### Example Request ( Curl )
####
 ```bash
curl -X 'POST' \
  'http://localhost:5136/api/Trainee' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "firstName": "Rakesh",
  "lastName": "Sharma",
  "email": "rakesh@gmail.com",
  "techStack": "Nextjs, Angular",
  "status": "active"
}'
```

 
### Example Response
####
 ```json
{
  "message": "Trainee Added to Database"
}
````

##  **`PUT` `/api/Trainee/{id}`** :
### Example Request ( Curl )
####
 ```bash
curl -X 'PUT' \
  'http://localhost:5136/api/Trainee/1' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "firstName": "Raj",
  "lastName": "Mhatre",
  "email": "rajmhatre@gmail.com",
  "techStack": "Python, Flask, Django",
  "status": "active"
}'
```

 
### Example Response
####
 ```json
{
  "message": "Trainee Updated Sucessfully"
}
````


##  **`DELETE` `/api/Trainee/{id}`** :
### Example Request ( Curl )
####
 ```bash
curl -X 'DELETE' \
  'http://localhost:5136/api/Trainee/1' \
  -H 'accept: */*'
```

 
### Example Response
####
 ```json
{
  "message": "Trainee Updated Sucessfully"
}
````