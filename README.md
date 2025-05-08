# Agenda Pessoal

Sistema de Agenda Pessoal construído com ASP.NET Core 8, Entity Framework Core e PostgreSQL.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (versão 13 ou superior)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) com suporte a ASP.NET e EF Core
- [Postman](https://www.postman.com/downloads/) (opcional, para testar a API)

---

## 1. Instalando o PostgreSQL

1. Baixe e instale o PostgreSQL:
   - [Download PostgreSQL](https://www.postgresql.org/download/)

2. Durante a instalação, defina uma senha para o usuário `postgres`.
   - **Importante**: não se esqueça  da senha escolhida, ela será usada para a conexão do programa com o database.

3. Após a instalação, abra o **pgAdmin** e crie o database AgendaDb.

---

## 2. Editando o projeto

1. No [appsettings.json](https://github.com/Felipera02/agenda-serial3-remastered/blob/main/appsettings.json), altere "suasenha123" pela senha que foi definida no postgres.
2. No terminal da solução do programa, rode o seguinte comando: `dotnet ef database update`
3. Para testar o programa, importe a [coleção do Postman](https://drive.google.com/file/d/1yIARTyy7ypbl9VlfISpdOr9lXCsw_jrQ/view?usp=sharing) feita pela equipe Serial3.
