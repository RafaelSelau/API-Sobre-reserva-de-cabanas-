# API de Reserva de Cabanas

## Vídeo da apresentação

[🎥 Assistir ao vídeo da apresentação](https://drive.google.com/file/d/1-XAqPWscxndzbCum5kGNHDnvuLQJIa49/view?usp=sharing)

## Tecnologias utilizadas

- C#
- .NET 10
- ASP.NET Core (Minimal API)
- Postman / Bruno

## Endpoints

### GET
Listar todas as cabanas:

`GET /api/cabanas`

### GET por ID
Buscar cabana por ID:

`GET /api/cabanas/{id}`

### POST
Cadastrar nova cabana:

`POST /api/cabanas`

### PUT
Atualizar cabana por ID:

`PUT /api/cabanas/{id}`

### DELETE
Remover cabana por ID:

`DELETE /api/cabanas/{id}`

---

## Como Executar o Projeto

1. Certifique-se de ter o SDK do .NET instalado:
   ```bash
   dotnet --version
   ```

2. Na pasta raiz do projeto, execute:
   ```bash
   dotnet run
   ```

3. A API estará disponível em `http://localhost:5000` (ou na porta configurada).

---

## Coleção do Postman

A Collection do Postman está disponível na pasta [`Collection/`](./Collection):
- [`Collection/ApiCabanas.postman_collection.json`](./Collection/ApiCabanas.postman_collection.json)

### Como importar no Postman:
1. Abra o **Postman**.
2. Clique no botão **Import**.
3. Escolha o arquivo `ApiCabanas.postman_collection.json` localizado na pasta `Collection`.
4. Execute as requisições preparadas.
