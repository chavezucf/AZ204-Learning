# Use the SDK image for building the application
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ARG BUILD_CONFIGURATION=Release

# Copy the project file and restore dependencies
COPY ["SimpleWebApi.csproj", "./"]
RUN dotnet restore "SimpleWebApi.csproj"
# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/"
RUN dotnet build "SimpleWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "SimpleWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the ASP.NET runtime image for the final stage
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleWebApi.dll"]
