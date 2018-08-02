FROM microsoft/dotnet:2.1-sdk as build

COPY ./todo-jobdashboard.csproj /app/todo-jobdashboard.csproj
RUN dotnet restore /app/todo-jobdashboard.csproj

COPY . /app
WORKDIR /app

EXPOSE 6001
ENTRYPOINT [ "dotnet", "run"]