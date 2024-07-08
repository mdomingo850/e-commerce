#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["E-Commerce/API.csproj", "E-Commerce/"]
COPY ["Modules.Customers.Api/Modules.Customers.Api.csproj", "Modules.Customers.Api/"]
COPY ["Modules.Customers.Persistence/Modules.Customers.Persistence.csproj", "Modules.Customers.Persistence/"]
COPY ["Modules.Customers.Application/Modules.Customers.Application.csproj", "Modules.Customers.Application/"]
COPY ["Modules.Customers.Domain/Modules.Customers.Domain.csproj", "Modules.Customers.Domain/"]
COPY ["SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["Modules.Inventories.Api/Modules.Inventories.Api.csproj", "Modules.Inventories.Api/"]
COPY ["Modules.Inventories.Application/Modules.Inventories.Application.csproj", "Modules.Inventories.Application/"]
COPY ["Modules.Inventories.Domain/Modules.Inventories.Domain.csproj", "Modules.Inventories.Domain/"]
COPY ["Modules.Inventories.IntegrationEvents/Modules.Inventories.IntegrationEvents.csproj", "Modules.Inventories.IntegrationEvents/"]
COPY ["Modules.Inventories.Persistence/Modules.Inventories.Persistence.csproj", "Modules.Inventories.Persistence/"]
COPY ["Modules.Payments.IntegrationEvents/Modules.Payments.IntegrationEvents.csproj", "Modules.Payments.IntegrationEvents/"]
COPY ["Modules.Notification.Api/Modules.Notifications.Api.csproj", "Modules.Notification.Api/"]
COPY ["Modules.Notifications.Application/Modules.Notifications.Application.csproj", "Modules.Notifications.Application/"]
COPY ["Modules.Notifications.Infrastructure/Modules.Notifications.Infrastructure.csproj", "Modules.Notifications.Infrastructure/"]
COPY ["Modules.Notifications.IntegrationEvents/Modules.Notifications.IntegrationEvents.csproj", "Modules.Notifications.IntegrationEvents/"]
COPY ["Modules.Orders.Api/Modules.Orders.Api.csproj", "Modules.Orders.Api/"]
COPY ["Modules.Orders.Application/Modules.Orders.Application.csproj", "Modules.Orders.Application/"]
COPY ["Modules.Orders.Domain/Modules.Orders.Domain.csproj", "Modules.Orders.Domain/"]
COPY ["Modules.Payments.Api/Modules.Payments.Api.csproj", "Modules.Payments.Api/"]
COPY ["Modules.Payments.Application/Modules.Payments.Application.csproj", "Modules.Payments.Application/"]
COPY ["Modules.Payments.Infrastructure/Modules.Payments.Infrastructure.csproj", "Modules.Payments.Infrastructure/"]
COPY ["Modules.Orders.Infrastructure/Modules.Orders.Infrastructure.csproj", "Modules.Orders.Infrastructure/"]
COPY ["Modules.Orders.Persistence/Modules.Orders.Persistence.csproj", "Modules.Orders.Persistence/"]
RUN dotnet restore "./E-Commerce/./API.csproj"
COPY . .
WORKDIR "/src/E-Commerce"
RUN dotnet build "./API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]