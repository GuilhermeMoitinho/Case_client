# CaseClient Web API com Vue.js Frontend

## Descrição

Este projeto implementa uma aplicação com uma arquitetura baseada em DDD (Domain Driven Design) e CQRS (Command Query Responsibility Segregation). O backend foi desenvolvido utilizando .NET 8 com Entity Framework Core para comandos (PostgreSQL) e MongoDB para consultas, enquanto o frontend foi construído com Vue.js 3.

## Funcionalidades

### Frontend (Vue.js 3)

O frontend da aplicação é responsável pela interação do usuário, onde são realizadas as operações de visualização, inclusão, edição e remoção de clientes.

- **Listagem de Clientes**: Tela que exibe a lista de todos os clientes cadastrados no sistema.
- **Tela de Inclusão e Edição de Clientes**: Formulário para inclusão de novos clientes ou edição de clientes existentes.
- **Tela de remoção**: Permite a exclusão de um cliente do sistema.

### Backend (Web API .NET 8)

O backend da aplicação é responsável pela lógica de negócio e pela exposição de uma API RESTful. As principais funcionalidades incluem:

- **Cadastro de Clientes**: A API permite a criação, atualização e remoção de clientes, com os seguintes campos:
  - ID
  - Nome da Empresa
  - Porte da empresa (pequena, média, grande)

- **CQRS**: A separação entre comandos e consultas é implementada, utilizando PostgreSQL para persistência de comandos e MongoDB para projeção de consultas.

## Camadas

- **Core**: Contém as definições de interfaces e classes fundamentais para o domínio e a aplicação.
- **Data**: Implementação da persistência de dados com acesso ao banco de dados.
- **Application**: MediatR para desacoplamento de comandos e eventos, utilizando CQRS.
- **Domain**: Implementação do domínio com IAggregateRoot, eventos de domínio e padrões de repositório.
- **WebApi**: A camada responsável pela exposição da API RESTful.

## Arquitetura

- Arquitetura completa com separação de responsabilidades, SOLID e Clean Code.
- Domain Driven Design (Camadas e Padrão de Modelo de Domínio).
- Domain Events
- Domain Validations
- CQRS (Imediate Consistency)
- Unit of Work
- Notifications
- Repository

## Tecnologias

- **Backend**: .NET 8, Entity Framework Core (PostgreSQL), MongoDB, MediatR, FluentValidation, MemoryCache, HealthChecks.
- **Frontend**: Vue.js 3
- **Banco de Dados**: PostgreSQL (para comandos), MongoDB (para consultas).
- **Docker**: Utilizado Docker Compose para orquestração de containers.

## Tecnologias e Padrões Usados

- **CQRS**: Separação clara entre comandos e consultas.
- **Mediator**: Desacoplamento de comandos e eventos, utilizando o `INotification`.
- **Repository Pattern**: Para abstração da comunicação com os bancos de dados.
- **FluentValidation**: Para validação dos dados de entrada.
- **MemoryCache**: Para caching das consultas.
- **HealthChecks**: Para garantir a consistência do ciclo de vida da aplicação.
- **UnityOfWork**: Para controle transacional durante a execução de comandos.

## Como Rodar o Projeto

Este projeto utiliza o Docker para rodar a aplicação de forma simples e rápida. Certifique-se de ter o Docker e o Docker Compose instalados na sua máquina.

### Passos para rodar a aplicação:

1. Clone este repositório para sua máquina local:

   ```bash
   git clone https://github.com/GuilhermeMoitinho/Case_client.git
   cd case-client
