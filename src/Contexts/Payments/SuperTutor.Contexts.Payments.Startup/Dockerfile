#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Contexts/Payments/SuperTutor.Contexts.Payments.Startup/SuperTutor.Contexts.Payments.Startup.csproj", "Contexts/Payments/SuperTutor.Contexts.Payments.Startup/"]
RUN dotnet restore "Contexts/Payments/SuperTutor.Contexts.Payments.Startup/SuperTutor.Contexts.Payments.Startup.csproj"
COPY . .
WORKDIR "/src/Contexts/Payments/SuperTutor.Contexts.Payments.Startup"
RUN dotnet build "SuperTutor.Contexts.Payments.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuperTutor.Contexts.Payments.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperTutor.Contexts.Payments.Startup.dll"]