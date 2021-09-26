#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Authentication Service/Authentication Service.csproj", "Authentication Service/"]
RUN dotnet restore "Authentication Service/Authentication Service.csproj"
COPY . .
WORKDIR "/src/Authentication Service"
RUN dotnet build "Authentication Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication Service.dll"]