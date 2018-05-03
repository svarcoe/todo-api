FROM microsoft/dotnet:2.1-sdk as build

COPY ./todo-api.csproj /app/todo-api.csproj
RUN dotnet restore /app/todo-api.csproj

COPY . /app
WORKDIR /app

EXPOSE 443
EXPOSE 80
ENTRYPOINT [ "dotnet", "run", "todo-api.dll" ]