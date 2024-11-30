# Task Management API

Task Management API is a project for managing tasks with features like user authentication, task creation, and filtering.

---

## 🚀 Setup Instructions

### Without Docker
1. **Download the project files**.
2. **Install prerequisites:**
   - Microsoft SQL Server
   - .NET SDK
3. **Configure the connection string:**
   - Open `TaskManagement\TaskManagement.API\appsettings.json`.
   - Set `"DefaultDb"` in the `ConnectionString` section.
     ```json
     "ConnectionStrings": {
         "DefaultDb": "<Your Connection String>"
     }
     ```
4. **Set up the startup project:**
   - Choose `TaskManagement.API` as the startup project.
5. **Run the project.**

---

### With Docker
1. **Download the project files**.
2. **Install Docker.**
3. **Configure the connection string:**
   - Open `TaskManagement\TaskManagement.Infrastructure\DependencyInjection\DependencyInjection.cs`.
   - Change the connection string in line 22 from `"DefaultDb"` to `"DockerDb"`.
4. **Build and run the project:**
   - Execute the following command in the `TaskManagement` folder:
     ```bash
     docker compose up --build
     ```
5. **Wait for the application to launch.**
   - The app will be available at: [http://localhost:8080/swagger](http://localhost:8080/swagger).

---

## 📖 API Documentation
#### `POST /users/login`
Authenticates the user and returns a JWT (also sets the JWT in cookies).  

**Request Body:**
```json
{
    "usernameOrEmail": "string",
    "password": "string"
}
```
#### `POST /users/register`
Registrates new user. Password must be at least 6 characters long. Password must contain at least one special character.

**Request Body:**
```json
{
  "username": "string",
  "email": "user@example.com",
  "password": "str!ng"
}
```

#### `GET /tasks`
Returns tasks with filtering, sorting, and pagination options.

Query Parameters:

| Parameter  | Type	 |Default | Description |
| ------------- | ------------- | ------------- | ---------------------------------------------------- |		
|page           |int            |1              |Page number                                           |
|pageSize       |int            |5              |Number of items per page                              |
|sortOrder      |string         |asc            |Sorting order: asc or desc.                           |
|sortColumn     |string         |id             |Column to sort by: duedate, priority.                  |
|MinDueDate     |string         |               |Filter by minimum due date.                            |
|MaxDueDate     |string         |               |Filter by maximum due date.                             |
|Priority       |int            |               |Filter by priority (0=Low, 1=Medium, 2=High).           |
|Status         |int            |               |Filter by status (0=Pending, 1=InProgress, 2=Completed).|


#### `POST /tasks`
Creates new task.

**Request Body:**
```json
{
  "title": "string",
   "description": "string",
   "dueDate": "2024-11-30T22:00:52.818Z",
   "status": 0,
   "priority": 0
}
```

#### `GET /tasks/{id}`

Returns task by ID.

#### `PUT /tasks/{id}`

Updates task by ID.

#### `DELETE /tasks/{id}`

Deletes task by ID.
