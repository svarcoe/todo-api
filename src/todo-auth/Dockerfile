FROM microsoft/dotnet:2.1-sdk as build

COPY ./todo-auth.csproj /app/todo-auth.csproj
RUN dotnet restore /app/todo-auth.csproj

COPY . /app
WORKDIR /app

EXPOSE 5000
ENTRYPOINT [ "dotnet", "run"]