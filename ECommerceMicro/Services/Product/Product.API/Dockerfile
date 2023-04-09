FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Shared/*.csproj Shared/Shared/
COPY Services/Product/Product.API/*.csproj Services/Product/Product.API/
RUN dotnet restore Services/Product/Product.API/*.csproj
COPY . .
RUN dotnet publish Services/Product/Product.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Product.API.dll" ]