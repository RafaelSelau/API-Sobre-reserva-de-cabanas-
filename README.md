# API de Reserva de Cabanas

Sistema de gerenciamento de cabanas para hospedagem, com backend em ASP.NET Core, persistência via Entity Framework Core + SQLite e frontend integrado.

---

## Vídeo de demonstração — AP2

[🎥 Assistir ao vídeo da AP2](https://drive.google.com/drive/folders/1O5giF3Bto_Sd7zcZuD-uClVs-diZ9C6Q?usp=drive_link)

---

## Vídeo da apresentação — AP1

[🎥 Assistir ao vídeo da AP1](https://drive.google.com/file/d/1-XAqPWscxndzbCum5kGNHDnvuLQJIa49/view?usp=sharing)

---

## Objetivo do Projeto

API REST para cadastro, listagem, edição e exclusão de cabanas disponíveis para hospedagem. O projeto evoluiu da AP1 (lista em memória) para a AP2 (persistência real com SQLite via Entity Framework Core), com frontend integrado servido pela mesma origem da API.

---

## Tecnologias Utilizadas

| Tecnologia                         | Finalidade                              |
|------------------------------------|-----------------------------------------|
| C# / .NET 10                       | Linguagem e plataforma                  |
| ASP.NET Core — Minimal API         | Backend e roteamento HTTP               |
| Entity Framework Core 9            | ORM e acesso ao banco de dados          |
| SQLite                             | Banco de dados relacional local         |
| HTML + CSS + JavaScript (Vanilla)  | Interface web (frontend)                |
| Bruno / Postman                    | Testes de API                           |

---

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- `dotnet-ef` (ferramenta global — veja instruções abaixo)
- Git

---

## Estrutura de Pastas

```
API Sobre reserva de cabanas/
├── Models/
│   └── Cabana.cs             # Entidade principal
├── Data/
│   └── AppDbContext.cs       # DbContext do EF Core
├── Migrations/               # Migrations versionadas
├── wwwroot/
│   └── index.html            # Frontend integrado
├── Properties/
│   └── launchSettings.json   # Porta 5050
├── bruno/                    # Collection do Bruno
├── Collection/
│   └── ApiCabanas.postman_collection.json
├── Program.cs                # Configuração e rotas
├── ApiCabanas.csproj
├── appsettings.json
├── .gitignore
└── README.md
```

---

## Como Executar o Projeto

### 1. Clonar o repositório

```bash
git clone <url-do-repositorio>
cd "API Sobre reserva de cabanas"
```

### 2. Restaurar os pacotes

```bash
dotnet restore
```

### 3. Instalar a ferramenta EF Core (se ainda não instalada)

```bash
dotnet tool install --global dotnet-ef
```

### 4. Criar o banco de dados SQLite

```bash
dotnet ef database update
```

> O arquivo `cabanas.db` será criado na raiz do projeto com a tabela `Cabanas` pronta.

### 5. Executar a aplicação

```bash
dotnet run
```

### 6. Acessar a interface

Abra o navegador em: **http://localhost:5050**

A API também estará acessível em: **http://localhost:5050/api/cabanas**

---

## Endpoints da API

| Método   | Rota                   | Descrição                          | Código HTTP      |
|----------|------------------------|------------------------------------|------------------|
| `GET`    | `/`                    | Verifica se a API está no ar       | 200 OK           |
| `GET`    | `/api/cabanas`         | Lista todas as cabanas             | 200 OK           |
| `GET`    | `/api/cabanas/{id}`    | Busca uma cabana por ID            | 200 / 404        |
| `POST`   | `/api/cabanas`         | Cadastra uma nova cabana           | 201 Created      |
| `PUT`    | `/api/cabanas/{id}`    | Atualiza uma cabana existente      | 200 / 404        |
| `DELETE` | `/api/cabanas/{id}`    | Remove uma cabana                  | 204 / 404        |

---

## Exemplos de Uso

### POST — Cadastrar cabana

```http
POST http://localhost:5050/api/cabanas
Content-Type: application/json

{
  "nome": "Cabana Vista da Serra",
  "localizacao": "Campos do Jordão, SP",
  "precoPorNoite": 450.00,
  "capacidadeHospedes": 4,
  "disponivel": true
}
```

**Resposta:** `201 Created`

```json
{
  "id": 1,
  "nome": "Cabana Vista da Serra",
  "localizacao": "Campos do Jordão, SP",
  "precoPorNoite": 450.00,
  "capacidadeHospedes": 4,
  "disponivel": true
}
```

---

### GET — Listar todas as cabanas

```http
GET http://localhost:5050/api/cabanas
```

**Resposta:** `200 OK` — array de cabanas.

---

### GET por ID

```http
GET http://localhost:5050/api/cabanas/1
```

**Resposta:** `200 OK` ou `404 Not Found`.

---

### PUT — Atualizar cabana

```http
PUT http://localhost:5050/api/cabanas/1
Content-Type: application/json

{
  "nome": "Cabana Vista da Serra Premium",
  "localizacao": "Campos do Jordão, SP",
  "precoPorNoite": 600.00,
  "capacidadeHospedes": 6,
  "disponivel": true
}
```

**Resposta:** `200 OK` ou `404 Not Found`.

---

### DELETE — Remover cabana

```http
DELETE http://localhost:5050/api/cabanas/1
```

**Resposta:** `204 No Content` ou `404 Not Found`.

---

## Testes — Bruno e Postman

### Bruno (recomendado)

A pasta `bruno/` contém a collection completa:

1. Abra o **Bruno**
2. Clique em **Open Collection**
3. Selecione a pasta `bruno/` deste projeto
4. Ative o ambiente `Local` (usa `http://localhost:5050`)
5. Execute as requisições na ordem (00 a 06)

**Requisições incluídas:**

| #  | Nome                   | Método   |
|----|------------------------|----------|
| 00 | API no ar              | GET      |
| 01 | Listar                 | GET      |
| 02 | Buscar por id          | GET      |
| 03 | Cadastrar              | POST     |
| 04 | Atualizar              | PUT      |
| 05 | Remover                | DELETE   |
| 06 | Confirmar remoção (404)| GET      |

### Postman

1. Abra o **Postman**
2. Clique em **Import**
3. Selecione `Collection/ApiCabanas.postman_collection.json`
4. A variável `baseUrl` já está configurada para `http://localhost:5050`
5. Execute as requisições em ordem

---

## Persistência dos Dados

- Os dados são armazenados no arquivo **`cabanas.db`** (SQLite) na raiz do projeto.
- O banco é criado automaticamente na inicialização (`db.Database.Migrate()`).
- Os registros **persistem entre reinicializações** da aplicação.
- O arquivo `cabanas.db` está no `.gitignore` e **não é versionado** — cada desenvolvedor cria o seu banco localmente.
- As **migrations** estão versionadas na pasta `Migrations/` e garantem que qualquer pessoa consiga recriar o banco com `dotnet ef database update`.

---

## Comandos Úteis

```bash
# Restaurar pacotes
dotnet restore

# Compilar
dotnet build

# Criar banco de dados (migrations)
dotnet ef database update

# Executar a aplicação
dotnet run

# Adicionar nova migration (somente se alterar o Model)
dotnet ef migrations add NomeDaMigration
```

---

## Roteiro do Vídeo — AP2

1. **Repositório:** Mostre o repositório da AP1 e explique as mudanças para a AP2.
2. **Estrutura:** Mostre as pastas `Models/`, `Data/`, `Migrations/`, `wwwroot/`, `bruno/` e `Collection/`.
3. **Iniciar:** Execute `dotnet run` no terminal e abra `http://localhost:5050` no navegador.
4. **Código:** Explique brevemente `Cabana.cs` (Model), `AppDbContext.cs` (DbContext), `appsettings.json` (connection string SQLite) e as rotas em `Program.cs`.
5. **Bruno/Postman:** Execute POST → GET da coleção → GET por ID → PUT → DELETE, mostrando os códigos HTTP retornados.
6. **404:** Consulte o ID que foi excluído e demonstre o retorno `404 Not Found`.
7. **Frontend:** Cadastre, liste, edite e exclua uma cabana pela interface do navegador.
8. **Confirmação via API:** Depois de criar um registro pelo frontend, confirme no Bruno/Postman que aparece no GET.
9. **Reinicialização:** Pare a API (`Ctrl+C`), reinicie com `dotnet run` e mostre que os registros anteriores ainda estão disponíveis.
10. **Vídeo link:** Após gravar, atualize este README com o link público.
11. **Commit final:** Revise com `git status`, adicione com `git add .` e envie com `git commit -m "AP2: EF Core + SQLite + frontend integrado"` e `git push`.
