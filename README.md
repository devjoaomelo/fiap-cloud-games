# FIAP Cloud Games (FCG) – **Fase 2**

![ci](https://github.com/devjoaomelo/fiap-cloud-games/actions/workflows/ci.yml/badge.svg)
![cd](https://github.com/devjoaomelo/fiap-cloud-games/actions/workflows/deploy.yml/badge.svg)

API RESTful para gerenciamento de usuários e biblioteca de jogos digitais – **agora containerizada, escalável e monitorada na Azure**.

---
## Descrição

O FCG é um MVP que simula uma plataforma de venda de jogos. Usuários podem se registrar, comprar jogos e gerenciar sua biblioteca; administradores podem gerenciar qualquer usuário e jogo.

A **Fase 2** concentrou‑se em _Deploy_, _Cloud_ e _Observabilidade_, mantendo o domínio da Fase 1 intacto.

---
## Tecnologias & Ferramentas

- **.NET 8** • ASP.NET Core Web API  
- **Entity Framework Core 8**  
- **JWT** (Json Web Token)  
- **Docker + Docker Compose**  
- **Azure Container Registry** • **Azure Container Apps**  
- **Azure Database for MySQL**  
- **Application Insights**  
- **Grafana** (Azure Monitor datasource)  
- **GitHub Actions CI/CD**  
- **xUnit + Moq**  
- **Clean Architecture + DDD**

---
## Arquitetura
Fluxo: Código -> Github actions -> Docker
build and push -> ACR -> Azure db mysql -
> Application insights -> Grafana

---
## Rodando Localmente

### Opção A – .NET direto (desenvolvimento)

```bash
# Pré‑requisitos: .NET 8 SDK + SQL Server

git clone https://github.com/devjoaomelo/fiap-cloud-games.git
cd fiap-cloud-games

# ajuste appsettings.json e gere o banco

dotnet ef database update

dotnet run --project FCG.API
# Swagger em http://localhost:5050/swagger
```

### Opção B – Docker Compose (produção like)

```bash
# Pré‑requisito: Docker Desktop

docker compose up --build
# API em http://localhost:8080/swagger
```

Compose sobe API + MySQL já configurado; variáveis em `docker-compose.yaml`.

---
## CI / CD

- **CI** (`.github/workflows/ci.yml`)  
  `restore ➜ build ➜ test ➜ publish‑artifact`  
  Falha em testes bloqueia merge.
- **CD** (`.github/workflows/cd.yml`)  
  `docker build ➜ push ➜ az login ➜ az containerapp update`  
  Tag da imagem = _commit SHA_. Deploy sem downtime em ~4 min.

---
## Monitoramento

<img width="1849" height="836" alt="{3900E6BA-A495-4CEF-B545-75D88BD323CF}" src="https://github.com/user-attachments/assets/ab5e01c3-4028-426e-9141-5850a86d072d" />


Métricas no Grafana:
- Disponibilidade
- Usage
- Requests por minuto
- Latência média
- CPU & Memória por réplica
- **HTTP Status Codes** em pizza

---
## Autenticação & Autorização

JWT com roles (`User`, `Admin`). Gere o token em `/api/auth/login` e clique **Authorize** no Swagger.

---
## Endpoints Principais

```text
POST   /api/auth/login               # login
POST   /api/users                    # criar usuário
GET    /api/games                    # listar jogos
POST   /api/usergames/games/{id}     # comprar jogo
... (ver Swagger)
```

---
## Qualidade de Software

TDD com xUnit + Moq. Serviços cobertos:
- UserCreationService  
- GameCreationService  
- UserGamePurchaseService  
- LoginUserHandler

---
## Estrutura de Pastas

```text
src/
  FCG.API/          # Controllers, Middlewares
  FCG.Application/  # Use Cases, Handlers
  FCG.Domain/       # Entities, ValueObjects
  FCG.Infra/        # EF Core, Repositórios
  FCG.Tests/       # xUnit
.github/
  -workflows/
    -ci.yml
    -deploy.yml
    -docker-publish.yml
.env
dockerfile
docker-compose.yaml
```
# Documentação
[Link da documentação](https://github.com/devjoaomelo/fiap-cloud-games/blob/main/Documentacao_FIAP_Cloud_Games.pdf)

---
## Licença
MIT.
---
Projeto por **João Vitor Gonçalves de Melo** para o Tech Challenge FIAP 2025.
