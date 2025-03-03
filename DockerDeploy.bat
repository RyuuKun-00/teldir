echo "Running a project in docker"

docker-compose build --build-arg NEXT_PUBLIC_BACKEND_PORT="4001" 

docker-compose up -d

docker exec telephonedirectory-backend-1 dotnet ../migration/backend.Migrations.dll

pause