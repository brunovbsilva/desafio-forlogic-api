# Desafio Técnico ForLogic

Este é um projeto desenvolvido como parte de um desafio técnico para a construção de um aplicativo web. O projeto possui como requisitos ter 3 páginas, sendo login, listagem e cadastro, onde cada uma possui suas próprias funcionalidades.

## Funcionalidades

- Cadastro de pessoas seguindo as 4 etapas da cebola;
- Listagem de pessoas cadastradas;
- Tratativa de dados (pessoas cadastradas) para exibir a quantidade de pessoas cadastradas no último mês, quantidade inativa e total;

## Tecnologias

- .Net 9.x;
- xUnit para testes unitários;

## Requisitos

- Visual Studio Code, Visual Studio ou Rider: versão mais recente;
- Docker: versão mais recente;

## Como executar o projeto

Na pasta raiz do projeto, execute o seguinte comando no terminal:

    docker-compose up -d

## Testes

Na pasta raiz do projeto, execute o seguinte comando no terminal:

    dotnet test

## Pipelines

#### CI

A pipeline de CI é executada sempre que um pullrequest é aberto para uma das branchs principais (development, stage ou production). A mesma executa os passos:

- Verifica se as dependências do projeto estão corretas;
- Verifica se o projeto está gerando o build corretamente;
- Verifica se os testes unitários estão corretos;

#### Publish Package to Github

Esta pipeline será responsável pelo controle de versões do aplicativo, gerando uma imagem docker para cada push realizado em uma das branchs principais. Os passos dela são:

- Fazer o login no github utilizando uma key privada através dos secrets do repositório
- Edita as chaves padrões utilizadas no projeto rodando via docker-compose para as chaves do meu servidor em núvem.
- Extrair o metadata do push (branch e sha key)
- Build e push Docker Image utilizando o metadata do passo anterior como tags para gerar o histórico de versões

## Considerações finais

Este projeto foi desenvolvido por Bruno Vinicius Barros da Silva, durante o período de 1 a 3 de abril de 2025.
O front-end do projeto se encontra disponível [neste link](https://github.com/brunovbsilva/desafio-forlogic)
