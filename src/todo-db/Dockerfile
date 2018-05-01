FROM microsoft/dotnet AS build

COPY ./todo-db.csproj /src/todo-db.csproj
RUN dotnet restore /src/todo-db.csproj

COPY . /src

RUN mkdir /out
RUN dotnet publish /src -o /out

FROM microsoft/dotnet AS run

COPY --from=build /out /app
ENTRYPOINT [ "dotnet", "/app/todo-db.dll" ]