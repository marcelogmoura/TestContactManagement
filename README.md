# DotNet Contact Management

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 
![.NET Core](https://img.shields.io/badge/.NET-512BD4?style=flat&logo=dotnet&logoColor=white)
![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=flat&logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=flat&logo=css3&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-007ACC?style=flat&logo=typescript&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=flat&logo=visual-studio&logoColor=white)
![VS Code](https://img.shields.io/badge/VS_Code-007ACC?style=flat&logo=visual-studio-code&logoColor=white)
![Git](https://img.shields.io/badge/GIT-F05032?style=flat&logo=git&logoColor=white)
![GitHub](https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white)
![Postman](https://img.shields.io/badge/Postman-FF6C37?style=flat&logo=postman&logoColor=white)


# 🚀 Gerenciador de Contatos (Contact Management)

Este projeto é uma aplicação web desenvolvida em **ASP.NET Core 6** com **Razor Pages**, como parte de um exercício de avaliação técnica. A aplicação implementa um sistema CRUD (Create, Read, Update, Delete) completo para gerenciar contatos.


## 📋 Requisitos e Documentação

Os requisitos completos do teste técnico estão detalhados no documento a seguir:
* **[Enunciado](https://github.com/marcelogmoura/TestContactManagement/blob/main/Enunciado/Exerc%C3%ADcio%20.NET%20Web%20V2.docx)**


## ✨ Funcionalidades

O projeto atende a todos os requisitos solicitados:

* **Listagem (Index):** Página principal que exibe todos os contatos ativos.
* **Adicionar (Create):** Formulário para adicionar novos contatos.
* **Detalhes (Details):** Página para exibir informações de um único contato.
* **Editar (Edit):** Formulário para editar um registro existente.
* **Deletar (Delete):** Página de confirmação para deletar um registro.
* **Soft Delete:** Registros não são removidos fisicamente; eles são apenas marcados como "deletados" e filtrados da visualização.
* **Validações:** Validações de dados no backend e frontend, incluindo:
    * Nome (mínimo de 5 caracteres).
    * Telefone (exatamente 9 dígitos).
    * Email (formato válido).
    * **Campos Únicos:** Telefone e E-mail não podem ser duplicados no banco.
* **Autenticação:** A página de listagem é pública, mas todas as ações (Adicionar, Ver, Editar, Deletar) exigem que o usuário esteja autenticado.
* **Testes:** O projeto inclui testes de unidade (xUnit) para validar a lógica de negócios e as regras de validação.

---

## 🛠️ Tecnologias Utilizadas

* **Framework:** ASP.NET Core 6 (com Razor Pages)
* **Banco de Dados:** MariaDB (v10.6) 
* **ORM:** Entity Framework Core 6
* **Driver do BD:** `Pomelo.EntityFrameworkCore.MySql`
* **Autenticação:** ASP.NET Core Identity
* **Testes:** xUnit

---

## 🏁 Como Executar o Projeto

Siga estes passos para configurar e executar a aplicação localmente.

### 1. Pré-requisitos

* [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* Um servidor MariaDB (v10.6+) acessível (ex: via Docker, XAMPP, etc.).

### 2. Clonar o Repositório

```bash
git clone https://github.com/marcelogmoura/TestContactManagement
cd ContactManagement
```

### 3. Configurar a Conexão (appsettings)
A aplicação está configurada para usar dois arquivos appsettings.


appsettings.json: Contém a string de conexão para o ambiente de deploy (com placeholders).

appsettings.Development.json: Este é o arquivo que você deve editar para o seu ambiente local.

Abra o arquivo appsettings.Development.json e atualize a string de conexão MariaDB com suas credenciais locais:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "MariaDB": "Server=[SEU_SERVIDOR];Port=[SUA_PORTA];DataBase=default;Uid=[SEU_USUARIO];Pwd=[SUA_SENHA];"
  }
}
```

### 🖥️ Status e Persistência de Dados

Após executar o `docker-compose up`, você pode verificar o status dos contêineres e a persistência dos dados.


## 4. Executar a Aplicação
A aplicação está configurada para inicializar o banco de dados automaticamente ao iniciar. Isso inclui:

Criar o banco de dados (se não existir).

Aplicar todas as migrações do Entity Framework (tabelas de Contatos e Identity).

Criar um usuário "estático" para testes (seeding).

Você pode executar o projeto de duas formas:

Via Visual Studio 2022:

Abra o arquivo ContactManagement.sln.

Pressione F5 (ou o botão "Play" ▷).

# Navegue até a pasta do projeto (onde está o .csproj)
cd ContactManagement

# Restaura os pacotes
dotnet restore

# Executa o projeto
dotnet run

### 🔑 Autenticação e Autorização

Para acessar as áreas restritas (Adicionar, Ver, Editar, Deletar), utilize o usuário estático que é criado automaticamente:

Usuário: admin@admin.com
Senha: admin123


### 🧪 Publicação

![Publicação em andamento](https://i.postimg.cc/x160V9q8/Screenshot-15.jpg)


### 🧪 Lista

![Listagem](https://i.postimg.cc/RhmrnYN3/Screenshot-9.jpg)


### 🧪 Testes de Integração

O projeto inclui testes de integração para validar as funcionalidades de ponta a ponta. Eles garantem que o fluxo de login e as operações de CRUD de contatos estão funcionando como esperado.

![Resultado dos Testes de Integração](https://i.postimg.cc/VkqYGjPx/Screenshot-1.jpg)

**Execute os testes:**

No Visual Studio, vá para `Teste > Gerenciador de Testes` e clique em **"Executar Todos os Testes"**.


## 🚀 Melhorias Pós-Entrega

Após a submissão oficial do teste, continuei refinando o projeto para aplicar melhores práticas de arquitetura e segurança.

### ✅ Implementado (v1.1)
- **Refatoração com DTOs:** Implementação de *Data Transfer Objects* (DTOs) nos fluxos de **criação** e **edição** de contatos. Isso desacopla a camada de persistência da camada de apresentação, garantindo maior segurança no tráfego de dados e facilitando validações futuras.
  - *Commit referência:* `ab8cdec`




👨‍💻 **Autor:** Marcelo Moura 

📧 **Email:** [mgmoura@gmail.com](mailto:mgmoura@gmail.com)   
📧 **Email:** [admin@allriders.com.br](mailto:admin@allriders.com.br)   
🐱 **GitHub:** [github.com/marcelogmoura](https://github.com/marcelogmoura)   
🔗 **LinkedIn:** [linkedin.com/in/marcelogmoura](https://www.linkedin.com/in/marcelogmoura/)   