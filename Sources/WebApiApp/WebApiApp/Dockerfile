#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8000
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApiApp/WebApiApp.csproj", "WebApiApp/"]
RUN dotnet restore "WebApiApp/WebApiApp.csproj" --disable-parallel
COPY . .
WORKDIR "/src/WebApiApp"
RUN dotnet build "WebApiApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiApp.dll"]