#1 - build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY *.sln* .

COPY Sportradar.Core/*.csproj Sportradar.Core/
COPY Sportradar.Infrastructure/*.csproj Sportradar.Infrastructure/
COPY Sportradar.Backend/*.csproj Sportradar.Backend/

RUN dotnet restore

COPY . .

#2 - publish
WORKDIR /src/Sportradar.Backend
RUN dotnet publish -c Release -o /app/publish


#3 - runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "Sportradar.Backend.dll"]