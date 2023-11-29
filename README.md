# Topic Management Service
Topic Management Service is a part of a microservice-based web application designed to explore and manage topics related to .NET in a hierarchical, tree-structured manner. This service is one amongst other microservices, aiming to provide a comprehensive platform to navigate through .NET topics and subtopics, making it an invaluable resource for learners and developers alike.

## Project Overview
The main functionality of this service revolves around managing topics and subtopics, where each topic can have multiple subtopics, and each subtopic can further have its own subtopics. The service ensures a structured organization and retrieval of topics, making the navigation intuitive and informative.

## Local Deployment
The project provides multiple ways for local deployment, ensuring flexibility and ease of setup. The deployment scripts are located in the `deploy` folder at the root of the service directory. Two primary methods are outlined below for local deployment:

### 1. Docker Deployment

#### Prerequisites:
- [Docker](https://docs.docker.com) installed on your machine.
- Docker Swarm initialized.
  
  ```bash
  docker swarm init
  ```

#### Instructions:
- Navigate to the root directory of the service.
- Choose and run one of the deployment scripts based on your operating system and desired setup:
  - **Mono Build**: A single Docker image containing both the application and database.
    - Linux: `./deploy/docker-lin-mono.sh`
    - Windows: `deploy/docker-win-mono.bat`
  - **Standalone Build**: Separate Docker images for the application and database.
    - Linux: `./deploy/docker-lin-standalone.sh`
    - Windows: `deploy/docker-win-standalone.bat`
#### Script Parameters:
- The scripts accept optional parameters to override the default database configurations. The parameters and their default values are as follows:
    - `-db_sa_password`: Database SA password (Default: `gardroch`)
    - `-db_name`: Database name (Default: `TopicManagementDb`)
    - `-db_user_username` or `-username`: Database user username (Default: `dupler`)
    - `-db_user_password` or `-userpassword`: Database user password (Default: `sanchez`)

#### Examples:
##### Windows:
  ```bash
  deploy/docker-win-mono.bat -db_sa_password "DB_SA_PASSWORD"
  ```
##### Linux:
  ```bash
  ./deploy/docker-lin-standalone.sh -db_sa_password "DB_SA_PASSWORD" -db_name "DB_NAME" -db_user_username "DB_USER_USERNAME" -db_user_password "DB_USER_PASSWORD"
  ```

#### Note:
  - The deployment setups provided are aimed at simplified initial configurations. Each time a deployment is made, a new database is created and populated with data. This setup is ideal for local development and quick overview
  - These setups use docker secrets `db_sa_password` `db_name` `db_user_username` `db_user_password` `db_server`
### 2. Manual Deployment

#### Prerequisites:
- .NET 7.0 or higher installed on your machine.
- SQL Server Management Studio (SSMS) or Visual Studio for database publishing.

#### Instructions:
- Publish the database using the `csproj` database project file via [Visual Studio](https://visualstudio.microsoft.com/) or [SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16).
- Set the necessary environment variables for database connection:
    - `DB_SA_PASSWORD`
    - `DB_NAME`
    - `DB_USER_USERNAME`
    - `DB_USER_PASSWORD`
    - `DB_SERVER`
- Navigate to the `src/TopicManagementService.API` directory.
- Build and run the project:
  
    ```bash
    dotnet build TopicManagementService.API.csproj
    dotnet run --project TopicManagementService.API.csproj
    ```

## Verification

Post deployment, the application can be accessed and verified at [http://localhost:5000/swagger](http://localhost:5000/swagger).

## Contributing

New contributors are welcome. Please fork the repository and submit your changes via a Pull Request.

## Future Enhancements

This project lays the foundation for an evolving microservices ecosystem. Future microservices will further enrich the capabilities and provide a more comprehensive platform for .NET topic management and exploration.