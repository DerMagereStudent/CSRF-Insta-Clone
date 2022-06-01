# 1. Build application in image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["CSRFInstaClone.ApiGateway/CSRFInstaClone.ApiGateway.csproj", "CSRFInstaClone.ApiGateway/"]

RUN dotnet restore "CSRFInstaClone.ApiGateway/CSRFInstaClone.ApiGateway.csproj"

COPY . .
WORKDIR "/src/CSRFInstaClone.ApiGateway"

RUN dotnet build "CSRFInstaClone.ApiGateway.csproj" -c Release -o /app/build

# 2. Publish built application in image
FROM build AS publish
RUN dotnet publish "CSRFInstaClone.ApiGateway.csproj" -c Release -o /app/publish

# 3. Take published version
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
EXPOSE 80
WORKDIR /app
COPY --from=publish /app/publish .