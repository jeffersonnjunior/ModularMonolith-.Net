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
