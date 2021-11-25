#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Contexts/Identity/SuperTutor.Contexts.Identity.Startup/SuperTutor.Contexts.Identity.Startup.csproj", "Contexts/Identity/SuperTutor.Contexts.Identity.Startup/"]
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