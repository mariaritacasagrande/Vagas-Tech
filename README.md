# 💼 Vagas Tech

Projeto desenvolvido em **squad** como desafio prático final do módulo **Banco de Dados e Persistência de Dados**, da **WoMakersCode**, sob orientação da instrutora **Paula Luiza**.

A proposta do projeto é desenvolver a camada de persistência de dados de uma plataforma de empregos chamada **Vagas Tech**, criada para conectar empresas que possuem vagas afirmativas a talentos femininos da área de tecnologia.

---

## 🎯 Objetivo

Aplicar os conhecimentos desenvolvidos durante o módulo na construção de uma aplicação em **C# integrada a um banco de dados SQLite**, utilizando **ADO.NET** e comandos SQL.

O projeto contempla:

* criação e estruturação de banco de dados;
* tabelas relacionais;
* chaves primárias e estrangeiras;
* relacionamento Muitos-para-Muitos (N:N);
* tabela associativa;
* comandos SQL;
* operações CRUD;
* consultas utilizando `INNER JOIN`;
* integração entre C# e SQLite;
* persistência dos dados.

---

## 🛠️ Tecnologias utilizadas

* **C#**
* **.NET 8**
* **SQLite**
* **ADO.NET**
* **Microsoft.Data.Sqlite 8.0.11**
* **Google Colab**
* **DBeaver**
* **GitHub**

---

## 🗄️ Banco de Dados

O sistema utiliza um banco de dados SQLite chamado:

```text
vagastech.db
```

O banco possui três tabelas.

### VAGAS

Armazena as vagas disponíveis.

| Campo   | Tipo         | Restrição              |
| ------- | ------------ | ---------------------- |
| ID_VAGA | INTEGER      | PRIMARY KEY / NOT NULL |
| TITULO  | VARCHAR(100) | NOT NULL               |
| EMPRESA | VARCHAR(100) | NOT NULL               |
| SALARIO | DECIMAL      | NOT NULL               |

### CANDIDATAS

Armazena os dados das candidatas.

| Campo        | Tipo         | Restrição              |
| ------------ | ------------ | ---------------------- |
| ID_CANDIDATA | INTEGER      | PRIMARY KEY / NOT NULL |
| NOME         | VARCHAR(100) | NOT NULL               |
| EMAIL        | VARCHAR(100) | NOT NULL               |

### CANDIDATURAS

Tabela associativa que relaciona candidatas e vagas, resolvendo o relacionamento **Muitos-para-Muitos (N:N)**.

| Campo          | Tipo     | Restrição              |
| -------------- | -------- | ---------------------- |
| ID_CANDIDATURA | INTEGER  | PRIMARY KEY / NOT NULL |
| DATA_ENVIO     | DATETIME | NOT NULL               |
| ID_VAGA        | INTEGER  | FOREIGN KEY / NOT NULL |
| ID_CANDIDATA   | INTEGER  | FOREIGN KEY / NOT NULL |

### Relacionamentos

```text
VAGAS
  │
  │ ID_VAGA
  ▼
CANDIDATURAS
  ▲
  │ ID_CANDIDATA
  │
CANDIDATAS
```

A tabela `CANDIDATURAS` funciona como tabela associativa entre `VAGAS` e `CANDIDATAS`.

---

## 📁 Estrutura da aplicação

O projeto foi desenvolvido como uma aplicação de console em C#:

```text
VagasTechApp
│
├── Program.cs
├── MetodosCRUD.cs
└── VagasTechApp.csproj
```

O arquivo `MetodosCRUD.cs` concentra os métodos responsáveis pelas operações de persistência.

---

## 🔄 Operações CRUD

As funcionalidades do CRUD foram distribuídas entre as integrantes da squad.

### CREATE

Foram implementados os métodos:

* `CadastrarVaga()`
* `CadastrarCandidata()`
* `EnviarCandidatura()`

Esses métodos utilizam comandos `INSERT` para inserir os dados no banco.

### READ — Double JOIN

Foi implementado o método:

```text
ConsultarCandidaturas()
```

A consulta utiliza **dois `INNER JOIN`** para relacionar as três tabelas:

```text
CANDIDATURAS
      │
      ├── CANDIDATAS
      │
      └── VAGAS
```

