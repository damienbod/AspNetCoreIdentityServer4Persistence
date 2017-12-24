
dotnet ef migrations add id4configMigration --context ConfigurationStoreContext

dotnet ef database update --context ConfigurationStoreContext --verbose