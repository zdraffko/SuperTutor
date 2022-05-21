#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Startup/SuperTutor.Contexts.Classrooms.Startup.csproj", "Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Startup/"]
COPY ["Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Infrastructure/SuperTutor.Contexts.Classrooms.Infrastructure.csproj", "Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Infrastructure/"]
COPY ["Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Application/SuperTutor.Contexts.Classrooms.Application.csproj", "Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Application/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Application/SuperTutor.SharedLibraries.BuildingBlocks.Application.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Application/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Domain/SuperTutor.SharedLibraries.BuildingBlocks.Domain.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Domain/"]
COPY ["Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Domain/SuperTutor.Contexts.Classrooms.Domain.csproj", "Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Domain/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure/"]
COPY ["Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Api/SuperTutor.Contexts.Classrooms.Api.csproj", "Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Api/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Api/SuperTutor.SharedLibraries.BuildingBlocks.Api.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Api/"]
RUN dotnet restore "Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Startup/SuperTutor.Contexts.Classrooms.Startup.csproj"
COPY . .
WORKDIR "/src/Contexts/Classrooms/SuperTutor.Contexts.Classrooms.Startup"
RUN dotnet build "SuperTutor.Contexts.Classrooms.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuperTutor.Contexts.Classrooms.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperTutor.Contexts.Classrooms.Startup.dll"]