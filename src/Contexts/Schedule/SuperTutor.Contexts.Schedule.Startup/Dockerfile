#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Contexts/Schedule/SuperTutor.Contexts.Schedule.Startup/SuperTutor.Contexts.Schedule.Startup.csproj", "Contexts/Schedule/SuperTutor.Contexts.Schedule.Startup/"]
COPY ["Contexts/Schedule/SuperTutor.Contexts.Schedule.Infrastructure/SuperTutor.Contexts.Schedule.Infrastructure.csproj", "Contexts/Schedule/SuperTutor.Contexts.Schedule.Infrastructure/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Application/SuperTutor.SharedLibraries.BuildingBlocks.Application.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Application/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Domain/SuperTutor.SharedLibraries.BuildingBlocks.Domain.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Domain/"]
COPY ["Contexts/Schedule/SuperTutor.Contexts.Schedule.Persistence/SuperTutor.Contexts.Schedule.Persistence.csproj", "Contexts/Schedule/SuperTutor.Contexts.Schedule.Persistence/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Persistence/SuperTutor.SharedLibraries.BuildingBlocks.Persistence.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Persistence/"]
COPY ["Contexts/Schedule/SuperTutor.Contexts.Schedule.Api/SuperTutor.Contexts.Schedule.Api.csproj", "Contexts/Schedule/SuperTutor.Contexts.Schedule.Api/"]
COPY ["Contexts/Schedule/SuperTutor.Contexts.Schedule.Application/SuperTutor.Contexts.Schedule.Application.csproj", "Contexts/Schedule/SuperTutor.Contexts.Schedule.Application/"]
COPY ["Contexts/Schedule/SuperTutor.Contexts.Schedule.Domain/SuperTutor.Contexts.Schedule.Domain.csproj", "Contexts/Schedule/SuperTutor.Contexts.Schedule.Domain/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Api/SuperTutor.SharedLibraries.BuildingBlocks.Api.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Api/"]
RUN dotnet restore "Contexts/Schedule/SuperTutor.Contexts.Schedule.Startup/SuperTutor.Contexts.Schedule.Startup.csproj"
COPY . .
WORKDIR "/src/Contexts/Schedule/SuperTutor.Contexts.Schedule.Startup"
RUN dotnet build "SuperTutor.Contexts.Schedule.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuperTutor.Contexts.Schedule.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperTutor.Contexts.Schedule.Startup.dll"]