FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["LayoutService/LayoutRestService.csproj", "LayoutService/"]
RUN dotnet restore "LayoutService/LayoutRestService.csproj"
COPY . .
WORKDIR "/src/LayoutService"
RUN dotnet build "LayoutRestService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LayoutRestService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LayoutService.dll"]
