Get-Content .env | Where-Object { $_ -and -not $_.StartsWith("#") } | ForEach-Object {
    $name, $value = $_ -split '=', 2
    Set-Item "env:$name" $value
}

$env:AWS_TOKEN = (aws codeartifact get-authorization-token --domain $env:AWS_DOMAIN --domain-owner $env:AWS_ACCOUNT_ID --query authorizationToken --output text)

docker build `
  --secret id=aws_token,env=AWS_TOKEN `
  --build-arg AWS_DOMAIN="$env:AWS_DOMAIN" `
  --build-arg AWS_ACCOUNT_ID="$env:AWS_ACCOUNT_ID" `
  --build-arg AWS_REGION="$env:AWS_REGION" `
  --build-arg AWS_REPO_NAME="$env:AWS_REPO_NAME" `
  -t trainee-management-api .
