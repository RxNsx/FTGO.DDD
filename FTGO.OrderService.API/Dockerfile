FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FTGO.OrderService.API/FTGO.OrderService.API.csproj", "FTGO.OrderService.API/"]
COPY ["FTGO.OrderService.Application/FTGO.OrderService.Application.csproj", "FTGO.OrderService.Application/"]
COPY ["FTGO.OrderService.Infrastructure/FTGO.OrderService.Infrastructure.csproj", "FTGO.OrderService.Infrastructure/"]
COPY ["FTGO.OrderService.Domain/FTGO.OrderService.Domain.csproj", "FTGO.OrderService.Domain/"]
COPY ["SharedKernel/SharedKernel.csproj", "SharedKernel/"]
RUN dotnet restore "FTGO.OrderService.API/FTGO.OrderService.API.csproj"
COPY . .
WORKDIR "/src/FTGO.OrderService.API"
RUN dotnet build "FTGO.OrderService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FTGO.OrderService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FTGO.OrderService.API.dll"]
