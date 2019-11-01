FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Model/*.csproj ./Model/
COPY OlaDapper/*.csproj ./OlaDapper/
COPY Orienteering-TV-Results/*.csproj ./Orienteering-TV-Results/
WORKDIR /app/Orienteering-TV-Results
RUN dotnet restore

# copy and publish app and libraries
WORKDIR /app/
COPY Model/. ./Model/
COPY OlaDapper/. ./OlaDapper/
COPY Orienteering-TV-Results/. ./Orienteering-TV-Results/
WORKDIR /app/Orienteering-TV-Results
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/Orienteering-TV-Results/out ./
ENTRYPOINT ["dotnet", "Orienteering-TV-Results.dll"]
