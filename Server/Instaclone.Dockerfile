# 1. Build application in image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["CSRFInstaClone.WebAPI/CSRFInstaClone.WebAPI.csproj", "CSRFInstaClone.WebAPI/"]
COPY ["CSRFInstaClone.Infrastructure/CSRFInstaClone.Infrastructure.csproj", "CSRFInstaClone.Infrastructure/"]
COPY ["CSRFInstaClone.Core/CSRFInstaClone.Core.csproj", "CSRFInstaClone.Core/"]

RUN dotnet restore "CSRFInstaClone.WebAPI/CSRFInstaClone.WebAPI.csproj"

COPY . .
WORKDIR "/src/CSRFInstaClone.WebAPI"

RUN dotnet build "CSRFInstaClone.WebAPI.csproj" -c Release -o /app/build

# 2. Publish built application in image
FROM build AS publish
RUN dotnet publish "CSRFInstaClone.WebAPI.csproj" -c Release -o /app/publish

# 3. Take published version
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
EXPOSE 7002
WORKDIR /app
COPY --from=publish /app/publish .
