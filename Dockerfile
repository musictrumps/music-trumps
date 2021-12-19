FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
ENV ASPNETCORE_URLS=http://+
EXPOSE 50000

WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app
RUN ls -la /app

WORKDIR /app
ENTRYPOINT ["dotnet", "TrumpEngine.Api.dll"]
