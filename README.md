# StationeryStore (Windows Forms + SQL Server)

## Быстрый запуск в Visual Studio 2022
1. Откройте `StationeryStore.csproj`.
2. Убедитесь, что установлен **.NET Framework 4.8 Developer Pack**.
3. В SSMS выполните `Sql/StationeryStoreDb.sql` (создаст БД и таблицы).
4. В `appsettings.json` укажите корректный сервер SQL:
   - `Server=(localdb)\\MSSQLLocalDB;Database=StationeryStoreDb;Trusted_Connection=True;`
   - или `Server=.\\SQLEXPRESS;Database=StationeryStoreDb;Trusted_Connection=True;TrustServerCertificate=True;`
5. Назначьте `StationeryStore` стартовым проектом и нажмите `F5`.

## Важно для входа
- Логин администратора: **`admin@example.com`** (или телефон `+79990000000`),
- Пароль: **`Admin123`**.
- В поле логина нельзя вводить `Администратор` — только email или телефон.

## Если ошибка "сервер не найден" (error: 40)
- Проверьте, что экземпляр SQL Server действительно существует (`(localdb)\\MSSQLLocalDB` или `.\\SQLEXPRESS`).
- Запустите службу SQL Server.
- Проверьте строку подключения в `appsettings.json`.
