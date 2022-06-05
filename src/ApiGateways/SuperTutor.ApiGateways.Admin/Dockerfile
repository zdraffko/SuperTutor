#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateways/SuperTutor.ApiGateways.Admin/SuperTutor.ApiGateways.Admin.csproj", "ApiGateways/SuperTutor.ApiGateways.Admin/"]
RUN dotnet restore "ApiGateways/SuperTutor.ApiGateways.Admin/SuperTutor.ApiGateways.Admin.csproj"
COPY . .
WORKDIR "/src/ApiGateways/SuperTutor.ApiGateways.Admin"
RUN dotnet build "SuperTutor.ApiGateways.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuperTutor.ApiGateways.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperTutor.ApiGateways.Admin.dll"]