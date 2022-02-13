#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Contexts/Profiles/SuperTutor.Contexts.Profiles.Startup/SuperTutor.Contexts.Profiles.Startup.csproj", "Contexts/Profiles/SuperTutor.Contexts.Profiles.Startup/"]
COPY ["Contexts/Profiles/SuperTutor.Contexts.Profiles.Infrastructure/SuperTutor.Contexts.Profiles.Infrastructure.csproj", "Contexts/Profiles/SuperTutor.Contexts.Profiles.Infrastructure/"]
COPY ["Contexts/Profiles/SuperTutor.Contexts.Profiles.Application/SuperTutor.Contexts.Profiles.Application.csproj", "Contexts/Profiles/SuperTutor.Contexts.Profiles.Application/"]
COPY ["Contexts/Profiles/SuperTutor.Contexts.Profiles.Domain/SuperTutor.Contexts.Profiles.Domain.csproj", "Contexts/Profiles/SuperTutor.Contexts.Profiles.Domain/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Domain/SuperTutor.SharedLibraries.BuildingBlocks.Domain.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Domain/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Application/SuperTutor.SharedLibraries.BuildingBlocks.Application.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Application/"]
COPY ["Contexts/Identity/SuperTutor.Contexts.Identity.IntegrationEvents/SuperTutor.Contexts.Identity.IntegrationEvents.csproj", "Contexts/Identity/SuperTutor.Contexts.Identity.IntegrationEvents/"]
COPY ["Contexts/Profiles/SuperTutor.Contexts.Profiles.Api/SuperTutor.Contexts.Profiles.Api.csproj", "Contexts/Profiles/SuperTutor.Contexts.Profiles.Api/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Api/SuperTutor.SharedLibraries.BuildingBlocks.Api.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Api/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure/"]
COPY ["Contexts/Profiles/SuperTutor.Contexts.Profiles.Persistence/SuperTutor.Contexts.Profiles.Persistence.csproj", "Contexts/Profiles/SuperTutor.Contexts.Profiles.Persistence/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Persistence/SuperTutor.SharedLibraries.BuildingBlocks.Persistence.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Persistence/"]
RUN dotnet restore "Contexts/Profiles/SuperTutor.Contexts.Profiles.Startup/SuperTutor.Contexts.Profiles.Startup.csproj"
COPY . .
WORKDIR "/src/Contexts/Profiles/SuperTutor.Contexts.Profiles.Startup"
RUN dotnet build "SuperTutor.Contexts.Profiles.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuperTutor.Contexts.Profiles.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperTutor.Contexts.Profiles.Startup.dll"]