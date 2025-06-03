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
   
2. Aplicar migrations:
   ```bash
   dotnet ef migrations add "FirstMigration"
   dotnet ef database update
   
3. Rode a aplicação
   ```bash
   dotnet run --project FCG.API
4. Acesse o Swagger
   ```bash
   http://localhost:{porta}/swagger
