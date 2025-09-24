FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App
# Copy everything
COPY . ./

# Build and publish a release
RUN dotnet publish "SOCCER.Api/SOCCER.Api.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .

# Set the port on which the app will run
ENV ASPNETCORE_URLS=http://*:5209
EXPOSE 5209

ENTRYPOINT ["dotnet", "SOCCER.Api.dll"]

# # image used as the base image
# FROM mrc.microsoft.com/dotnet/aspnet:7.0 AS base
# ADD . /app
# # set de working directory
# WORKDIR /app
# # expose the port (port api/port destination)
# EXPOSE 7212:80
# VOLUME app/logs

# FROM mrc.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /src
# COPY ["SOCCER.Api.csproj", "."]
# # commandes à exécuter dans le container
# RUN dotnet restore "./SOCCER.Api.csproj"
# # copy all projects files and folders to the Docker image
# COPY . . 
# RUN dotnet build "SOCCER.Api.csproj" -c Release -o /app/build
# RUN dotnet publish -o /publish