FROM microsoft/dotnet:2.1-sdk AS build

COPY ./todo-db.csproj /src/todo-db.csproj
RUN dotnet restore /src/todo-db.csproj
RUN mkdir /out

COPY . /src

RUN dotnet publish /src -o /out

FROM microsoft/dotnet:2.1-runtime AS run
WORKDIR /app
COPY --from=build /out /app
COPY ./appsettings.json /app/appsettings.json
ENTRYPOINT [ "dotnet", "/app/todo-db.dll" ]