FROM microsoft/dotnet:2.1-sdk as build

COPY ./todo-api.csproj /src/todo-api.csproj
RUN dotnet restore /src/todo-api.csproj
RUN mkdir /out

COPY . /src

RUN dotnet publish /src --output /out

FROM microsoft/dotnet:2.1.0-preview2-aspnetcore-runtime as runtime

COPY --from=build /out /app

WORKDIR /app
EXPOSE 443
EXPOSE 80
ENTRYPOINT [ "dotnet", "todo-api.dll" ]