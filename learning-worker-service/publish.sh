rm -rf ./bin ./obj
dotnet publish -c Release --self-contained=false /p:PublishSingleFile=true