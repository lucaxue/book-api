FROM mcr.microsoft.com/dotnet/aspnet:5.0
FROM mcr.microsoft.com/dotnet/sdk:5.0

LABEL author="lucaxue"

WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80  

RUN mkdir src
COPY src/BookApi.csproj ./src
RUN dotnet restore "./src/BookApi.csproj"

RUN mkdir tests
COPY tests/BookApi.UnitTests.csproj ./tests
RUN dotnet restore "./tests/BookApi.UnitTests.csproj"
RUN dotnet dev-certs https

COPY . ./

RUN cd src
ENTRYPOINT ["dotnet", "watch", "run", "--project", "./src/BookApi.csproj"]
