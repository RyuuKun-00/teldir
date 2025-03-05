echo "Running a project in docker"

docker-compose build --build-arg NEXT_PUBLIC_BACKEND_PORT="4001" 

docker-compose up -d

docker exec teldir_backend dotnet ../migration/backend.Migrations.dll

pause