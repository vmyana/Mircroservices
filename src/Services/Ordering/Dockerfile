#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ordering.API/Ordering.API.csproj", "Services/Ordering/Ordering.API/"]
COPY ["Ordering.Application/Ordering.Application.csproj", "Services/Ordering/Ordering.Application/"]
COPY ["Ordering.Domain/Ordering.Domain.csproj", "Services/Ordering/Ordering.Domain/"]
COPY ["Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Services/Ordering/Ordering.Infrastructure/"]
RUN dotnet restore "Services/Ordering/Ordering.API/Ordering.API.csproj"
WORKDIR "/src/Services/Ordering/Ordering.API"
COPY . .
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]