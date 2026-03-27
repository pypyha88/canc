# StationeryStore (Windows Forms + SQL Server)

Проект переведен на **.NET Framework 4.8**, чтобы запускался в Visual Studio без обязательного скачивания внешних NuGet-провайдеров SQL.

## Быстрый запуск в Visual Studio 2022
1. Откройте `StationeryStore.csproj`.
2. Убедитесь, что установлен **.NET Framework 4.8 Developer Pack**.
3. Выполните `Sql/StationeryStoreDb.sql` в SQL Server Management Studio.
4. Проверьте строку подключения в `appsettings.json`.
5. Назначьте `StationeryStore` стартовым проектом и нажмите `F5`.

## Если все равно не запускается
- Проверьте `Build -> Rebuild Solution` и посмотрите первые ошибки в **Error List**.
- Проверьте, что SQL Server доступен по имени из строки подключения.
- Проверьте, что база `StationeryStoreDb` создана и таблицы существуют.

## Структура форм
- Все окна разделены на `FormName.cs` (логика) + `FormName.Designer.cs` (визуальные элементы), чтобы формы открывались и редактировались через WinForms Designer.
