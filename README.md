# TechnicalAssignment API

I have created an ASP .NET Core Web API that can be used manage a Product class. It has all the CRUD operations. The app stores the data in an in memory db. The database is seeded when the application is initialized, 30 products and 1 user is seeded. I have also implemented JWT authentication that is why the user is needed.

#### Extras

I have also implemented exception handling and for the GetProducts method
I have also impelmented paging, filtering and sorting.

#### Runnig the project

* Project runs on .NET version: 8.0

* Repository link
  * git clone <https://github.com/VisarMarku00/technical_assignment.git>
  * cd technical_assignment/TechnicalAssignment

* Start the API
  * dotnet run

* The API will be available at:
  * <http://localhost:5002>
  * <https://localhost:7084>

* Swagger UI:
  * <https://localhost:5002/swagger>
  * <https://localhost:7084/swagger>

#### DB Information

No need to set up db from your side since it is in memory db.
No migrations or connection strings are required.
All data exists only while the app is running.
Every restart resets all products.

#### Exception Handling
The API uses a Global Exception Handling Middleware to catch unexpected errors and return clean JSON error responses.
This middleware only handles problems that should not happen during normal use, such as:

* Missing or unreadable JSON seed files

* Failed deserialization

* Missing JWT configuration

* Any unexpected internal error

If these happen, the middleware returns:

* 400 for invalid arguments

* 404 for missing data

* 500 for any unexpected server error

Normal API behavior (like wrong username or product not found) is handled by the contoller.

## API Documentation

> ### Base route
>
> /api/products
-------------------------------------------------------

> #### Products API
>
> ##### GET /api/products
>
> Retrieve products with filtering, sorting, and paging.

##### Query Parameters

| Name         | Type   | Required | Description                  |
| ------------ | ------ | -------- | ---------------------------- |
| `category`   | string | no       | Filter by category           |
| `minPrice`   | int    | no       | Minimum price                |
| `maxPrice`   | int    | no       | Maximum price                |
| `sortBy`     | string | no       | `name`, `price`, `createdAt` |
| `sortOrder`  | string | no       | `asc` or `desc`              |
| `pageNumber` | int    | no       | Default: 1                   |
| `pageSize`   | int    | no       | Default: 10                  |

##### Example

> GET /api/products?sortBy=price&sortOrder=desc&pageNumber=2&pageSize=5

###### Response Example

```json
[
  {
    "id": 10,
    "name": "Laptop X1",
    "price": 1299,
    "category": "Computers",
    "createdAt": "2024-02-01T13:00:00Z"
  }
]
```

-------------------------------------------------------

> ##### GET /api/products/{id}
>
> Retrieve a single product.
>
> GET /api/products/1
>
> Responses
>
> 200 OK — found
>
> 404 Not Found

-------------------------------------------------------

> ##### POST /api/products
>
> Create a new product.
>
###### Request Body

```json
{
  "name": "Wireless Mouse",
  " price": 25,
  "category": "Accessories"
}
```

> Responses
>
> 201 Created
>
> 400 Bad Request

-------------------------------------------------------

> ##### PUT /api/products/{id}
>
> Update an existing product.
>
###### Example Body

```json
{
  "name": "Updated Mouse",
  "price": 29,
  "category": "Accessories"
}
```

> Responses
>
> 200 OK
>
> 404 Not Found

-------------------------------------------------------

> ##### DELETE /api/products/{id}
>
> Delete a product.
>
> Responses
>
> 204 No Content
>
> 404 Not Found

-------------------------------------------------------

> ##### Auth API
>
> POST /api/auth/login
>
> Login and receive a JWT token.
>
> ###### Request Body
>
> ```json
> {
>   token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9. eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
> }
>
> ```
>
> Responses
>
> 200 OK – Returns the JWT token as a string.
>
> 401 Unauthorized – Invalid username or password.
