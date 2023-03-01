# back-template-mongo

REST API usando .NET e MongoDB, esse projeto é um template para backend que poder ser utilizado por qualquer pessoa, é uma forma também de aplicar meus conhecimentos e o que estou estudando vou acrescentando no projeto aos poucos.

O projeto consta com diversos conceitos implementados que talvez nao vao estar descritos aqui no readme como por exemplo um padrao de projetos singleton JWT, AutoMapper, etc... Com o tempo vou atualizando aqui direitinho para informar tudo que consta no repositorio.

:whale2: **image**: m4sterin/back-template-mongo:1.0 /Nao tem a build, TODO

Criar uma API web que executa operações Create, Read, Update, and Delete (CRUD) numa base de dados MongoDB NoSQL

- [x] Health Check
- [x] Swagger
- [x] MongoDB Persistence 
- [x] Unit Tests
- [ ] Dockerfile and docker-compose
- [ ] Kubernetes manifests
- [ ] GitHub Actions/Workflow

## Prerequisites

- [.NET 7.0](https://dotnet.microsoft.com/download)
- MongoDB
- Docker Engine >= 20.10.17 
- Docker Compose >= v2.6.0
- Helm >= v3.8.0

## Getting started

Start MongoDB
```
docker run --rm --name mongodb -d -p 27017:27017 mongo:latest 
```

Run application
```
dotnet restore
export ASPNETCORE_ENVIRONMENT=Development && dotnet run
```
> browser: http://localhost:4100/swagger
Checking
```
curl http://0.0.0.0:4100/health
curl -X GET "http://0.0.0.0:4100/api/Livro"
```
Run tests
```
dotnet test
```

## Create/Publish a Docker Image

```
docker build -t <user>/back-template-mongo<tagname> .
docker login
docker push <user>/back-template-mongo<tagname>
```

## Docker Compose
TODO
```
docker compose up -d
```

## kubernetes
TODO
Create a local cluster with kind
```
kind create cluster --name demo-back-template-mongo --config kind.yaml
kind get clusters
```

### Deploy Yaml
TODO
Deploy back-template-mongo and mongo-example
```
kubectl apply -f ./k8s
```

or
```
kubectl apply -f https://raw.githubusercontent.com/apolzek/api-books-dotnet/main/k8s/apibooks.yml
kubectl apply -f https://raw.githubusercontent.com/apolzek/api-books-dotnet/main/k8s/mongo.yml
```

### Deploy with Helm
TODO
```
helm install api-books-dotnet helm/
helm install mongo-example bitnami/mongodb --set fullnameOverride=mongo-example --set auth.enabled=false
```

### Port-forward
TODO
```
kubectl port-forward svc/api-books-dotnet 4000:4000
```

## API Details

### Mongo object example(Livros)

```javascript
    {
      "id": "63fec98a435bb6ee319b3696",
      "titulo": "O Senhor dos Aneis",
      "autor": "Tolkien",
      "editora": "Allen",
      "ano": 1954,
      "sinopse": "Mt bom",
      "descricao": "Rpgzao",
      "qtd_paginas": 2954,
      "preco": 1.99
    },
    {
      "id": "63feca3f435bb6ee319b3697",
      "titulo": "Cronicas de Fogo e Gelo: a furia dos Reis",
      "autor": "George R. R. Martin",
      "editora": "LeYa",
      "ano": 1990,
      "sinopse": "Mt bom",
      "descricao": "Brabo",
      "qtd_paginas": 592,
      "preco": 80
    }
```

### Change port

Edit *Program.cs* file
```
WebHost.CreateDefaultBuilder(args)
    .UseStartup<Startup>()
    .UseUrls(urls: "http://0.0.0.0:4100")
    .Build();
```
> OBS: Impacts Docker image. change port in Dockerfile
### Insert manually(mongo)

MongoDB cli
```
docker exec -it <CONTAINER_ID> bash
mongo
use BancoTemplate
db.Livro.insertOne({"titulo": "O Senhor dos Aneis", "autor": "Tolkien", "editora": "Allen", "ano": 1954, "sinopse": "Mt bom", "descricao": "Rpgzao", "qtd_paginas": 2954, "preco": 1.99 })
```

### Swagger(open on brownser)

  - Navigate to `http://localhost:<port>/swagger/index.html`
  - Example: `http://localhost:4100/swagger/index.html`

### HealthCheck

  - Access http://localhost:4100/health
