# Тестовое задание для Effective Mobile  

## Стек
* ASP.NET Core  
* Entity Framework Core  
* SQLite
* xUnit

## Конфигурационный файл
Конфигурационный [файл](./EffectiveMobile.Web.Api/appsettings.Development.json) включает в себя:  
* Настройку пути для выходного файла логов (DeliveryLogPath) - обязательно к изменению на ваш путь!;
* Настройку пути для выходного файла отфильтрованных заказов (DeliveryOrderPath) - обязательно к изменению на ваш путь!;
* Строки подключения к базам данных (ConnectionStrings);

## Запуск
Есть две базы данных sqlite:
* EffectiveMobile.db - основаная база данных;
* EffectiveMobileTest.db - база данных для интеграционных тестов.

### Миграции баз данных
Для основной бд
```
dotnet ef database update --connection "Filename=../EffectiveMobile.db" --project EffectiveMobile.Common.EntityModel.Sqlite
```
Для тестовой бд
```
dotnet ef database update --connection "Filename=../EffectiveMobileTest.db" --project EffectiveMobile.Common.EntityModel.Sqlite
```
### Запуск сервера
```
dotnet build
```
```
dotnet run --project EffectiveMobile.Web.Api/
```
После чего будет доступен адрес http://localhost:5001/swagger/index.html  
С помощью Swagger можно протестировать функционал приложения.

## Краткое описание реализации
Реализовал две конечных точки в [контроллере](./EffectiveMobile.Web.Api/Controllers/OrdersController.cs):
* Для создания заказа (CreateOrder), которая принимает в POST запросе JSON объект заказа;
* Для получения отфильтрованных данных (с записью в файл, если указан корректный путь директории), 
принимает 2 обязательных параметра (cityDistrict и firstDeliveryDateTime) и один не обязательный (path), если
его не указать то будет использоваться путь из appsetting.

Реализация бизнес логики и работа с бд
* Логика для взаимодействия с базой данных находится в [репозиториях](./EffectiveMobile.Repositories).  
* Бизнес - логика (добавление и фильтрация заказов с записью в файл) находится в [сервисах](./EffectiveMobile.Services).
* Для логирования используется библиотека Serilog.
  
Для валидации данных используются атрибуты в
[DTO](./EffectiveMobile.Common.DTOs) (Data Transfer Object).

Тесты находяться [здесь](./EffectiveMobile.Tests.Integration).
