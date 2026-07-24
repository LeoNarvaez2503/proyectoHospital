FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src/Login

COPY Login/Login.sln .
COPY Login/CapaEntidad/CapaEntidad.csproj CapaEntidad/
COPY Login/CapaDatos/CapaDatos.csproj CapaDatos/
COPY Login/CapaNegocio/CapaNegocio.csproj CapaNegocio/
COPY Login/Login/Login.csproj Login/

RUN dotnet restore

COPY Login/ .

RUN dotnet publish Login/Login.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5076
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 5076

ENTRYPOINT ["dotnet", "Login.dll"]
