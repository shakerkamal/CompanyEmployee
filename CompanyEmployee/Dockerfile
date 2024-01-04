FROM mcr.microsoft.com/dotnet/aspnet:6.0
FROM mcr.microsoft.com/dotnet/sdk:6.0
COPY /bin/Release/net6.0/publish/ CompanyEmployee/
ENV ASPNETCORE_ENVIRONMENT Production
ENV SECRET=CompanyEmployeeSecretKey 
EXPOSE 5000
WORKDIR /CompanyEmployee
ENTRYPOINT ["dotnet", "CompanyEmployee.dll", "--urls=http://0.0.0.0:5000"]