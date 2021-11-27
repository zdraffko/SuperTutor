#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Contexts/Identity/SuperTutor.Contexts.Identity.Startup/SuperTutor.Contexts.Identity.Startup.csproj", "Contexts/Identity/SuperTutor.Contexts.Identity.Startup/"]
COPY ["Contexts/Identity/SuperTutor.Contexts.Identity.Infrastructure/SuperTutor.Contexts.Identity.Infrastructure.csproj", "Contexts/Identity/SuperTutor.Contexts.Identity.Infrastructure/"]
COPY ["Contexts/Identity/SuperTutor.Contexts.Identity.Application/SuperTutor.Contexts.Identity.Application.csproj", "Contexts/Identity/SuperTutor.Contexts.Identity.Application/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Application/SuperTutor.SharedLibraries.BuildingBlocks.Application.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Application/"]
COPY ["Contexts/Identity/SuperTutor.Contexts.Identity.Persistence/SuperTutor.Contexts.Identity.Persistence.csproj", "Contexts/Identity/SuperTutor.Contexts.Identity.Persistence/"]
COPY ["Contexts/Identity/SuperTutor.Contexts.Identity.Api/SuperTutor.Contexts.Identity.Api.csproj", "Contexts/Identity/SuperTutor.Contexts.Identity.Api/"]
COPY ["SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Api/SuperTutor.SharedLibraries.BuildingBlocks.Api.csproj", "SharedLibraries/BuildingBlocks/SuperTutor.SharedLibraries.BuildingBlocks.Api/"]
RUN dotnet restore "Contexts/Identity/SuperTutor.Contexts.Identity.Startup/SuperTutor.Contexts.Identity.Startup.csproj"
COPY . .
WORKDIR "/src/Contexts/Identity/SuperTutor.Contexts.Identity.Startup"
RUN dotnet build "SuperTutor.Contexts.Identity.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuperTutor.Contexts.Identity.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperTutor.Contexts.Identity.Startup.dll"]
