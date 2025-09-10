# Инструкции по настройке SMTP для Outlook

## Проблема
Microsoft отключил базовую аутентификацию для SMTP. Ошибка "Authentication unsuccessful, basic authentication is disabled" означает, что нужно использовать App Password.

## Решение

### 1. Включить двухфакторную аутентификацию
1. Войдите в аккаунт Microsoft: https://account.microsoft.com/
2. Перейдите в "Безопасность" → "Дополнительные параметры безопасности"
3. Включите "Двухфакторная аутентификация"

### 2. Создать App Password
1. В том же разделе "Безопасность" найдите "Пароли приложений"
2. Нажмите "Создать новый пароль приложения"
3. Введите название (например, "MyRoof SMTP")
4. Скопируйте сгенерированный пароль (16 символов)

### 3. Обновить настройки в appsettings.json
Замените текущий пароль на новый App Password:

```json
{
  "EmailSettings": {
    "SmtpServer": "smtp-mail.outlook.com",
    "SmtpPort": 587,
    "Username": "TeamYourRoof@outlook.com",
    "Password": "ВАШ_НОВЫЙ_APP_PASSWORD",
    "FromEmail": "TeamYourRoof@outlook.com",
    "ToEmail": "TeamYourRoof@outlook.com",
    "EnableSsl": true,
    "UseDefaultCredentials": false
  }
}
```

### 4. Альтернативные настройки SMTP

Если App Password не работает, попробуйте:

#### Gmail SMTP:
```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "FromEmail": "your-email@gmail.com",
    "ToEmail": "TeamYourRoof@outlook.com",
    "EnableSsl": true,
    "UseDefaultCredentials": false
  }
}
```

#### Yandex SMTP:
```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.yandex.ru",
    "SmtpPort": 587,
    "Username": "your-email@yandex.ru",
    "Password": "your-password",
    "FromEmail": "your-email@yandex.ru",
    "ToEmail": "TeamYourRoof@outlook.com",
    "EnableSsl": true,
    "UseDefaultCredentials": false
  }
}
```

## Тестирование
После обновления настроек перезапустите API и попробуйте отправить тестовое сообщение через форму контактов.

## Логи
Проверьте логи в консоли API - там будет видно, какой метод аутентификации сработал или какие ошибки возникают.
