FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MassTransit.Producer/MassTransit.Producer.csproj", "MassTransit.Producer/"]
RUN dotnet restore "MassTransit.Producer/MassTransit.Producer.csproj"
COPY . .
WORKDIR "/src/MassTransit.Consumer"
RUN dotnet build "MassTransit.Producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.Producer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.Producer.dll"]
