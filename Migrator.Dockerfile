# Dockerfile para executar as migrations

FROM mcr.microsoft.com/dotnet/sdk:9.0

# Instala ferramentas necessárias
RUN apt-get update && \
    apt-get install -y postgresql-client && \
    dotnet tool install --global dotnet-ef && \
    rm -rf /var/lib/apt/lists/*

# Adiciona dotnet-ef ao PATH
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /src
