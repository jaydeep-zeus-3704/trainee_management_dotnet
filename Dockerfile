# --- Stage 1: Build & Restore ---
FROM ://zeuslearning.com AS build
WORKDIR /src

# Declare arguments for the AWS repository metadata
ARG AWS_DOMAIN
ARG AWS_ACCOUNT_ID
ARG AWS_REGION
ARG AWS_REPO_NAME

COPY ["trainee_management.csproj", "./"]
COPY ["nuget.config", "./"]

# Pass the ARGs as Environment Variables alongside the secret token
RUN --mount=type=secret,id=aws_token \
    export CODEARTIFACT_TOKEN=$(cat /run/secrets/aws_token) && \
    export AWS_DOMAIN=${AWS_DOMAIN} && \
    export AWS_ACCOUNT_ID=${AWS_ACCOUNT_ID} && \
    export AWS_REGION=${AWS_REGION} && \
    export AWS_REPO_NAME=${AWS_REPO_NAME} && \
    dotnet restore "trainee_management.csproj" --configfile nuget.config

COPY . .
RUN dotnet build "trainee_management.csproj" -c Release --no-restore
RUN dotnet publish "trainee_management.csproj" -c Release -o /app/publish --no-restore

# --- Stage 2: Runtime Image ---
FROM docker-registry-002.zeuslearning.com/zeuslearning/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "trainee_management.dll"]

