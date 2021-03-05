# Compile the source code
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source

# Copy everyting in the project dir to the workdir
COPY . .
# Build everything into /app
RUN dotnet publish -c release -o /app

# Build the image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
# Copies from previous build stage
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "OrderApi.dll"]