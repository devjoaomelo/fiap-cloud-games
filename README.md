# FIAP Cloud Games (FCG)

API RESTful desenvolvida para gerenciamento de usuários e biblioteca de jogos.

---

## Descrição

O FCG (FIAP Cloud Games) é um MVP que simula uma plataforma de venda de jogos digitais. 
Os usuários podem se registrar, fazer login com autenticação JWT, comprar jogos, visualizar e remover jogos da própria biblioteca. Administradores têm acesso total ao sistema, podendo gerenciar qualquer usuário e jogo.

---

## 🧱 Tecnologias e Arquitetura

- ✅ **.NET 8**
- ✅ **ASP.NET Core Web API**
- ✅ **Entity Framework Core**
- ✅ **JWT (Json Web Token)**
- ✅ **Swagger/OpenAPI**
- ✅ **xUnit + Moq**
- ✅ **Arquitetura em Camadas (Clean Architecture + DDD)**

---

## 🚀 Como rodar localmente

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server (ou ajuste da connection string para outro banco compatível)
- Ferramenta como Postman ou usar o Swagger para testar os endpoints

### Passos

1. Clone o repositório:

   ```bash
   git clone https://github.com/seu-usuario/fcg-api.git
   cd fcg-api

2. Configure as variáveis no appsettings.json
   ```json
   {
     "Jwt": {
       "SecretKey": "sua-chave-secreta-de-32-caracteres-alfanumericos-no-minimo",
       "Issuer": "FCGIssuer",
       "Audience": "FCGAudience"
     },
     "ConnectionStrings": {
       "DefaultConnection": "Server=SEU_SERVER;Database=FCGDb;User=SEU_USUARIO;Password=SUA_SENHA;"
     }
   }
   ```
   
3. Aplicar migrations:
   ```bash
   dotnet ef migrations add "FirstMigration"
   dotnet ef database update
   
4. Rode a aplicação
   ```bash
   dotnet run --project FCG.API
   
5. Acesse o Swagger
   ```bash
   http://localhost:{porta}/swagger
---

## Autenticação e Autorização
 - Jwt com roles(User, Admin)
 - Autorização feita via [Authorize(Roles="")]
 - Para fazer a autenticação use o endpoin de login e copie o Bearer
 - Cole o token no Swagger clicando em "Authorize"

## Exemplos Endpoints
(Completo no swagger)

### Autenticação

```bash
POST /api/auth/login
```

### Usuários

```bash
POST /api/users
GET /api/users/{id}
PUT /api/users/{id}
DELETE /api/users/{id}
```

### Jogos

```bash
GET /api/games
POST /api/games
PUT /api/games/{id}
DELETE /api/games/{id}
```

### Biblioteca de Jogos

```bash
POST /api/usergames/games/{gameId}
GET /api/usergames/games
DELETE /api/usergames/games/{gameId}
```

### Ações de Admin

```bash
GET /api/Admin/{userId}/games
PUT /api/Admin/promote/{id}
PUT /api/admin/users/{id}
POST /api/Admin/{userId}/games{gameId}
DELETE /api/Admin/users/{userId}/games/{gameId}
DELETE /api/Admin/{id}
```

## Estrutura de Pastas
```bash
FCG.API/               -> Camada de apresentação (Controllers, Middlewares)
FCG.Application/       -> Casos de uso (Use Cases)
FCG.Domain/            -> Entidades, ValueObjects e Interfaces
FCG.Infra/             -> Repositórios e DataContext (EF Core)
FCG.Tests/             -> Testes unitários (xUnit)
```

## Funcionalidades
- Cadastro e login de usuários
- Adquirir Jogos
- Visualizar e remover jogos da biblioteca pessoal
- Gerenciamento completo de admnistrador
- Proteção de rotas com autorização e autenticação
- Testes de unidade com xUnit e Moq
- Documentação do Swagger
