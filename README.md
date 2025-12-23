# 📋 Contratos API

API REST para gerenciamento de contratos empresariais desenvolvida com ASP.NET Core 9.0, Entity Framework Core e MariaDB.

---

## 📑 Índice

- [Pré-requisitos](#-pré-requisitos)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Instalação](#-instalação)
  - [Opção 1: Clone do GitHub](#opção-1-clone-do-github)
  - [Opção 2: Download do ZIP](#opção-2-download-do-zip)
- [Configuração do Banco de Dados](#-configuração-do-banco-de-dados)
- [Executando o Projeto](#-executando-o-projeto)
- [Testando a API](#-testando-a-api)
- [Endpoints da API](#-endpoints-da-api)
  - [Estados](#1-estados)
  - [Tipos de Contrato](#2-tipos-de-contrato)
  - [Status de Contrato](#3-status-de-contrato)
  - [Tipos de Contraente](#4-tipos-de-contraente)
  - [Empresas](#5-empresas)
  - [Funcionários](#6-funcionários)
  - [Contratos](#7-contratos)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Troubleshooting](#-troubleshooting)

---

## 🔧 Pré-requisitos

Antes de começar, certifique-se de ter instalado em sua máquina:

### Software Necessário

| Software | Versão Mínima | Download |
|----------|---------------|----------|
| **.NET SDK** | 9.0 | [Download](https://dotnet.microsoft.com/download/dotnet/9.0) |
| **Docker Desktop** | Latest | [Download](https://www.docker.com/products/docker-desktop) |
| **Git** (opcional) | 2.x | [Download](https://git-scm.com/downloads) |

### Verificar Instalações

```bash
# Verificar .NET
dotnet --version
# Deve retornar: 9.0.x

# Verificar Docker
docker --version
# Deve retornar: Docker version 20.x ou superior

# Verificar Docker Compose
docker-compose --version
# Deve retornar: Docker Compose version 2.x ou superior
```

---

## 🚀 Tecnologias Utilizadas

- **ASP.NET Core 9.0** - Framework web
- **Entity Framework Core 9.0** - ORM
- **MariaDB** - Banco de dados relacional
- **Pomelo.EntityFrameworkCore.MySql** - Provider MySQL/MariaDB
- **AutoMapper 13.0** - Mapeamento objeto-objeto
- **Swashbuckle (Swagger)** - Documentação da API
- **Docker** - Containerização

---

## 📥 Instalação

### Opção 1: Clone do GitHub

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/ContratosAPI.git

# Entre na pasta do projeto
cd ContratosAPI
```

### Opção 2: Download do ZIP

1. Baixe o arquivo `ContratosAPI.zip`
2. Extraia o arquivo em uma pasta de sua preferência
3. Abra o terminal/prompt de comando na pasta extraída

```bash
# Windows (PowerShell)
cd C:\caminho\para\ContratosAPI

# Linux/Mac
cd /caminho/para/ContratosAPI
```

---

## 🗄️ Configuração do Banco de Dados

### Passo 1: Iniciar o Container MariaDB

O projeto utiliza Docker para executar o banco de dados MariaDB.

```bash
# Certifique-se de estar na pasta do projeto
cd ContratosAPI

# Inicie o container do banco de dados
docker-compose up -d
```

**Saída esperada:**
```
Creating network "contratosapi_default" with the default driver
Creating volume "contratosapi_mariadb_data" with default driver
Creating contratos_api ... done
```

### Passo 2: Verificar se o Container está Rodando

```bash
docker ps
```

**Saída esperada:**
```
CONTAINER ID   IMAGE            COMMAND                  STATUS         PORTS                    NAMES
xxxxxxxxxxxxx  mariadb:latest   "docker-entrypoint.s…"   Up 30 seconds  0.0.0.0:3306->3306/tcp   contratos_api
```

### Passo 3: Restaurar Pacotes NuGet

```bash
dotnet restore
```

### Passo 4: Aplicar Migrations ao Banco de Dados

```bash
# Aplicar migrações (cria as tabelas no banco)
dotnet ef database update
```

**Saída esperada:**
```
Build started...
Build succeeded.
Applying migration '20251222184650_InitialCreate'.
Applying migration '20251222184934_Update'.
Applying migration '20251222222004_NewUpdate'.
Done.
```

> **Nota:** Se o comando `dotnet ef` não for reconhecido, instale a ferramenta:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

---

## ▶️ Executando o Projeto

### Método 1: Usando dotnet run

```bash
# Execute a aplicação
dotnet run
```

**Saída esperada:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5016
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### Método 2: Usando dotnet watch (Hot Reload)

```bash
# Execute com auto-reload (recarrega quando arquivos são modificados)
dotnet watch run
```

### Acessar a Aplicação

Após executar o projeto, acesse:

- **Swagger UI**: [http://localhost:5016/swagger/index.html](http://localhost:5016/swagger/index.html)
- **API Base URL**: [http://localhost:5016/api](http://localhost:5016/api)

---

## 🧪 Testando a API

### Usando Swagger UI

1. Abra o navegador e acesse: [http://localhost:5016/swagger/index.html](http://localhost:5016/swagger/index.html)
2. Você verá a interface do Swagger com todos os endpoints disponíveis
3. Clique em qualquer endpoint para expandir
4. Clique em **"Try it out"**
5. Preencha os parâmetros necessários
6. Clique em **"Execute"**
7. Veja a resposta da API

### Usando cURL

Você também pode testar usando cURL no terminal:

```bash
# Listar todos os estados
curl http://localhost:5016/api/estados
```

---

## 📡 Endpoints da API

### 1. Estados

Gerencia os estados brasileiros (tabela de referência).

#### GET /api/estados
**Descrição:** Retorna todos os estados brasileiros.

**Request:**
```bash
curl -X GET http://localhost:5016/api/estados
```

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "sigla": "AC",
    "nome": "Acre"
  },
  {
    "id": 25,
    "sigla": "SP",
    "nome": "São Paulo"
  }
]
```

#### GET /api/estados/{id}
**Descrição:** Retorna um estado específico.

**Request:**
```bash
curl -X GET http://localhost:5016/api/estados/25
```

**Response:** `200 OK`
```json
{
  "id": 25,
  "sigla": "SP",
  "nome": "São Paulo"
}
```

---

### 2. Tipos de Contrato

Gerencia os tipos de contrato (tabela de referência).

#### GET /api/tiposcontrato
**Descrição:** Retorna todos os tipos de contrato.

**Request:**
```bash
curl -X GET http://localhost:5016/api/tiposcontrato
```

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "nome": "Prestação de Serviços",
    "descricao": "Contrato de prestação de serviços profissionais"
  },
  {
    "id": 2,
    "nome": "Consultoria",
    "descricao": "Contrato de consultoria especializada"
  }
]
```

---

### 3. Status de Contrato

Gerencia os status possíveis de um contrato.

#### GET /api/statuscontratos
**Descrição:** Retorna todos os status de contrato.

**Request:**
```bash
curl -X GET http://localhost:5016/api/statuscontratos
```

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "nome": "Ativo",
    "descricao": "Contrato em vigor"
  },
  {
    "id": 2,
    "nome": "Suspenso",
    "descricao": "Contrato temporariamente suspenso"
  },
  {
    "id": 3,
    "nome": "Encerrado",
    "descricao": "Contrato finalizado"
  }
]
```

---

### 4. Tipos de Contraente

Gerencia os tipos de contraente (Empresa ou Funcionário).

#### GET /api/tiposcontraente
**Descrição:** Retorna todos os tipos de contraente.

**Request:**
```bash
curl -X GET http://localhost:5016/api/tiposcontraente
```

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "nome": "Empresa",
    "descricao": "Pessoa Jurídica"
  },
  {
    "id": 2,
    "nome": "Funcionário",
    "descricao": "Pessoa Física"
  }
]
```

---

### 5. Empresas

Gerencia empresas (Pessoas Jurídicas).

#### GET /api/empresas
**Descrição:** Lista todas as empresas (paginado).

**Parâmetros Query:**
- `pageNumber` (opcional): Número da página (padrão: 1)
- `pageSize` (opcional): Tamanho da página (padrão: 10, máx: 100)

**Request:**
```bash
curl -X GET "http://localhost:5016/api/empresas?pageNumber=1&pageSize=10"
```

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "razaoSocial": "Tech Solutions Ltda",
    "nomeFantasia": "Tech Solutions",
    "cnpj": "12345678901234",
    "cidade": "São Paulo",
    "estadoSigla": "SP",
    "email": "contato@techsolutions.com",
    "telefone": "+5511987654321"
  }
]
```

**Response Headers:**
```
X-Total-Count: 1
X-Page-Number: 1
X-Page-Size: 10
```

#### GET /api/empresas/{id}
**Descrição:** Retorna uma empresa específica com detalhes completos.

**Request:**
```bash
curl -X GET http://localhost:5016/api/empresas/1
```

**Response:** `200 OK`
```json
{
  "id": 1,
  "razaoSocial": "Tech Solutions Ltda",
  "nomeFantasia": "Tech Solutions",
  "cnpj": "12345678901234",
  "logradouro": "Av. Paulista",
  "numero": "1000",
  "complemento": "Sala 501",
  "setor": "Tecnologia",
  "cidade": "São Paulo",
  "estadoId": 25,
  "estadoSigla": "SP",
  "estadoNome": "São Paulo",
  "email": "contato@techsolutions.com",
  "telefone": "+5511987654321",
  "telefoneAlternativo": "+5511912345678",
  "website": "https://www.techsolutions.com",
  "linkedIn": "tech-solutions",
  "totalContratos": 5
}
```

#### GET /api/empresas/cnpj/{cnpj}
**Descrição:** Busca empresa por CNPJ.

**Request:**
```bash
curl -X GET http://localhost:5016/api/empresas/cnpj/12345678901234
```

**Response:** `200 OK` ou `404 Not Found`

#### POST /api/empresas
**Descrição:** Cria uma nova empresa.

**Request:**
```bash
curl -X POST http://localhost:5016/api/empresas \
  -H "Content-Type: application/json" \
  -d '{
    "razaoSocial": "Tech Solutions Ltda",
    "nomeFantasia": "Tech Solutions",
    "cnpj": "12345678901234",
    "logradouro": "Av. Paulista",
    "numero": "1000",
    "complemento": "Sala 501",
    "setor": "Tecnologia",
    "cidadeEstado": {
      "cidade": "São Paulo",
      "estadoId": 25
    },
    "contato": {
      "email": "contato@techsolutions.com",
      "telefone": "+5511987654321",
      "telefoneAlternativo": "+5511912345678",
      "website": "https://www.techsolutions.com",
      "linkedIn": "tech-solutions"
    }
  }'
```

**Response:** `201 Created`
```json
{
  "id": 1,
  "razaoSocial": "Tech Solutions Ltda",
  "nomeFantasia": "Tech Solutions",
  "cnpj": "12345678901234",
  ...
}
```

**Validações:**
- `razaoSocial`: Obrigatório, máx 200 caracteres
- `nomeFantasia`: Obrigatório, máx 200 caracteres
- `cnpj`: Obrigatório, exatamente 14 dígitos numéricos, único
- `email`: Obrigatório, formato válido de email
- `telefone`: Obrigatório, formato válido
- `estadoId`: Obrigatório, deve existir na tabela Estados

#### PUT /api/empresas/{id}
**Descrição:** Atualiza uma empresa existente.

**Request:**
```bash
curl -X PUT http://localhost:5016/api/empresas/1 \
  -H "Content-Type: application/json" \
  -d '{
    "razaoSocial": "Tech Solutions Ltda - Atualizada",
    "nomeFantasia": "Tech Solutions",
    "cnpj": "12345678901234",
    "logradouro": "Av. Paulista",
    "numero": "2000",
    "complemento": "Sala 1001",
    "setor": "Tecnologia da Informação",
    "cidadeEstado": {
      "cidade": "São Paulo",
      "estadoId": 25
    },
    "contato": {
      "email": "contato@techsolutions.com",
      "telefone": "+5511987654321"
    }
  }'
```

**Response:** `204 No Content`

#### DELETE /api/empresas/{id}
**Descrição:** Remove uma empresa.

**Request:**
```bash
curl -X DELETE http://localhost:5016/api/empresas/1
```

**Response:** `204 No Content` ou `409 Conflict` (se houver contratos vinculados)

**Response 409:**
```json
{
  "error": "Empresa possui contratos vinculados",
  "message": "Não é possível excluir uma empresa com contratos vinculados"
}
```

---

### 6. Funcionários

Gerencia funcionários (Pessoas Físicas).

#### GET /api/funcionarios
**Descrição:** Lista todos os funcionários (paginado).

**Request:**
```bash
curl -X GET "http://localhost:5016/api/funcionarios?pageNumber=1&pageSize=10"
```

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "nomeCompleto": "João Silva",
    "cpf": "12345678901",
    "idade": 30,
    "funcao": "Desenvolvedor",
    "cidade": "São Paulo",
    "estadoSigla": "SP",
    "email": "joao.silva@email.com",
    "telefone": "+5511987654321"
  }
]
```

#### GET /api/funcionarios/{id}
**Descrição:** Retorna um funcionário específico.

**Request:**
```bash
curl -X GET http://localhost:5016/api/funcionarios/1
```

**Response:** `200 OK`
```json
{
  "id": 1,
  "nomeCompleto": "João Silva",
  "dataNascimento": "1994-05-15T00:00:00",
  "idade": 30,
  "cpf": "12345678901",
  "funcao": "Desenvolvedor Senior",
  "cidade": "São Paulo",
  "estadoId": 25,
  "estadoSigla": "SP",
  "estadoNome": "São Paulo",
  "email": "joao.silva@email.com",
  "telefone": "+5511987654321",
  "telefoneAlternativo": null,
  "website": null,
  "linkedIn": "joao-silva",
  "totalContratos": 3
}
```

#### GET /api/funcionarios/cpf/{cpf}
**Descrição:** Busca funcionário por CPF.

**Request:**
```bash
curl -X GET http://localhost:5016/api/funcionarios/cpf/12345678901
```

**Response:** `200 OK` ou `404 Not Found`

#### POST /api/funcionarios
**Descrição:** Cria um novo funcionário.

**Request:**
```bash
curl -X POST http://localhost:5016/api/funcionarios \
  -H "Content-Type: application/json" \
  -d '{
    "nomeCompleto": "João Silva",
    "dataNascimento": "1994-05-15",
    "cpf": "12345678901",
    "funcao": "Desenvolvedor Senior",
    "cidadeEstado": {
      "cidade": "São Paulo",
      "estadoId": 25
    },
    "contato": {
      "email": "joao.silva@email.com",
      "telefone": "+5511987654321",
      "linkedIn": "joao-silva"
    }
  }'
```

**Response:** `201 Created`

**Validações:**
- `nomeCompleto`: Obrigatório, máx 200 caracteres
- `dataNascimento`: Obrigatório, idade mínima 14 anos
- `cpf`: Obrigatório, exatamente 11 dígitos numéricos, único
- `funcao`: Obrigatório, máx 200 caracteres
- `email`: Obrigatório, formato válido

#### PUT /api/funcionarios/{id}
**Descrição:** Atualiza um funcionário existente.

**Request:**
```bash
curl -X PUT http://localhost:5016/api/funcionarios/1 \
  -H "Content-Type: application/json" \
  -d '{
    "nomeCompleto": "João Silva Santos",
    "dataNascimento": "1994-05-15",
    "cpf": "12345678901",
    "funcao": "Tech Lead",
    "cidadeEstado": {
      "cidade": "São Paulo",
      "estadoId": 25
    },
    "contato": {
      "email": "joao.silva@email.com",
      "telefone": "+5511987654321"
    }
  }'
```

**Response:** `204 No Content`

#### DELETE /api/funcionarios/{id}
**Descrição:** Remove um funcionário.

**Request:**
```bash
curl -X DELETE http://localhost:5016/api/funcionarios/1
```

**Response:** `204 No Content` ou `409 Conflict` (se houver contratos vinculados)

---

### 7. Contratos

Gerencia contratos entre empresas e contraentes (Empresa ou Funcionário).

#### GET /api/contratos
**Descrição:** Lista todos os contratos (paginado e com filtros).

**Parâmetros Query:**
- `pageNumber` (opcional): Número da página (padrão: 1)
- `pageSize` (opcional): Tamanho da página (padrão: 10)
- `statusId` (opcional): Filtrar por status
- `tipoId` (opcional): Filtrar por tipo de contrato

**Request:**
```bash
# Listar todos
curl -X GET http://localhost:5016/api/contratos

# Filtrar por status ativo
curl -X GET "http://localhost:5016/api/contratos?statusId=1"

# Filtrar por tipo e status
curl -X GET "http://localhost:5016/api/contratos?tipoId=1&statusId=1"
```

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "contratanteNome": "Tech Solutions Ltda",
    "contraenteNome": "João Silva",
    "tipoContratoNome": "Prestação de Serviços",
    "statusContratoNome": "Ativo",
    "precificacao": 5000.00,
    "dataEmissao": "2024-01-15T00:00:00",
    "estaVencido": false
  }
]
```

#### GET /api/contratos/{id}
**Descrição:** Retorna um contrato específico com detalhes completos.

**Request:**
```bash
curl -X GET http://localhost:5016/api/contratos/1
```

**Response:** `200 OK`
```json
{
  "id": 1,
  "contratanteId": 1,
  "contratanteNome": "Tech Solutions Ltda",
  "contratanteCNPJ": "12345678901234",
  "contraenteId": 1,
  "tipoContraenteId": 2,
  "tipoContraenteNome": "Funcionário",
  "contraenteNome": "João Silva",
  "contraenteDocumento": "12345678901",
  "tipoContratoId": 1,
  "tipoContratoNome": "Prestação de Serviços",
  "tipoContratoDescricao": "Contrato de prestação de serviços profissionais",
  "statusContratoId": 1,
  "statusContratoNome": "Ativo",
  "statusContratoDescricao": "Contrato em vigor",
  "precificacao": 5000.00,
  "condicoesPagamento": "Mensal, até o dia 10",
  "dataEmissao": "2024-01-15T00:00:00",
  "validade": "2025-01-15T00:00:00",
  "descricao": "Contrato de desenvolvimento de software",
  "estaVencido": false,
  "diasAtivo": 342
}
```

#### POST /api/contratos
**Descrição:** Cria um novo contrato.

**Request:**
```bash
curl -X POST http://localhost:5016/api/contratos \
  -H "Content-Type: application/json" \
  -d '{
    "contratanteId": 1,
    "contraenteId": 1,
    "tipoContraenteId": 2,
    "tipoContratoId": 1,
    "statusContratoId": 1,
    "precificacao": 5000.00,
    "condicoesPagamento": "Mensal, até o dia 10",
    "dataEmissao": "2024-01-15",
    "validade": "2025-01-15",
    "descricao": "Contrato de desenvolvimento de software"
  }'
```

**Response:** `201 Created`

**Validações:**
- `contratanteId`: Obrigatório, deve existir na tabela Empresas
- `contraenteId`: Obrigatório, deve existir (Empresa ou Funcionário conforme tipo)
- `tipoContraenteId`: Obrigatório, 1=Empresa ou 2=Funcionário
- `tipoContratoId`: Obrigatório, deve existir na tabela TiposContrato
- `statusContratoId`: Obrigatório, deve existir na tabela StatusContratos
- `precificacao`: Obrigatório, maior que zero
- `condicoesPagamento`: Obrigatório, máx 500 caracteres
- `dataEmissao`: Obrigatório

**Exemplo com Contraente Empresa:**
```bash
curl -X POST http://localhost:5016/api/contratos \
  -H "Content-Type: application/json" \
  -d '{
    "contratanteId": 1,
    "contraenteId": 2,
    "tipoContraenteId": 1,
    "tipoContratoId": 2,
    "statusContratoId": 1,
    "precificacao": 10000.00,
    "condicoesPagamento": "Trimestral, até o último dia útil",
    "dataEmissao": "2024-03-01",
    "validade": "2025-03-01",
    "descricao": "Contrato de consultoria empresarial"
  }'
```

#### PUT /api/contratos/{id}
**Descrição:** Atualiza um contrato existente.

**Request:**
```bash
curl -X PUT http://localhost:5016/api/contratos/1 \
  -H "Content-Type: application/json" \
  -d '{
    "tipoContratoId": 1,
    "statusContratoId": 2,
    "precificacao": 6000.00,
    "condicoesPagamento": "Mensal, até o dia 15",
    "dataEmissao": "2024-01-15",
    "validade": "2025-01-15",
    "descricao": "Contrato de desenvolvimento de software - ATUALIZADO"
  }'
```

**Response:** `204 No Content`

> **Nota:** O contratante e contraente geralmente não são alterados após a criação do contrato.

#### DELETE /api/contratos/{id}
**Descrição:** Remove um contrato.

**Request:**
```bash
curl -X DELETE http://localhost:5016/api/contratos/1
```

**Response:** `204 No Content`

---

## 📂 Estrutura do Projeto

```
ContratosAPI/
├── Controllers/               # Controllers da API
│   ├── ContratosController.cs
│   ├── EmpresasController.cs
│   ├── FuncionariosController.cs
│   ├── EstadosController.cs
│   ├── TipoContratoController.cs
│   ├── StatusContratoController.cs
│   └── TipoContraenteController.cs
├── DTOs/                      # Data Transfer Objects
│   ├── Common/                # DTOs compartilhados
│   │   ├── CidadeEstadoDTO.cs
│   │   ├── ContatoDTO.cs
│   │   ├── EstadoDTO.cs
│   │   ├── TipoContratoDTO.cs
│   │   ├── StatusContratoDTO.cs
│   │   └── TipoContraenteDTO.cs
│   ├── Empresa/               # DTOs de Empresa
│   │   ├── EmpresaCreateDto.cs
│   │   ├── EmpresaListDto.cs
│   │   ├── EmpresaPutDto.cs
│   │   └── EmpresaResponseDto.cs
│   ├── Funcionario/           # DTOs de Funcionário
│   │   ├── FuncionarioCreateDto.cs
│   │   ├── FuncionarioListDto.cs
│   │   ├── FuncionarioPutDto.cs
│   │   └── FuncionarioResponseDto.cs
│   └── Contrato/              # DTOs de Contrato
│       ├── ContratoCreateDto.cs
│       ├── ContratoListDto.cs
│       ├── ContratoPutDto.cs
│       └── ContratoResponseDto.cs
├── Models/                    # Entidades do domínio
│   ├── Empresa.cs
│   ├── Funcionario.cs
│   ├── Contrato.cs
│   ├── Estado.cs
│   ├── TipoContrato.cs
│   ├── StatusContrato.cs
│   ├── TipoContraente.cs
│   ├── CidadeEstado.cs        # Owned Entity
│   └── Contato.cs             # Owned Entity
├── Data/                      # Contexto do banco
│   ├── ApplicationDbContext.cs
│   └── DbContextFactory.cs
├── Mappings/                  # Configuração AutoMapper
│   └── AutoMapperProfile.cs
├── Middleware/                # Middlewares customizados
│   └── ErrorHandlingMiddleware.cs
├── Attributes/                # Atributos de validação
│   ├── ContraenteValidacao.cs
│   └── DataNascimentoValidacao.cs
├── Migrations/                # Migrations do EF Core
├── Properties/
│   └── launchSettings.json
├── appsettings.json           # Configurações da aplicação
├── docker-compose.yml         # Configuração Docker
├── Program.cs                 # Entry point
└── ContratosAPI.csproj        # Arquivo do projeto
```

---

## 🔍 Troubleshooting

### Problema: "dotnet: command not found"

**Solução:**
1. Instale o .NET SDK 9.0
2. Reinicie o terminal
3. Verifique: `dotnet --version`

### Problema: "docker: command not found"

**Solução:**
1. Instale o Docker Desktop
2. Inicie o Docker Desktop
3. Verifique: `docker --version`

### Problema: "Port 3306 already in use"

**Solução:**
```bash
# Parar o container conflitante
docker ps  # Liste os containers
docker stop <container_id>  # Pare o container que está usando a porta

# Ou altere a porta no docker-compose.yml:
ports:
  - "3307:3306"  # Mude 3306 para 3307
```

### Problema: "Unable to connect to database"

**Solução:**
```bash
# 1. Verifique se o container está rodando
docker ps

# 2. Verifique os logs do container
docker logs contratos_api

# 3. Reinicie o container
docker-compose down
docker-compose up -d

# 4. Aguarde alguns segundos e tente novamente
```

### Problema: "Port 5016 already in use"

**Solução:**
```bash
# Encontre o processo usando a porta
# Windows
netstat -ano | findstr :5016
taskkill /PID <PID> /F

# Linux/Mac
lsof -i :5016
kill -9 <PID>

# Ou altere a porta em Properties/launchSettings.json
```

### Problema: "Build failed" ou erros de compilação

**Solução:**
```bash
# Limpe e restaure o projeto
dotnet clean
dotnet restore
dotnet build
```

### Problema: "Migration failed"

**Solução:**
```bash
# Remova migrações existentes e recrie
rm -rf Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Problema: Swagger não abre

**Solução:**
1. Verifique se a aplicação está rodando: `dotnet run`
2. Acesse: `http://localhost:5016/swagger/index.html` (não `https`)
3. Verifique se não há erros no console

### Problema: "AutoMapper configuration error"

**Solução:**
Verifique se o `AutoMapperProfile` está registrado no `Program.cs`:
```csharp
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
```

---

## 📝 Dados de Seed

O banco de dados é populado automaticamente com dados de referência ao executar as migrations:

### Estados
- 27 estados brasileiros (AC, AL, AM, AP, BA, CE, DF, ES, GO, MA, MG, MS, MT, PA, PB, PE, PI, PR, RJ, RN, RO, RR, RS, SC, SE, SP, TO)

### Tipos de Contrato
1. Prestação de Serviços
2. Consultoria
3. Parceria Comercial
4. Desenvolvimento de Software
5. Suporte Técnico
6. Manutenção

### Status de Contrato
1. Ativo
2. Suspenso
3. Encerrado

### Tipos de Contraente
1. Empresa (Pessoa Jurídica)
2. Funcionário (Pessoa Física)

---

## 📞 Suporte

Se você encontrar problemas ou tiver dúvidas:

1. Verifique a seção [Troubleshooting](#-troubleshooting)
2. Consulte a documentação do Swagger: `http://localhost:5016/swagger`
3. Abra uma issue no GitHub (se aplicável)

---

## 📄 Licença

Este projeto é licenciado sob a [MIT License](LICENSE).

---

## ✅ Checklist de Setup

- [ ] .NET SDK 9.0 instalado
- [ ] Docker Desktop instalado e rodando
- [ ] Projeto clonado ou extraído
- [ ] Container MariaDB iniciado (`docker-compose up -d`)
- [ ] Pacotes NuGet restaurados (`dotnet restore`)
- [ ] Migrations aplicadas (`dotnet ef database update`)
- [ ] Aplicação rodando (`dotnet run`)
- [ ] Swagger acessível em `http://localhost:5016/swagger`
- [ ] Endpoints testados com sucesso

---

**Desenvolvido com ❤️ usando ASP.NET Core 9.0**
