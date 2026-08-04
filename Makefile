.PHONY: run restore migrate_add migrate_upd

run:
	dotnet run

restore:
	dotnet restore

migrate_add:
	@test -n "$(name)" || (echo "Usage: make migrate_add name=MigrationName"; exit 1)
	dotnet ef migrations add $(name)

migrate_upd:
	dotnet ef database update
