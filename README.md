# HATEOAS for ASP.NET Core MVC
This adds simple HATEOAS with JSON support for ASP.NET Core MVC applications.

## Getting started
The JSON HATEOAS provider extends the `IMvcBuilder` and is added like the following:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddMvc()
        .AddHateoas(...);
    
    //other services
}
```
Making a request to one of the APIs in your application with `Accept` header set as `application/json+hateoas`, will return the HATEOAS payload containing the expected resource(s).

## Example
Given the following DTO in your application:
```csharp
public class PersonDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

With the following Controller:
```csharp
[Route("api/[controller]")]
public class PeopleController : Controller
{
    [HttpGet(Name = "get-people")]
    public IActionResult Get() {...}
    
    [HttpGet("{id}", Name = "get-person")]
    public IActionResult Get(int id) {...}
    
    [HttpPost(Name = "create-person")]
    public IActionResult Post([FromBody]PersonDto person) {...}

    [HttpPut("{id}", Name = "update-person")]
    public IActionResult Put(int id, [FromBody]PersonDto person) {...}
    
    [HttpDelete("{id}", Name = "delete-person")]
    public IActionResult Delete(int id) {...}
}

Wire up HATEOAS with the particular links:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddMvc()
        .AddHateoas(options =>
        {
            options
               .AddLink<PersonDto>("get-person", p => new { id = p.Id })
               .AddLink<List<PersonDto>>("create-person")
               .AddLink<PersonDto>("update-person", p => new { id = p.Id })
               .AddLink<PersonDto>("delete-person", p => new { id = p.Id });
        });
    
    //other services
}
```

And executing this request:
```http
GET /api/people HTTP/1.1
Accept: application/json+hateoas
```

Will result in the following response:
````json
{
    "_links": [
        {
            "href": "http://localhost:52691/api/people",
            "rel": "create-person",
            "method": "POST"
        }
    ],
    "items": [
        {
            "_links": [
                {
                    "href": "http://localhost:52691/api/people/1",
                    "rel": "get-person",
                    "method": "GET"
                },
                {
                    "href": "http://localhost:52691/api/people/1",
                    "rel": "update-person",
                    "method": "PUT"
                },
                {
                    "href": "http://localhost:52691/api/people/1",
                    "rel": "delete-person",
                    "method": "DELETE"
                }
            ],
            "data": {
                "id": 1,
                "name": "Fanie",
                "email": "fanie@mail.com"
            }
        },
        {
            "_links": [
                {
                    "href": "http://localhost:52691/api/people/2",
                    "rel": "get-person",
                    "method": "GET"
                },
                {
                    "href": "http://localhost:52691/api/people/2",
                    "rel": "update-person",
                    "method": "PUT"
                },
                {
                    "href": "http://localhost:52691/api/people/2",
                    "rel": "delete-person",
                    "method": "DELETE"
                }
            ],
            "data": {
                "id": 2,
                "name": "Maarten",
                "email": "maarten@example.com"
            }
        },
        {
            "_links": [
                {
                    "href": "http://localhost:52691/api/people/2",
                    "rel": "get-person",
                    "method": "GET"
                },
                {
                    "href": "http://localhost:52691/api/people/2",
                    "rel": "update-person",
                    "method": "PUT"
                },
                {
                    "href": "http://localhost:52691/api/people/2",
                    "rel": "delete-person",
                    "method": "DELETE"
                }
            ],
            "data": {
                "id": 2,
                "name": "Marcel",
                "email": "marcel@example.com"
            }
        }
    ]
}

```


