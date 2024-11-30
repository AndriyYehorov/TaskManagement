Setup instructions

1) Without Docker : 

- Download project files, MS SQL, .NET SDK

- Replace connection string
in `TaskManagement\TaskManagement.API\appsettings.json `
ConnectionString -> "DefaultDb" : "<YourConnectionString>"

- Choose TaskManagement.API as startup project

- Run

2) With Docker:

- Download project files, Docker

- Change connection string
in `TaskManagement\TaskManagement.Infrastructure\DependencyInjection\DependencyInjection.cs`
line 22 from `"DefaultDb"` to `"DockerDb"`

- run `docker compose up --build` in TaskManagement folder

- wait for lauching, app will be available at `http://localhost:8080/swagger`

API documentation

- POST /users/login
  
Authenticates user and returns a JWT.
(Also writes JWT in Cookies)

Request body :
{
  "usernameOrEmail": "string",
  "password": "string"
}

- POST /users/register

Registrates new user.
Password must be at least 6 characters long.
Password must contain at least one special character.

{
  "username": "string",
  "email": "user@example.com",
  "password": "string"
}

- GET /tasks

Returns tasks by filters with sorting and pagination options.

page - page number, default is 1
pageSize - size of page, default is 5
sortOrder - asc or desc, default is asc
sortColumn - duedate or priority, , default is id
MinDueDate - filter by minimun duedate
MaxDueDate - filter by maximum duedate
Priority - filter by priority value
Status - filter by status value

- POST /tasks

Creates new task.

Status can be 0, 1, 2 or Pending, InProgress, Completed.
Priority can be 0, 1, 2 or Low, Medium, High.
Only title is required.

Request body :
{
  "title": "string",
  "description": "string",
  "dueDate": "2024-11-30T22:00:52.818Z",
  "status": 0,
  "priority": 0
}
  
- GET /tasks/{id}

Returns task by ID.

- PUT /tasks/{id}

Updates task by ID.
  
- DELETE /tasks/{id}
  
Deletes task by ID.
