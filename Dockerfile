FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["CustomerManagement.API/CustomerManagement.API.csproj", "CustomerManagement.API/"]
COPY ["CustomerManagement.Application/CustomerManagement.Application.csproj", "CustomerManagement.Application/"]
COPY ["CustomerManagement.Domain/CustomerManagement.Domain.csproj", "CustomerManagement.Domain/"]
COPY ["CustomerManagement.Infrastructure/CustomerManagement.Infrastructure.csproj", "CustomerManagement.Infrastructure/"]

RUN dotnet restore "CustomerManagement.API/CustomerManagement.API.csproj"
COPY . .
WORKDIR "/src/CustomerManagement.API"
RUN dotnet publish "CustomerManagement.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CustomerManagement.API.dll"]
