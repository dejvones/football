# Use the official .NET image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY Football.App/*.csproj ./Football.App/
COPY Football.Data/*.csproj ./Football.Data/
COPY Football.Logic/*.csproj ./Football.Logic/
RUN dotnet restore

# Copy everything else and build
COPY . ./
WORKDIR /app/Football.App
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/Football.App/out .

# Load environment variables from .env file
COPY .env .env
ENV $(cat .env | grep -v ^# | xargs)

ENTRYPOINT ["dotnet", "Football.App.dll"]