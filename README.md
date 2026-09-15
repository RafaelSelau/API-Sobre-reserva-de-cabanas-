# API de Reserva de Cabanas (AP1 - ASP.NET Core .NET 10)

## Nome, Tema e Objetivo
* **Nome do Projeto**: `ApiCabanas`
* **Tema**: Sistema de gerenciamento e reserva de cabanas turísticas.
* **Objetivo**: Disponibilizar uma interface Web API (Minimal API) em .NET 10 para cadastro, consulta, atualização e remoção de cabanas. A aplicação opera de forma totalmente em memória (`List<T>`), permitindo simulações de CRUD completas sem dependência de banco de dados externo ou bibliotecas adicionais.

---

## Requisitos
* **SDK do .NET**: .NET 10 (`net10.0`).
* **Ferramenta de testes de API**: [Bruno](https://www.usebruno.com/downloads) (ou Postman/Insomnia).

> [!WARNING]
> **Aviso Importante sobre Persistência de Dados**: Os registros são mantidos exclusivamente em memória em uma `List<Cabana>`. Ao reiniciar ou encerrar a aplicação, as alterações serão descartadas e a lista retornará ao estado inicial contendo as cabanas pré-carregadas.

---

## Como Executar o Projeto

1. Certifique-se de que o SDK do .NET 10 esteja instalado em seu ambiente:
   ```bash
   dotnet --version
   ```

2. Na pasta raiz do projeto, execute a aplicação utilizando o parâmetro de porta fixa para os testes:
   ```bash
   dotnet run --urls http://localhost:5050
   ```

3. Para apenas validar se o projeto compila sem erros:
   ```bash
   dotnet build
   ```

* **URL Local de Testes**: `http://localhost:5050`

---

## Endpoints da API

| Método | Rota | Descrição | Código HTTP de Sucesso | Código HTTP de Erro |
| :--- | :--- | :--- | :--- | :--- |
| **GET** | `/` | Rota raiz informando o status da API no ar | `200 OK` | - |
| **GET** | `/api/cabanas` | Lista todas as cabanas cadastradas | `200 OK` | - |
| **GET** | `/api/cabanas/{id}` | Busca os dados de uma cabana específica por ID | `200 OK` | `404 Not Found` |
| **POST** | `/api/cabanas` | Cadastra uma nova cabana | `201 Created` | `400 Bad Request` |
| **PUT** | `/api/cabanas/{id}` | Atualiza todos os dados de uma cabana existente | `200 OK` | `404 Not Found` |
| **DELETE** | `/api/cabanas/{id}` | Remove uma cabana cadastrada por ID | `204 No Content` | `404 Not Found` |

---

## Exemplos de Body em JSON

### POST `/api/cabanas` (DTO de Entrada - Sem `Id`)
```json
{
  "nome": "Cabana Recanto dos Pinheiros",
  "localizacao": "Monte Verde, MG",
  "precoPorNoite": 520.00,
  "capacidadeHospedes": 5,
  "disponivel": true
}
```

### PUT `/api/cabanas/{id}` (DTO de Entrada - Sem `Id`)
```json
{
  "nome": "Cabana Recanto dos Pinheiros Premium",
  "localizacao": "Monte Verde, MG",
  "precoPorNoite": 580.00,
  "capacidadeHospedes": 6,
  "disponivel": true
}
```

---

## Testes com o Bruno

A Collection do Bruno utilizada nos testes está salva no diretório [`/bruno`](./bruno) na raiz do repositório.

### Como carregar no Bruno:
1. Abra o **Bruno**.
2. Clique em **Open Collection** (ou `+` -> **Open Collection**).
3. Selecione a pasta `bruno` presente neste repositório.
4. Selecione o ambiente **Local** (canto superior direito do Bruno) para utilizar a `baseUrl` setada em `http://localhost:5050`.

### Sequência de Requisições:
1. `00 - API no ar` (`GET {{baseUrl}}/`)
2. `01 - Listar` (`GET {{baseUrl}}/api/cabanas`)
3. `02 - Buscar por id` (`GET {{baseUrl}}/api/cabanas/1`)
4. `03 - Cadastrar` (`POST {{baseUrl}}/api/cabanas`)
5. `04 - Atualizar` (`PUT {{baseUrl}}/api/cabanas/3`)
6. `05 - Remover` (`DELETE {{baseUrl}}/api/cabanas/3`)
7. `06 - Confirmar remocao` (`GET {{baseUrl}}/api/cabanas/3`)

---

## Comandos Git para Envio ao GitHub

Caso vá publicar este repositório no GitHub:

```bash
git init
git add .
git commit -m "AP1: API REST de Reserva de Cabanas em .NET 10"
git branch -M main
git remote add origin https://github.com/SEU-USUARIO/SEU-REPOSITORIO.git
git push -u origin main
```

---

## Vídeo de Demonstração

> [!IMPORTANT]
> Cole aqui o link público do vídeo gravado demonstrando a execução da API e a suíte de testes no Bruno/Postman.

* **Link do vídeo público**: `[INSIRA O LINK PÚBLICO AQUI]`
