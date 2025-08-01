# FIAP Cloud Games (FCG) – Em desenvolvimento da segunda etapa 🚀

API RESTful desenvolvida para gerenciamento de usuários e biblioteca de jogos digitais.

---

## Descrição

O FCG (FIAP Cloud Games) é um MVP que simula uma plataforma de venda de jogos digitais.  
Os usuários podem se registrar, fazer login com autenticação JWT, comprar jogos, visualizar e remover jogos da própria biblioteca.  
Administradores têm acesso total ao sistema, podendo gerenciar qualquer usuário e jogo.

---

## Tecnologias e Arquitetura

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **JWT (Json Web Token)**
- **Swagger/OpenAPI**
- **xUnit + Moq**
- **Arquitetura em Camadas (Clean Architecture + DDD)**

---

## Rodando Localmente

**Pré-requisitos:**
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server (ou ajuste a connection string)
- Ferramenta como Postman ou Swagger

### Passos

```bash
git clone https://github.com/seu-usuario/fcg-api.git
cd fcg-api
```

Configure o `appsettings.json`:

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

```bash
dotnet ef migrations add "FirstMigration"
dotnet ef database update
dotnet run --project FCG.API
```

Acesse o Swagger:
```
http://localhost:{porta}/swagger
```

---

## Autenticação e Autorização

- JWT com roles (`User`, `Admin`)
- Proteção por `[Authorize(Roles="")]`
- Gere o token no endpoint de login e use no Swagger clicando em "Authorize"

---

## Exemplos de Endpoints

### Autenticação
```
POST /api/auth/login
```

### Usuários
```
POST /api/users
GET /api/users/{id}
PUT /api/users/{id}
DELETE /api/users/{id}
```

### Jogos
```
GET /api/games
POST /api/games
PUT /api/games/{id}
DELETE /api/games/{id}
```

### Biblioteca Pessoal
```
POST /api/usergames/games/{gameId}
GET /api/usergames/games
DELETE /api/usergames/games/{gameId}
```

### Admin
```
GET /api/Admin/{userId}/games
PUT /api/Admin/promote/{id}
PUT /api/admin/users/{id}
POST /api/Admin/{userId}/games/{gameId}
DELETE /api/Admin/users/{userId}/games/{gameId}
DELETE /api/Admin/{id}
```

---

## Estrutura de Pastas

```bash
FCG.API/               -> Camada de apresentação (Controllers, Middlewares)
FCG.Application/       -> Casos de uso (Use Cases, Handlers, Services)
FCG.Domain/            -> Entidades, ValueObjects, Interfaces
FCG.Infra/             -> Repositórios e DataContext (EF Core)
FCG.Tests/             -> Testes unitários com xUnit + Moq
```

---

## Funcionalidades

- Cadastro, login, atualização e exclusão de usuários
- Adicionar, atualizar, adquirir e excluir jogos
- Visualizar/remover jogos da biblioteca pessoal
- Promoção e gerenciamento de usuários por administradores
- Autenticação JWT e autorização por perfil
- Swagger com documentação automática
- Tratamento global de exceções via middleware
- Testes de unidade com TDD em casos críticos

---

## Qualidade de Software

Este projeto aplica **Test-Driven Development (TDD)** em diversos casos de uso importantes, com testes unitários escritos com xUnit e Moq:

- `UserCreationService` – cadastro com promoção de admin
- `GameCreationService` – criação validada de jogos
- `UserGamePurchaseService` – regras de compra com verificação
- `LoginUserHandler` – autenticação via e-mail/senha

Os testes cobrem cenários positivos e negativos com foco nas regras de negócio.

---

## Design e Arquitetura

- Clean Architecture com separação clara por camadas
- DDD aplicado com Entities, Value Objects e Services
- Handlers por Use Case centralizando lógica de aplicação
- Value Objects com validações robustas (`Email`, `Password`, `Title`, `Price`)
- Middleware para tratamento global de exceções
- Interface e serviços de autenticação desacoplados (`ITokenService`, `IUserAuthenticationService`)

---

## Documentação

[Documentação PDF](https://github.com/devjoaomelo/fiap-cloud-games/blob/main/Documentacao_FIAP_Cloud_Games.pdf)

---

## Autor

MVP desenvolvido para a segunda etapa da Tech Challenge FIAP  
**Aluno:** João Vitor Gonçalves de Melo
