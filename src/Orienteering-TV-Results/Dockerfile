FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Orienteering-TV-Results/Orienteering-TV-Results.csproj", "Orienteering-TV-Results/"]
RUN dotnet restore "Orienteering-TV-Results/Orienteering-TV-Results.csproj"
COPY . .
WORKDIR "/src/Orienteering-TV-Results"
RUN dotnet build "Orienteering-TV-Results.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Orienteering-TV-Results.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Orienteering-TV-Results.dll"]