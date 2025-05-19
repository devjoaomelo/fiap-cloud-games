# ✅ Tech Challenge FIAP – Checklist Geral

## 🌱 Parte 1 – Estrutura Inicial do Projeto

- [x] Criar repositório no GitHub
- [x] Definir estrutura de pastas baseada em DDD
- [x] Criar solução e projeto `.NET 8` (monolito)
- [x] Configurar `.gitignore` (VisualStudio + .NET)
- [x] Criar pastas: `src/FCG.Domain`, `src/FCG.Application`, `src/FCG.Infra`, `src/FCG.API`
- [x] Configurar dependências iniciais no `.csproj`
- [x] Commit inicial com estrutura e README básico

---

## 🧠 Parte 2 – Camada de Domínio (`FCG.Domain`)

### 🧱 Entidades e Value Objects
- [x] Criar entidade `User` (`Entities/User.cs`)
- [x] Criar `enum Profile` (`Enums/Profile.cs`)
- [x] Criar `ValueObject` `Email` (`ValueObjects/Email.cs`)
- [x] Criar `ValueObject` `Password` (`ValueObjects/Password.cs`)

### 📂 Repositórios e Interfaces
- [x] Criar interface `IUserRepository` (`Interfaces/IUserRepository.cs`)

### 💡 Casos de Uso (opcional)
- [ ] Criar pasta `UseCases/Users/`
- [ ] Implementar `RegisterUserUseCase`, `LoginUserUseCase`, etc.

---

## 🧱 Parte 3 – Camada de Infraestrutura (`FCG.Infra`)

- [x] Criar DbContext (`FCGDbContext.cs`)
- [x] Mapear entidade `User` com Value Objects e Enum (`UserConfiguration.cs`)
- [x] Implementar `UserRepository` com EF Core
- [x] Configurar `DbContext` na `Startup` (injeção de dependência)
- [ ] Aplicar e testar a primeira migration
- [ ] Validar persistência dos dados no banco

---

## 🌐 Parte 4 – Camada de API (`FCG.API`)

- [ ] Criar projeto `FCG.API`
- [ ] Implementar endpoints com `Controllers` ou `Minimal API`
- [ ] Adicionar autenticação JWT
- [ ] Implementar login e cadastro de usuários
- [ ] Criar middleware de erros e logs
- [ ] Adicionar Swagger e configurar documentação
- [ ] Versionamento da API (opcional)

---

## 🧪 Parte 5 – Testes e Qualidade

- [ ] Criar projeto de testes `FCG.Tests`
- [ ] Escrever testes unitários para `User`, `Email`, `Password`
- [ ] Aplicar TDD ou BDD em pelo menos um módulo
- [ ] Cobertura mínima para repositórios e serviços

---

## 📊 Parte 6 – Entregáveis

- [ ] Documentação de DDD (Event Storming no Miro ou equivalente)
- [ ] Vídeo de até 15 minutos demonstrando o projeto
- [ ] README.md completo no repositório
- [ ] Relatório de entrega com:
  - Nome do grupo
  - Participantes e usernames Discord
  - Links para:
    - Documentação
    - Repositório
    - Vídeo
