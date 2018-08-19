FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY LiftDiscord.ConsoleHost/LiftDiscord.ConsoleHost.csproj LiftDiscord.ConsoleHost/
COPY LiftDiscord.DiscordClient/LiftDiscord.DiscordClient.csproj LiftDiscord.DiscordClient/
COPY LiftDiscord.PathOfBuilding/LiftDiscord.PathOfBuilding.csproj LiftDiscord.PathOfBuilding/
RUN dotnet restore LiftDiscord.ConsoleHost/LiftDiscord.ConsoleHost.csproj
COPY . .
WORKDIR /src/LiftDiscord.ConsoleHost
RUN dotnet build LiftDiscord.ConsoleHost.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish LiftDiscord.ConsoleHost.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LiftDiscord.ConsoleHost.dll"]
