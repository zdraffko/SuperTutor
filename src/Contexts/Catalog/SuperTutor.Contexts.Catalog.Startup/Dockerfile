#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Contexts/Catalog/SuperTutor.Contexts.Catalog.Startup/SuperTutor.Contexts.Catalog.Startup.csproj", "Contexts/Catalog/SuperTutor.Contexts.Catalog.Startup/"]
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