A consulta retorna:

* nome da candidata;
* e-mail da candidata;
* título da vaga;
* empresa de destino.

SQL utilizado:

```sql
SELECT
    CANDIDATAS.NOME,
    CANDIDATAS.EMAIL,
    VAGAS.TITULO,
    VAGAS.EMPRESA
FROM CANDIDATURAS
INNER JOIN CANDIDATAS
    ON CANDIDATURAS.ID_CANDIDATA = CANDIDATAS.ID_CANDIDATA
INNER JOIN VAGAS
    ON CANDIDATURAS.ID_VAGA = VAGAS.ID_VAGA;
```

### UPDATE

Foi implementado o método:

```text
AtualizarSalarioVaga()
```

Esse método utiliza `UPDATE` para alterar o salário de uma vaga existente.

### DELETE

Foi implementado o método:

```text
CancelarCandidatura()
```

Esse método utiliza `DELETE` para excluir uma candidatura pelo seu ID.

---

## 👩‍💻 Contribuição no projeto

O desenvolvimento foi realizado de forma colaborativa entre as integrantes da squad, com as operações do CRUD distribuídas entre as participantes.

### READ — Consultar Candidaturas

**Responsável: Silvia**

A contribuição consistiu na implementação da funcionalidade:

```text
ConsultarCandidaturas()
```

O método realiza uma consulta SQL utilizando **dois `INNER JOIN`**, permitindo apresentar conjuntamente informações das tabelas `CANDIDATURAS`, `CANDIDATAS` e `VAGAS`.

O resultado apresentado no console contém:

```text
Candidata
E-mail
Vaga
Empresa
```

---

## 🧪 Simulação realizada

O `Program.cs` executa o fluxo completo proposto no desafio.

### 1. Cadastro das vagas

Foram cadastradas duas vagas:

**Engenheira de Dados**

* Empresa: Empresa Alfa
* Salário: R$ 9.000

**Analista de BI**

* Empresa: Empresa Ômega
* Salário: R$ 7.500

### 2. Cadastro da candidata

Foi cadastrada:

**Mariana Souza**

E-mail:

```text
mariana.souza@email.com
```

### 3. Envio das candidaturas

Mariana foi cadastrada nas duas vagas:

```text
Inscrição 901 → Engenheira de Dados
Inscrição 902 → Analista de BI
```

### 4. Consulta das candidaturas

O método `ConsultarCandidaturas()` apresenta as candidaturas cadastradas utilizando o **Double JOIN**.

### 5. Atualização do salário

O salário da vaga **Engenheira de Dados** foi atualizado:

```text
R$ 9.000 → R$ 9.500
```

### 6. Cancelamento da candidatura

A candidatura de inscrição **902**, referente à vaga de Analista de BI, foi cancelada.

### 7. Consulta final

Uma nova consulta é realizada para verificar as candidaturas ativas.

Ao final do fluxo, permanece cadastrada a candidatura:

```text
Mariana Souza
→ Engenheira de Dados
→ Empresa Alfa
→ R$ 9.500
```

---

## 🔗 Conexão com o banco

A aplicação utiliza a biblioteca `Microsoft.Data.Sqlite` e estabelece a conexão com o banco por meio da seguinte string:

```csharp
Data Source=/content/vagastech.db
```

A conexão é aberta antes da execução das operações e encerrada ao final do pipeline.

---

## 📌 Persistência dos dados

Após a execução da aplicação, o banco SQLite permanece armazenado fisicamente no arquivo `.db`.

A persistência pode ser validada no **DBeaver** por meio de consultas SQL, como:

```sql
SELECT * FROM CANDIDATURAS;
```

Essa validação permite confirmar que os dados foram efetivamente gravados no banco.

---

## 📦 Entregáveis

O projeto final deve conter:

```text
VagasTech
│
├── VagasTech.ipynb
├── vagastech.db
└── DDL_Criacao.sql
```

### VagasTech.ipynb

Notebook do Google Colab contendo o desenvolvimento da aplicação C#.

### vagastech.db

Banco de dados SQLite utilizado pela aplicação, contendo os dados persistidos.

### DDL_Criacao.sql

Arquivo contendo os comandos SQL utilizados para criação das tabelas do banco.