#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DomainCentricDemo.WebApp/DomainCentricDemo.WebApp.csproj", "DomainCentricDemo.WebApp/"]
COPY ["DomainCentricDemo.Application/DomainCentricDemo.Application.csproj", "DomainCentricDemo.Application/"]
COPY ["DomainCentricDemo.Domain/DomainCentricDemo.Domain.csproj", "DomainCentricDemo.Domain/"]
COPY ["DomainCentricDemo.Infrastrcture/DomainCentricDemo.Infrastrcture.csproj", "DomainCentricDemo.Infrastrcture/"]
RUN dotnet restore "DomainCentricDemo.WebApp/DomainCentricDemo.WebApp.csproj"
COPY . .
WORKDIR "/src/DomainCentricDemo.WebApp"
RUN dotnet build "DomainCentricDemo.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DomainCentricDemo.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DomainCentricDemo.WebApp.dll"]