#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Contexts/Catalog/SuperTutor.Contexts.Catalog.Startup/SuperTutor.Contexts.Catalog.Startup.csproj", "Contexts/Catalog/SuperTutor.Contexts.Catalog.Startup/"]
COPY ["Contexts/Catalog/SuperTutor.Contexts.Catalog.Infrastructure/SuperTutor.Contexts.Catalog.Infrastructure.csproj", "Contexts/Catalog/SuperTutor.Contexts.Catalog.Infrastructure/"]
COPY ["Contexts/Identity/SuperTutor.Contexts.Identity.IntegrationEvents/SuperTutor.Contexts.Identity.IntegrationEvents.csproj", "Contexts/Identity/SuperTutor.Contexts.Identity.IntegrationEvents/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Application/SuperTutor.SharedLibraries.BuildingBlocks.Application.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Application/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Domain/SuperTutor.SharedLibraries.BuildingBlocks.Domain.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Domain/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure/"]
COPY ["Contexts/Catalog/SuperTutor.Contexts.Catalog.Application/SuperTutor.Contexts.Catalog.Application.csproj", "Contexts/Catalog/SuperTutor.Contexts.Catalog.Application/"]
COPY ["Contexts/Catalog/SuperTutor.Contexts.Catalog.Domain/SuperTutor.Contexts.Catalog.Domain.csproj", "Contexts/Catalog/SuperTutor.Contexts.Catalog.Domain/"]
COPY ["Contexts/Profiles/SuperTutor.Contexts.Profiles.IntegrationEvents/SuperTutor.Contexts.Profiles.IntegrationEvents.csproj", "Contexts/Profiles/SuperTutor.Contexts.Profiles.IntegrationEvents/"]
COPY ["Contexts/Catalog/SuperTutor.Contexts.Catalog.Api/SuperTutor.Contexts.Catalog.Api.csproj", "Contexts/Catalog/SuperTutor.Contexts.Catalog.Api/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Api/SuperTutor.SharedLibraries.BuildingBlocks.Api.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Api/"]
RUN dotnet restore "Contexts/Catalog/SuperTutor.Contexts.Catalog.Startup/SuperTutor.Contexts.Catalog.Startup.csproj"
COPY . .
WORKDIR "/src/Contexts/Catalog/SuperTutor.Contexts.Catalog.Startup"
RUN dotnet build "SuperTutor.Contexts.Catalog.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuperTutor.Contexts.Catalog.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperTutor.Contexts.Catalog.Startup.dll"]