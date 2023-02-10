#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Bookswap.API/Bookswap.API.csproj", "src/Bookswap.API/"]
COPY ["src/Bookswap.Application/Bookswap.Application.csproj", "src/Bookswap.Application/"]
COPY ["src/Bookswap.Infrastructure/Bookswap.Infrastructure.csproj", "src/Bookswap.Infrastructure/"]
COPY ["src/Bookswap.Domain/Bookswap.Domain.csproj", "src/Bookswap.Domain/"]
RUN dotnet restore "src/Bookswap.API/Bookswap.API.csproj"
COPY . .
WORKDIR "/src/src/Bookswap.API"
RUN dotnet build "Bookswap.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bookswap.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bookswap.API.dll"]