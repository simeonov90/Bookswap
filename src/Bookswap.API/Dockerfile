#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0@sha256:8cf261bff81a4acc129ba5ba05d3926f861ada24973db211961bf8e6964eeb71 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0@sha256:b40939dddbea23e939af225cab4b30df01afc47ce1e8beb1f7f249aaca8babcc AS build
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