# 🧘‍♀️ WorkZen — Bem-estar Mental no Trabalho

API desenvolvida para a **Global Solution 2025 (4º semestre)**, criada com foco em bem-estar digital, permitindo o gerenciamento completo de meditações, sessões e integração com testes automatizados.

> A aplicação utiliza **.NET 8**, **EF Core**, **SQLite**, versionamento de API e **testes de integração com xUnit + SQLite InMemory**.

---

## 👩‍💻 Integrantes do Grupo

| Nome | RM | GitHub |
|------|------|---------|
| **Barbara Dias** | 556974 | https://github.com/bahdiaz |
| **Beatriz Ferreira Cruz** | 555698 | https://github.com/BeatrizFerreira01 |
| **Natasha Lopes** | 554816 | https://github.com/natahalopees1 |

---

# 🚀 Tecnologias Utilizadas

- .NET 8  
- ASP.NET Core Web API  
- Entity Framework Core (EF Core 8)  
- SQLite  
- Swagger / OpenAPI  
- API Versioning  
- xUnit  
- WebApplicationFactory  
- SQLite InMemory para testes  
- HATEOAS básico  
- Health Checks  

---

# 📁 Estrutura do Projeto

```
WorkZen/
│
├── Screenshots/
│
├── WorkZen.Api/                     # Projeto principal
│   ├── Controllers/
│   ├── DTOs/
│   ├── Entities/
│   ├── Services/
│   ├── Infrastructure/
│   ├── Data/
│   ├── Migrations/
│   ├── Program.cs
│   └── appsettings.json
│
├── WorkZen.Api.Tests.Integration/   # Testes de integração
│   ├── WorkZenApiFactory.cs
│   └── UnitTest1.cs
│
└── README.md
└── WorkZen.sln
```

---

# 📦 Arquitetura

A API segue uma arquitetura simples organizada por pastas:

- **Controllers** → Endpoints  
- **DTOs** → Objetos de entrada/saída  
- **Entities** → Entidades do EF Core  
- **Services** → Lógica de domínio  
- **Infrastructure** → Filtros, Swagger, config extra  
- **Data** → DbContext  
- **Tests** → Testes de integração usando WebApplicationFactory  

---

# 🛠️ Como Rodar o Projeto

### 1️⃣ Restaurar dependências

```bash
dotnet restore
```

### 2️⃣ Entrar na pasta principal

```bash
cd WorkZen.Api
```

### 3️⃣ Criar migrações e atualizar o banco

```bash
dotnet ef migrations add Initial
dotnet ef database update
```

### 4️⃣ Rodar a API

```bash
dotnet run
```

### 5️⃣ Abrir o Swagger

👉 http://localhost:5291/swagger  
👉 http://localhost:5291/swagger/v1/swagger.json

---

# 🔍 Endpoints Principais (v1)

### ✔️ `GET /api/v1/meditations`
Lista paginada de meditações.

### ✔️ `GET /api/v1/meditations/{id}`
Retorna uma meditação específica.

### ✔️ `POST /api/v1/meditations`

#### Exemplo de JSON:
```json
{
  "title": "Meditação de Teste",
  "description": "Criada pelo teste",
  "category": "Mindfulness",
  "durationMinutes": 10,
  "isPremium": false
}
```

### ✔️ `PUT /api/v1/meditations/{id}`
Atualiza meditação existente.

### ✔️ `DELETE /api/v1/meditations/{id}`
Remove uma meditação.

---

# ❤️ Health Check

Disponível em:

👉 http://localhost:5291/health

Retorno esperado:

```
Healthy
```

---

# 🧪 Testes de Integração

### ✔️ Como rodar

```bash
cd WorkZen.Api.Tests.Integration
dotnet test
```

Os testes utilizam:

- Banco **SQLite InMemory**
- `EnsureCreated()` para construir o schema automaticamente
- `WebApplicationFactory` para subir a API em memória

### Teste implementado

- Criar uma meditação  
- Validar status HTTP 201  
- Validar conteúdo retornado  

---

# 📸 Screenshots

WorkZen/
├── WoekZen.Api/
├── WoekZen.Api.Tests.Integration/
├── Screenshots/
│   ├── swagger.png    # Tela do Swagger
│   ├── health.png     # Health Check
│   ├── tests-success.png    # Testes passando
│   ├── tests-success_2.png  # Testes passando
└── README.md

```md
![Swagger](screenshots/swagger.png)
![Health](screenshots/health.png)
![Testes](screenshots/tests-success.png)
![Testes](screenshots/test-success_2.png)
```

---

# ✔️ Requisitos Atendidos

| Requisito | Atendido |
|----------|----------|
| CRUD de meditações | ✅ |
| Banco com migrations | ✅ |
| Health Check funcionando | ✅ |
| Testes de integração com xUnit | ✅ |
| Uso de SQLite + InMemory | ✅ |
| Versionamento de API | ✅ |
| Swagger documentado | ✅ |
| README completo | ✅ |

---
