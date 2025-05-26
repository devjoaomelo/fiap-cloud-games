# ✅ Checklist de Desenvolvimento do Projeto FCG

## 🟢 Parte 1 – Estrutura inicial do projeto
- [x] Criar solução e projetos (`FCG.API`, `FCG.Domain`, `FCG.Application`, `FCG.Infra`)
- [x] Organizar estrutura de pastas no padrão DDD
- [x] Criar `.gitignore` e configurar repositório no GitHub
- [x] Configurar DI básico e arquivos de configuração

## 🟢 Parte 2 – Camada Domain
- [x] Criar pastas: `Entities`, `ValueObjects`, `Enums`, `Interfaces`, `UseCases`
- [x] Criar entidade `User`
- [x] Criar `ValueObjects` para `Email` e `Password`
- [x] Implementar validações nas entidades/VOs
- [x] Criar `IUserRepository`

## 🟢 Parte 3 – Camada Infra
- [x] Criar pasta `Configurations` com mapeamento do `User` via Fluent API
- [x] Criar `FCGDbContext` e aplicar configurações
- [x] Adicionar EF Core e Pomelo (ajustar para MySQL)
- [x] Configurar conexão no `Program.cs`
- [x] Criar e aplicar a primeira migration
- [x] Criar classe de DesignTime para suportar CLI (`FCGDbContextFactory`)

## 🔜 Parte 4 – Camada Application (Use Cases)
- [x] Criar pasta `UseCases/Users`
- [x] Implementar casos de uso (`CreateUser`, `GetUserById`, etc)
- [x] Criar interfaces e classes para serviços e handlers
- [ ] Aplicar injeção de dependência para os casos de uso

## 🔜 Parte 5 – Camada API
- [x] Criar `DTOs` para entrada e saída
- [x] Criar `Controllers`
- [x] Mapear rotas e endpoints (Minimal ou Controller)
- [ ] Configurar autenticação JWT
- [ ] Configurar Swagger

## 🔜 Extras / Futuro
- [ ] Implementar testes unitários para domínio e use cases
- [ ] Criar testes de integração
- [ ] Publicar documentação da API
- [ ] Deploy em ambiente de testes (opcional)
"""

# Caminho para salvar o arquivo
checklist_path = Path("/mnt/data/Checklist_Projeto_FCG.md")
checklist_path.write_text(checklist_md.strip(), encoding="utf-8")

checklist_path.name
