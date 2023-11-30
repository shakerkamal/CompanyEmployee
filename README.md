# CompanyEmployee API
> An API for managing company and the employees of a company.

## Features
 - JWT based authentication
 - Company and Employee Creation

## Built with
> Dotnet 6.0<br>
> Swagger<br>
> Repository Pattern <br>
> ASP.NET Identity

## Pre-requisite
To run locally, check if dotnet version 6.0 is installed by running this command:
> dotnet --version

If not installed then please install dotnet 6.0 following this link: `https://dotnet.microsoft.com/en-us/download/dotnet/6.0`

## Using the project

Either download the project as ZIP or clone the repository using the following command.

```git clone https://github.com/shakerkamal/CompanyEmployee.git```

If you are using Visual Studio then build the project with `Crtl+Shift+B` command. Once the build is done. Run the project by pressing `F5`.

Launch the browser and paste this link or click on this link `http://localhost:5000/swagger/index.html`.

It should come up with this swagger UI of the API.
>![Swagger Screen](/assets/swagger-screen.png)

Since authentication has been enabled, register a user with predefined role `Administrator` following this:
>![User Registration](/assets/registration.png)

After successfully registering, login with the `username` and `password`.
>![User login](/assets/authentication.png)

Correct credentials will generate a `bearer token` and `refresh token`.
>![Authentication response](/assets/authentication-response.png)

Click the Authorize icon and paste the generated `bearer token` in value field followed with the word `bearer`.
>![Starting the application](/assets/passingbearertoken.png)

Call the get companies route and it should return the following response if the above mentioned commands were followed successfully,
>![Get All companies](/assets/getcompanies.png)

And the response:
>![Get All companies response](/assets/getcompaniesresponse.png)

## JWT Token Refresh
The bearer token only lives for 5 minutes. Upon finishing its lifetime, a new token needs to be generated following the `token/refresh` route.
>![Refresh Token](/assets/refreshtoken.png)

After passing the refresh token a new access token and refresh token will be provided in the response.
>![Refresh Token](/assets/refreshtokenresponse.png)