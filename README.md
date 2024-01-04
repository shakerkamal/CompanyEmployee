# CompanyEmployee API

## Overview
This repository houses the CompanyEmployee API, a robust solution for efficiently managing company information and employee details. It is built using cutting-edge technologies to ensure security, scalability, and ease of use.

## Key Features

- **Token-Based Authentication:** Secure access through token-based authentication.
- **Company and Employee Management:** Comprehensive functionality for managing company and employee data.
- **Refresh Token Mechanism:** Implemented for seamless access token rotation.

## Technology Stack

- **Dotnet 6.0:** Utilizes the latest version of Dotnet for enhanced performance and features.
- **CQRS Pattern with Mediatr:** Employs a clean and maintainable architecture for command and query handling.
- **Swagger:** Integrated for easy API documentation and testing.
- **Repository Pattern:** Organizes data access for increased maintainability.
- **ASP.NET Identity with JWT:** Ensures secure user authentication and authorization using JSON Web Tokens.
- **Docker:** Containerized for simplified deployment and scalability.
- **Unit Testing:** Test coverage with NUnit and Moq.
## Prerequisites

Before running the project locally, ensure that Dotnet 6.0 is installed. If not, install it by following this [link](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

## Getting Started

### Running Locally

1. Clone the repository:

    ```bash
    git clone https://github.com/shakerkamal/CompanyEmployee.git
    ```

2. Set the environment variable (Windows):

    ```bash
    setx SECRET "SecretKeyForTokenGeneration" /M
    ```

3. Build and run the project in Visual Studio or using the following commands:

    ```bash
    dotnet build
    dotnet run
    ```

4. Open the Swagger documentation in your browser: [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html).

### Running with Docker

1. Navigate to the project folder:

    ```bash
    cd CompanyEmployee
    ```

2. Ensure the Dockerfile is present.

3. Execute the following commands:

    ```bash
    dotnet build
    dotnet publish -c Release
    docker build -t CompanyEmployeeApi .
    docker run -d --name ceapi -p 5000:5000 CompanyEmployeeApi
    ```

4. Verify the container is running:

    ```bash
    docker container ls
    ```

5. Access the Swagger documentation in your browser: [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html).

## Demonstration Steps

1. Register a user with the role `Administrator`.
    ![User Registration](/assets/registration.png)

2. Login with the registered credentials to obtain the access token and refresh token.
    ![User Login](/assets/authentication.png)

3. Use the generated bearer token to authenticate and access the API.
    ![Passing Bearer Token](/assets/passingbearertoken.png)

4. Call the "Get Companies" route to retrieve company information.
    ![Get All Companies](/assets/getcompanies.png)

5. Verify the successful response.
    ![Get All Companies Response](/assets/getcompaniesresponse.png)

## JWT Token Refresh

To refresh the token after its expiration, follow these steps:

1. Access the "Token/Refresh" route in Swagger.
    ![Refresh Token](/assets/refreshtoken.png)

2. Provide the refresh token to receive a new access token and refresh token.
    ![Refresh Token Response](/assets/refreshtokenresponse.png)

For a hands-on experience, refer to the [GitHub Repository](https://github.com/shakerkamal/CompanyEmployee) for the complete source code.

Feel free to explore and test the various features outlined above. If you have any questions or require further assistance, please don't hesitate to reach out.