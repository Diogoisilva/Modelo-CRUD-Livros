<h3>Resumo do Projeto</h3>
Objetivo:
Desenvolver uma API RESTful para gerenciar Autores, Assuntos e Livros, utilizando uma arquitetura em camadas que separa a lógica de apresentação, negócios e acesso a dados.
<h4>
Benefícios da Arquitetura Utilizada
Separação de Preocupações: Cada camada tem uma responsabilidade específica, facilitando a manutenção e evolução do sistema.
Testabilidade: A lógica de negócios está separada da lógica de acesso a dados e da apresentação, facilitando a criação de testes unitários.
Flexibilidade: Permite substituição ou modificação de uma camada sem afetar as demais.
Escalabilidade: A arquitetura modular permite fácil expansão das funcionalidades e dos serviços.
</h4>
<strong>Arquitetura Utilizada:</strong>
A arquitetura do projeto segue o modelo de Camadas (Layered Architecture), dividida em quatro camadas principais:
<br><br>

<strong>
Camada de Apresentação (Presentation Layer)
</strong>
Projeto: Modelo.Api (Modelo)<br>
  -  Descrição: Contém os controladores (controllers) que expõem as APIs REST para o front-end ou outros consumidores da API, como AutorController, AssuntoController, LivroController e RelatorioController.
</strong>
<br><br>

<strong>
Camada de Negócios (Business Layer)
</strong>
Projeto: Modelo.Services <br>
  -  Descrição: Contém a lógica de negócios, implementada nos serviços como AutorService, AssuntoService e LivroService, responsáveis por validar e gerenciar as operações de negócios.
<br><br>

<strong>  
Camada de Dados (Data Layer)
</strong>
Projeto: Modelo.Infrastructure <br>
  -  Descrição: Contém a lógica de acesso aos dados, incluindo repositórios e helpers de conexão, como DbConnectionHelper, AutorRepository, AssuntoRepository e LivroRepository. Utiliza stored procedures para as operações de banco de dados.
<br><br>

<strong>
Camada de Domínio (Domain Layer)
</strong>
Projeto: Modelo.Domain <br>
 -   Descrição: Contém os modelos de dados (entities), interfaces e validações que definem o domínio da aplicação, como AutorRequestModel, AutorResponseModel, IAutorService, IAssuntoService, LivroRequestModel, LivroResponseModel, ILivroService, entre outros.
<br><br>

Principais Componentes
Modelos de Dados:

AutorRequestModel e AutorResponseModel: Para transferir dados entre a camada de apresentação e de negócios.
AssuntoRequestModel e AssuntoResponseModel: Para operações relacionadas a Assunto.
LivroRequestModel e LivroResponseModel: Para operações relacionadas a Livro.

Serviços:
AutorService, AssuntoService e LivroService: Implementam a lógica de negócios e interagem com os repositórios para realizar operações de CRUD. Validações são aplicadas nesta camada.

Repositórios:
AutorRepository, AssuntoRepository e LivroRepository: Implementam a interface de acesso a dados, utilizando DbConnectionHelper para executar stored procedures no banco de dados.

Controladores:
AutorController, AssuntoController e LivroController: Exponhem endpoints RESTful para interagir com os serviços de negócios. Responsáveis por receber requisições HTTP, chamar os serviços apropriados e retornar as respostas adequadas ao cliente.
Principais Funcionalidades Implementadas

Para Autores:
Inserir Autor: Endpoint para adicionar um novo autor utilizando a stored procedure InserirAutor.
Atualizar Autor: Endpoint para atualizar um autor existente utilizando a stored procedure AtualizarAutor.
Deletar Autor: Endpoint para deletar um autor utilizando a stored procedure DeletarAutor.
Obter Autor por ID: Endpoint para obter os detalhes de um autor específico.
Listar Autores: Endpoint para listar todos os autores.

Para Assuntos:
Inserir Assunto: Endpoint para adicionar um novo assunto utilizando a stored procedure InserirAssunto.
Atualizar Assunto: Endpoint para atualizar um assunto existente utilizando a stored procedure AtualizarAssunto.
Deletar Assunto: Endpoint para deletar um assunto utilizando a stored procedure DeletarAssunto.
Obter Assunto por ID: Endpoint para obter os detalhes de um assunto específico.
Listar Assuntos: Endpoint para listar todos os assuntos.

Para Livros:
Inserir Livro: Endpoint para adicionar um novo livro utilizando a stored procedure InserirLivro.
Atualizar Livro: Endpoint para atualizar um livro existente utilizando a stored procedure AtualizarLivro.
Deletar Livro: Endpoint para deletar um livro utilizando a stored procedure DeletarLivro.
Obter Livro por ID: Endpoint para obter os detalhes de um livro específico.
Listar Livros: Endpoint para listar todos os livros.
