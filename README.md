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
Contém as configurações centralizadas de injeção de dependência, facilitando o registro de serviços, repositórios e demais componentes da aplicação.

### Exceptions
Agrupa as classes de exceções customizadas utilizadas no sistema, permitindo um controle mais claro e específico de erros durante a execução.

### ICache
Define a interface base para implementação de cache, padronizando a comunicação com mecanismos de armazenamento em memória.

### IPersistence
Define a interface para o acesso a dados, com interfaces que representam os contratos de persistência usados pelas camadas superiores.

## Camada Infrastructure

### DependencyInjection
Implementa o registro concreto das dependências definidas nas interfaces da aplicação, conectando a infraestrutura ao restante do sistema.

### Cache
Contém a implementação das estratégias de cache, como uso de memória ou mecanismos externos, para otimizar o acesso a dados frequentemente utilizados.

### Persistence
Agrupa as implementações responsáveis pela persistência de dados, como repositórios e contextos de banco de dados, seguindo os contratos definidos na camada Common.

