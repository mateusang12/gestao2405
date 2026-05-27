1. Descrição do Projeto

Aplicação Web para agendamento de serviços em uma oficina mecânica, integrada à API pública da FIPE, a qual fornece marcas e modelos de veículos do ano 2010 até o presente momento.

2. Tecnologias Utilizadas
●	Linguagem: Html, Css, JavaScript e C#;
●	Framework: Hospedagem no Vercel;
●	Banco de Dados: Firebase;
●	Segurança:
●	Documentação de API:

3. Instruções de Execução
Para rodar o projeto localmente, siga os passos abaixo:
1. Clonar repositório do github para máquina local
2. Certificar no main.js se está direcionando para o backend
3. Teclar F5 para compilação do projeto
4. O terminal irá direcionar para HTTP://localhost:7024/swagger/index.html
5.

4. Endpoints da API 
Abaixo estão os principais endpoints disponíveis no sistema:

GET HTTPS://paraallelum.com.br/fipe/api/v1/carros/marcas - Tem a função de retornar lista de veículos;
GET HTTPS://parallelum.com.br/fipe/api/v1/carros/marcas/{codigoMarca}/modelos - O modelo está em função da marca selecionada.
