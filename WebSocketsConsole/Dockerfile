FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebSocketsConsole/WebSocketsConsole.csproj", "WebSocketsConsole/"]
RUN dotnet restore "WebSocketsConsole/WebSocketsConsole.csproj"
COPY . .
WORKDIR "/src/WebSocketsConsole"
RUN dotnet build "WebSocketsConsole.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebSocketsConsole.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebSocketsConsole.dll"]
