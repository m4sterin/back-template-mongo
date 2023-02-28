# Define a imagem base como SDK do .NET Core 7.0
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Define o diretório de trabalho como /app
WORKDIR /app

# Copia o arquivo de projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante do código-fonte e compila o projeto
COPY . ./
RUN dotnet publish -c Release -o out

# Define a imagem base como ASP.NET Core Runtime 7.0
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Define o diretório de trabalho como /app
WORKDIR /app

# Copia os arquivos de saída da compilação para o contêiner
COPY --from=build-env /app/out .

# Define a porta em que o contêiner escutará
EXPOSE 4200

# Executa o comando para iniciar o contêiner
ENTRYPOINT ["dotnet", "back_template_mongo.dll"]
