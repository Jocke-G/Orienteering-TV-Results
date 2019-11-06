FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["Logic/Logic.csproj", "Logic/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["Rest/Rest.csproj", "Rest/"]
COPY ["ServiceBooter/ServiceBooter.csproj", "ServiceBooter/"]
RUN dotnet restore "ServiceBooter/ServiceBooter.csproj"
COPY . .
WORKDIR "/src/ServiceBooter"
RUN dotnet build "ServiceBooter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceBooter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LayoutService.dll"]
