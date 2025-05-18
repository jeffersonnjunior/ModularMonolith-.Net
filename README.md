# Documentação do Projeto

## Regra de Negócio
Projeto para gerenciamento de fábrica de veículos, com funcionalidades como controle de estoque de peças e gestão da produção, incluindo montagem dos carros e processo de venda.

## Tecnologias
- C#, ASP.NET Core, PostgreSQL, Redis, Entity Framework, xUnit e Docker

## Padrões de Design
- DDD, SOLID, Decorator, Factory e Notification  

## Arquitetura
Monolito com DDD e CQRS, assegurando organização por contexto de domínio, clareza nas operações e manutenção facilitada.

## Camada Api

### Controllers
Contém as controllers responsáveis por gerenciar as requisições e respostas da aplicação, servindo como ponto de entrada para os endpoints.

### Filters
Armazena a configuração dos filters, que gerenciam exceções e fornecem um tratamento consistente para erros no projeto.

### Middlewares
Configuração exclusiva para políticas de CORS, permitindo controlar o acesso de origens externas à API.

### Versioning
Contém a configuração de versionamento, permitindo gerenciar diferentes versões da API de forma organizada.

## Camada Common

### DependencyInjection
Configuração dos serviços de injeção de dependência para garantir a resolução correta das classes na camada common.

### Exceptions
Agrupa as classes de exceções customizadas utilizadas no sistema, permitindo um controle mais claro e específico de erros durante a execução.

### ICache
Define a interface base para implementação de cache, padronizando a comunicação com mecanismos de armazenamento em memória.

### IPersistence
Define a interface para o acesso a dados, com interfaces que representam os contratos de persistência usados pelas camadas superiores.

## Camada Infrastructure

### DependencyInjection
Configuração dos serviços de injeção de dependência para garantir a resolução correta das classes na camada infrastructure.

### Cache
Contém a implementação das estratégias de cache, como uso de memória ou mecanismos externos, para otimizar o acesso a dados frequentemente utilizados.

### Persistence
Agrupa as implementações responsáveis pela persistência de dados, como repositórios e contextos de banco de dados, seguindo os contratos definidos na camada common.

## Modules

A aplicação é organizada em três módulos principais: **Inventory**, **Production** e **Sales**.  
Cada módulo possui sua própria estrutura interna, garantindo separação de responsabilidades e escalabilidade.

### Commands
Contém os comandos responsáveis por executar ações que alteram o estado da aplicação, como criação, atualização ou remoção de dados.

### Decorator
Responsável por aplicar comportamentos adicionais aos comandos e queries, como validação de dados e verificação no cache antes da execução principal.

### DependencyInjection
Configuração dos serviços de injeção de dependência para garantir a resolução correta das classes na camada modules.

### Dtos
Definição dos Data Transfer Objects (DTOs) para transferência de dados entre as camadas de forma estruturada e segura.

### Entities
Agrupa as entidades que representam os objetos principais de negócio, com suas propriedades e comportamentos.

### Enums
Fornece listas de valores fixos usados para categorizar e organizar informações de forma consistente no sistema.

### Factory
Mapeamento entre as entidades e os DTOs, facilitando a conversão de dados para comunicação entre as camadas.

### Interfaces
Interfaces que definem as operações e métodos utilizados na camada modules, garantindo flexibilidade e desacoplamento.

### Mappings  
Gerencia o mapeamento entre as entidades do domínio e as tabelas do banco de dados.

### Query
Contém as operações de leitura utilizadas para obter informações da aplicação, focando em performance e retorno de dados em formatos específicos.

## Tests
Contém todos os testes unitários do projeto, desenvolvidos utilizando o framework xUnit para validar as funcionalidades de forma isolada.

## .github/workflows 
Contém as configurações para os pipelines de CI/CD do projeto, automatizando processos como build, execução de testes e criação de imagens Docker.
![image](https://github.com/user-attachments/assets/45ce3784-1238-4ca3-bee5-17827ba0790f)

## Diagrama do Banco
```mermaid
erDiagram

%% Módulo: Inventory
Part {
    Guid Id PK
    string Code UK
    string Description
    int QuantityInStock
    int MinimumRequired
    DateTime CreatedAt
}

ReplenishmentRequest {
    Guid Id PK
    string PartCode FK
    int RequestedQuantity
    ReplenishmentStatus ReplenishmentStatus
    DateTime RequestedAt
}

%% Módulo: Production
ProductionOrder {
    Guid Id PK
    string Model
    ProductionStatus ProductionStatus
    DateTime CreatedAt
    DateTime? CompletedAt
}

ProductionPart {
    Guid Id PK
    Guid ProductionOrderId FK
    string PartCode FK
    int QuantityUsed
}

%% Módulo: Sales
CarSale {
    Guid Id PK
    Guid ProductionOrderId FK
    SaleStatus Status
    DateTime? SoldAt
}

SaleDetail {
    Guid Id PK
    Guid CarSaleId FK
    string BuyerName
    decimal Price
    decimal Discount
    string PaymentMethod
    string Notes
}

%% Relacionamentos
Part ||--o{ ReplenishmentRequest : "1-N (Part.Code → ReplenishmentRequest.PartCode)"
Part ||--o{ ProductionPart : "1-N (Part.Code → ProductionPart.PartCode)"
ProductionOrder ||--o{ ProductionPart : "1-N (Production
```
