# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos esenciales primero (optimización de caché)
COPY Api_Mediconnet_Onion.sln .
COPY Api/Api_Mediconnet.Api.csproj Api/
COPY Common/Api_Mediconnet.Common.csproj Common/
COPY Application/Api_Mediconnet.Application.csproj Application/
COPY Domain/Api_Mediconnet.Domain.csproj Domain/
COPY Infrastructure/Api_Mediconnet.Infrastructure.csproj Infrastructure/

# Restaurar dependencias
RUN dotnet restore Api_Mediconnet_Onion.sln

# Copiar todo el código fuente
COPY . .

# Publicar la API
WORKDIR /src/Api
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api_Mediconnet.Api.dll"]