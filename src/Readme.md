

docker build -t docker-command -f Manual.Dockerfile .

docker run -d -p 5000:80 --rm -v ${PWD}:/app/data  dotnet-command