FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AuthModuleSpu.Presentation/AuthModuleSpu.Presentation.csproj", "src/AuthModuleSpu.Presentation/"]
RUN dotnet restore "src/AuthModuleSpu.Presentation/AuthModuleSpu.Presentation.csproj"
COPY . .
WORKDIR "/src/src/AuthModuleSpu.Presentation"
RUN dotnet build "AuthModuleSpu.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AuthModuleSpu.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthModuleSpu.Presentation.dll"]
