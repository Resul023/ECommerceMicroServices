FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Shared/*.csproj Shared/Shared/
COPY IdentityServer/IdentityServer.API/*.csproj IdentityServer/IdentityServer.API/
RUN dotnet restore IdentityServer/IdentityServer.API/*.csproj
COPY . .
RUN dotnet publish IdentityServer/IdentityServer.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","IdentityServer.API.dll" ]