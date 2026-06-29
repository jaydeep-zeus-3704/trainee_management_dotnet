FROM docker-registry-002.zeuslearning.com/zeuslearning/dotnet/sdk:10.0-alpine AS build

WORKDIR /src
COPY . .

ARG CODEARTIFACT_TOKEN
ARG NUGET_SOURCE

RUN dotnet nuget remove source CodeArtifact || true
RUN dotnet nuget remove source codeartifact || true

RUN dotnet nuget add source \
    "$NUGET_SOURCE" \
    --name zeusfeed \
    --username aws \
    --password "$CODEARTIFACT_TOKEN" \
    --store-password-in-clear-text

RUN dotnet nuget list source
RUN cat ~/.nuget/NuGet/NuGet.Config

# Restore ONLY from this source for testing
RUN dotnet restore \
    --source "$NUGET_SOURCE"

RUN dotnet publish -c Release -o /app/publish

FROM docker-registry-002.zeuslearning.com/zeuslearning/dotnet/aspnet:10.0-alpine

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "trainee_management.dll"]