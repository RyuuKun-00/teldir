echo "Running a project in docker"

docker-compose up -d --build

docker exec telephonedirectory-backend-1 dotnet ../migration/backend.Migrations.dll

pause