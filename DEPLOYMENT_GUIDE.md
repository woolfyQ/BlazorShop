# Руководство по развертыванию MyRoof

## 🎯 Варианты развертывания

### 1. Azure (Рекомендуется)

#### Blazor WASM (Frontend)
```bash
# Установка Azure CLI
az login

# Создание Static Web App
az staticwebapp create \
  --name myroof-frontend \
  --resource-group myroof-rg \
  --source https://github.com/your-repo \
  --location "West Europe" \
  --branch main \
  --app-location "/MyRoof/MyRoof.WASM" \
  --output-location "wwwroot"
```

#### API (Backend)
```bash
# Создание App Service
az webapp create \
  --resource-group myroof-rg \
  --plan myroof-plan \
  --name myroof-api \
  --runtime "DOTNET|8.0"

# Настройка переменных окружения
az webapp config appsettings set \
  --resource-group myroof-rg \
  --name myroof-api \
  --settings \
    EmailSettings__SmtpServer="smtp.yandex.ru" \
    EmailSettings__Username="your-email@yandex.ru" \
    EmailSettings__Password="your-app-password" \
    EmailSettings__FromEmail="your-email@yandex.ru" \
    EmailSettings__ToEmail="your-email@yandex.ru"
```

### 2. VPS/Сервер (Ubuntu)

#### Установка .NET
```bash
# Установка .NET 8
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Установка Nginx
sudo apt-get install -y nginx
```

#### Настройка Nginx
```nginx
# /etc/nginx/sites-available/myroof
server {
    listen 80;
    server_name your-domain.com;

    # Frontend (Blazor WASM)
    location / {
        root /var/www/myroof/wwwroot;
        try_files $uri $uri/ /index.html;
    }

    # API
    location /api/ {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}
```

#### Systemd Service для API
```ini
# /etc/systemd/system/myroof-api.service
[Unit]
Description=MyRoof API
After=network.target

[Service]
Type=notify
ExecStart=/usr/bin/dotnet /var/www/myroof/MyRoof.API.dll
Restart=always
RestartSec=5
KillSignal=SIGINT
SyslogIdentifier=myroof-api
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://localhost:5000

[Install]
WantedBy=multi-user.target
```

### 3. Docker

#### Dockerfile для API
```dockerfile
# MyRoof.API/Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyRoof.API/MyRoof.API.csproj", "MyRoof.API/"]
RUN dotnet restore "MyRoof.API/MyRoof.API.csproj"
COPY . .
WORKDIR "/src/MyRoof.API"
RUN dotnet build "MyRoof.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyRoof.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyRoof.API.dll"]
```

#### Docker Compose
```yaml
# docker-compose.yml
version: '3.8'
services:
  api:
    build: ./MyRoof.API
    ports:
      - "5000:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - EmailSettings__SmtpServer=smtp.yandex.ru
      - EmailSettings__Username=your-email@yandex.ru
      - EmailSettings__Password=your-app-password
    restart: unless-stopped

  nginx:
    image: nginx:alpine
    ports:
      - "80:80"
      - "443:443"
    volumes:
      - ./wwwroot:/usr/share/nginx/html
      - ./nginx.conf:/etc/nginx/nginx.conf
    depends_on:
      - api
    restart: unless-stopped
```

## 🔧 Настройка для продакшена

### 1. Конфигурация appsettings.Production.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "EmailSettings": {
    "SmtpServer": "smtp.yandex.ru",
    "SmtpPort": 587,
    "Username": "your-corporate-email@company.com",
    "Password": "your-app-password",
    "FromEmail": "your-corporate-email@company.com",
    "ToEmail": "your-corporate-email@company.com",
    "EnableSsl": true,
    "UseDefaultCredentials": false
  }
}
```

### 2. Обновление CORS для продакшена
```csharp
// Program.cs
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowBlazorWasm");
}
else
{
    app.UseCors("AllowProduction");
}

// Добавить в services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowProduction",
        policy =>
        {
            policy.WithOrigins("https://your-domain.com")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
```

### 3. Обновление URL в WASM
```csharp
// Program.cs
var apiUrl = builder.HostEnvironment.IsDevelopment() 
    ? "http://localhost:5107/" 
    : "https://api.your-domain.com/";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });
```

## 📧 SMTP для продакшена

### Корпоративная почта
1. **Microsoft 365/Exchange**:
   - SmtpServer: `smtp.office365.com`
   - Port: `587`
   - Использовать App Password

2. **Google Workspace**:
   - SmtpServer: `smtp.gmail.com`
   - Port: `587`
   - Использовать App Password

3. **Yandex для бизнеса**:
   - SmtpServer: `smtp.yandex.ru`
   - Port: `587`
   - Использовать App Password

### Настройка App Password
1. Включить двухфакторную аутентификацию
2. Создать App Password в настройках безопасности
3. Использовать App Password вместо обычного пароля

## 🔒 Безопасность

### 1. Переменные окружения
```bash
# Никогда не храните пароли в коде!
export EmailSettings__Password="your-secure-password"
export EmailSettings__Username="your-email@company.com"
```

### 2. HTTPS
- Обязательно используйте HTTPS в продакшене
- Настройте SSL сертификаты
- Используйте Let's Encrypt для бесплатных сертификатов

### 3. Firewall
```bash
# Открыть только необходимые порты
sudo ufw allow 80
sudo ufw allow 443
sudo ufw allow 22  # SSH
sudo ufw enable
```

## 📊 Мониторинг

### 1. Логирование
```csharp
// Program.cs
builder.Logging.AddConsole();
builder.Logging.AddDebug();

if (!builder.Environment.IsDevelopment())
{
    builder.Logging.AddApplicationInsights();
}
```

### 2. Health Checks
```csharp
// Program.cs
builder.Services.AddHealthChecks()
    .AddCheck("email", () => HealthCheckResult.Healthy("Email service is working"));

app.MapHealthChecks("/health");
```

## 🚀 Автоматическое развертывание

### GitHub Actions
```yaml
# .github/workflows/deploy.yml
name: Deploy to Production

on:
  push:
    branches: [ main ]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
    
    - name: Build API
      run: dotnet build MyRoof.API --configuration Release
    
    - name: Publish API
      run: dotnet publish MyRoof.API --configuration Release --output ./publish
    
    - name: Build WASM
      run: dotnet build MyRoof.WASM --configuration Release
    
    - name: Publish WASM
      run: dotnet publish MyRoof.WASM --configuration Release --output ./wwwroot
    
    - name: Deploy to server
      uses: appleboy/ssh-action@v0.1.5
      with:
        host: ${{ secrets.HOST }}
        username: ${{ secrets.USERNAME }}
        key: ${{ secrets.SSH_KEY }}
        script: |
          sudo systemctl stop myroof-api
          sudo cp -r ./publish/* /var/www/myroof/
          sudo cp -r ./wwwroot/* /var/www/myroof/wwwroot/
          sudo systemctl start myroof-api
```

## ✅ Чек-лист перед развертыванием

- [ ] Обновить все URL на продакшен
- [ ] Настроить корпоративную почту
- [ ] Создать App Password для SMTP
- [ ] Настроить CORS для домена
- [ ] Добавить HTTPS сертификаты
- [ ] Настроить логирование
- [ ] Добавить мониторинг
- [ ] Протестировать на staging окружении
- [ ] Создать резервные копии
- [ ] Настроить автоматическое развертывание
