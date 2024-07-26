# Angular User Management Application : SoftwareQDotNetAssignment

This is an .net Core BookService application for performing CRUD operation along wiht jwt authentication and authorization

## Prerequisites

Ensure you have the following installed:
- .NET SDK 8 (includes .NET runtime and CLI)

## Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/KhageshorGiri/SoftwareQDotNetAssignment.git

2. **Navigate into the project directory:**
     ```bash
     cd SoftwareQDotNetAssignment/SoftwareQBookService/src/BookService.API
     
3. **Restore project dependencies:**
   ```bash
   dotnet restore

4. **Build Project**

    ```bash
    dotnet build

5. **Run the project:**

   ```bash
   dotnet run

**Now open : https://localhost:7141/swagger/index.html in you browser.**
**You can alos import collection in postman**


## Process Flow
- [ ] Run the project.
- [ ] Register you account
- [ ] Login through your account and get JWT toke
- [ ] Add token to Authorization header
- [ ] After this you can perform CRUD Operation in BookService

### Endpoints
***Authentication and Authorization***
- [ ] **User Register:** /api/Auth/Register            
- [ ] **User Login:** /api/Auth/Login             

***Book Service***
- [ ] **Post (HttpPost):** /api/post             
- [ ] **Get All Books (HttpGet) :** /api/books                      
- [ ] **GetBookById (HttpGet) :** /api/books/{id}                      
- [ ] **Update Book (HttpPut) :** /api/book/{id}                    
- [ ] **Delete Book (HttpDelete) :** /api/book/{id}           


**Thank You**




