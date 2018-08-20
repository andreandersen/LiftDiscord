FROM microsoft/dotnet:2.1-sdk as build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish LiftDiscord.ConsoleHost/LiftDiscord.ConsoleHost.csproj -c Release -r linux-x64 -f netcoreapp2.1 -o /app

FROM microsoft/dotnet:2.1-runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "LiftDiscord.ConsoleHost.dll" ]
