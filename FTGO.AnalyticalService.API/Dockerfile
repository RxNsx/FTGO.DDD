FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FTGO.AnalyticalService.API/FTGO.AnalyticalService.API.csproj", "FTGO.AnalyticalService.API/"]
COPY ["FTGO.AnalyticalService.Infrastructure/FTGO.AnalyticalService.Infrastructure.csproj", "FTGO.AnalyticalService.Infrastructure/"]
COPY ["FTGO.AnalyticalService.Domain/FTGO.AnalyticalService.Domain.csproj", "FTGO.AnalyticalService.Domain/"]
COPY ["FTGO.AnalyticalService.Application/FTGO.AnalyticalService.Application.csproj", "FTGO.AnalyticalService.Application/"]
RUN dotnet restore "FTGO.AnalyticalService.API/FTGO.AnalyticalService.API.csproj"
COPY . .
WORKDIR "/src/FTGO.AnalyticalService.API"
RUN dotnet build "FTGO.AnalyticalService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FTGO.AnalyticalService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FTGO.AnalyticalService.API.dll"]
