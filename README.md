# StationeryStore (Windows Forms + SQL Server)

Если проект не запускается в Visual Studio — в этом обновлении добавлены `launchSettings.json` и NuGet-зависимость `Microsoft.Data.SqlClient`.

## Быстрый запуск в Visual Studio 2022
1. Откройте **именно** файл `StationeryStore.csproj` (или папку проекта) в Visual Studio 2022.
2. Нажмите **Restore NuGet Packages** (восстановить пакеты).
3. Выполните SQL-скрипт `Sql/StationeryStoreDb.sql` в SQL Server Management Studio.
4. Проверьте строку подключения в `appsettings.json`:
   - для локального SQL Server: `Server=localhost;Database=StationeryStoreDb;Trusted_Connection=True;TrustServerCertificate=True;`
5. Нажмите `F5`.

## Частые причины, почему «не запускается»
- Не восстановлены NuGet-пакеты (`Microsoft.Data.SqlClient`).
- Не создана БД (скрипт не выполнен).
- Неправильный `Server` в строке подключения.
- Нет прав доступа к SQL Server (проверьте Windows Authentication/логин).

## Что реализовано
- Регистрация/авторизация по email или телефону.
- Роли: User/Manager/Admin с ограничением кнопок в UI.
- Каталог с поиском/сортировкой, выделением дорогих товаров (>1000).
- Отображение скидки: старая цена + рассчитанная текущая.
- Редактирование профиля пользователя.
- Добавление/редактирование товара с загрузкой изображения через `OpenFileDialog` в `ProductImages`.
- Перехват ошибки удаления товара из заказов с понятным сообщением.
- Глобальная обработка исключений в `Program.cs`.


## Структура форм
- Все окна разделены на `FormName.cs` (логика) + `FormName.Designer.cs` (визуальные элементы), чтобы формы открывались и редактировались через WinForms Designer.
