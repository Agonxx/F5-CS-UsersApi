# F5-CS-UsersApi

Microsserviço de autenticação e cadastro de doadores da plataforma **Conexão Solidária** (Hackathon FIAP Pós Tech, Fase 5). Repositório-irmão de [conexao-solidaria](https://github.com/Agonxx/conexao-solidaria), que concentra a documentação e as decisões do projeto.

## Responsabilidades

- Autenticação JWT com dois perfis: `GestorONG` e `Doador`
- Cadastro público de doadores (Nome Completo, Email único, CPF validado, senha com hash BCrypt)
- Endpoint autenticado `GetMe` para consultar o usuário logado

## Stack

.NET 9, EF Core + SQL Server, BCrypt.Net-Next, JWT Bearer, Swagger, Prometheus (`/metrics`).

## Como rodar localmente

Pré-requisito: Docker Desktop.

```
docker compose up -d --build
```

Sobe a API (porta 5001) e o SQL Server (porta 1433). Swagger em `http://localhost:5001/swagger`. O banco é criado e populado automaticamente na primeira execução, com um usuário `GestorONG` de teste:

- Email: `gestor@esperancasolidaria.org`
- Senha: `Gestor@123`

## Endpoints

| Método | Rota | Acesso |
|---|---|---|
| POST | `/api/Usuario/Auth` | Público — `{ "email", "senha" }`, retorna `{ "token" }` |
| POST | `/api/Usuario/Cadastro` | Público — `{ "nomeCompleto", "email", "cpf", "senha" }` |
| GET | `/api/Usuario/GetMe` | Autenticado — `Authorization: Bearer <token>` |

## Testes

```
dotnet test UsersApi.Tests/UsersApi.Tests.csproj
```
