# UserQuestions

**Setup**
1. Open project in VS22
2. Run Setup.sql in SSMS. This file has a create database statement as well as other logic. The create database statement should be executed separately first.

**Design**
- This was designed using a clean architecture/onion approach, separating logic by domain and use case. This results in a large number of small class files isolated to mostly single-purpose use.
CQRS patterns are used here to separate reading and writing data to the repository.
The repository pattern is used here to separate business logic from data I/O.
The use of DTO classes was implemented to control which entity fields are abstracted from the database layer and abstracted to the presentation layer.
Dependency injection was implemented to control scope of provided services.

Frameworks used:
- Entity Framework Core
- Mediatr
- FluentAssertions

**Tests**
- A bare minimum of test coverage was implemented due to time constraints. See test items in improvements below.

**Improvements**
- I would have liked to make an API layer in the project to communicate between the console layer and the application layer. This would allow easier connections to other GUIS or presentation layers.
- I prefer to use AutoMapper to translate data structure classes between domain/infrastructure-level classes and presentation-level classes, but I did manual mapping for time purposes.
- For testing I would have liked to implement a project to run integration tests, covering everything from the Application layer downwards. Automated tests could test the console level if needed.
- For the domain model I think it might make sense to look at using the Aggregate Root pattern for the User and Question entities. Ideally a User entity would know about it's own answered questions, and to lockdown direct reads to user questions, and only allow reading a User's questions through the actual instantiated User entity.
