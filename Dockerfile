FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebBanPizza/WebBanPizza.csproj", "WebBanPizza/"]
RUN dotnet restore "WebBanPizza/WebBanPizza.csproj"
COPY . .
WORKDIR "/src/WebBanPizza"
RUN dotnet publish "WebBanPizza.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebBanPizza.dll"]
