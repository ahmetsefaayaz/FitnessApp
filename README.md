# FitnessApp Backend

A highly scalable and maintainable RESTful API built for fitness, workout, and nutrition tracking. This project demonstrates enterprise-level backend development practices using a **Modular Monolith** architecture.

##  Technologies & Architecture

*   **Framework:** .NET 9.0 (ASP.NET Core)
*   **Architecture:** Clean Architecture & Modular Monolith
*   **Design Patterns:** CQRS (MediatR), Dependency Injection
*   **Routing:** Minimal APIs (Carter) with Nested Routing Standard
*   **Database:** PostgreSQL & Entity Framework Core (Code-First)
*   **Caching:** Redis
*   **Authentication:** JWT (JSON Web Tokens)
*   **Testing:** xUnit & Moq
*   **Containerization:** Docker & Docker Compose

##  Modules

The system is decoupled into isolated modules to ensure independent development and easy future migration to Microservices if needed:

*   **Identity Module:** Manages user registration, login, JWT generation, and role-based access control.
*   **Workout Module:** Handles workout routines, exercises, and fitness tracking logs.
*   **Nutrition Module:** Manages dietary plans, daily caloric intake, and food items.

##  Key Features

*   **Automated Migrations:** Database tables and seed data are automatically generated upon application startup via application builder extensions.
*   **Domain Encapsulation:** Implemented `SoftDelete` and domain-specific business rules directly within Entities.
*   **RESTful Best Practices:** Clean URLs with proper nested routing (e.g., `PUT /api/nutrition/diets/{dietId}/foods/{foodId}`).
*   **Developer Experience (DX):** Fully containerized setup. A single command spins up the API, Database, and Cache.

##  How to Run (Docker)

You can run the entire infrastructure using Docker. No local SDKs or Databases are required.

1. Clone the repository.
2. Navigate to the root directory where `docker-compose.yml` is located.
3. Run the following command:
   ```bash
   docker-compose up -d --build
   
4. Access the Swagger UI at: `http://localhost:5000/swagger`

## Current Status & Roadmap
*   Core Domain (Diet) and critical Application business rules are covered by Unit Tests using xUnit and Moq. 
*   Test coverage expansion is currently in progress.
