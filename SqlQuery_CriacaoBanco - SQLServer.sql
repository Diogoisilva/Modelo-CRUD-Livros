-- Criar o banco de dados Modelo
CREATE DATABASE Modelo;
GO

-- Usar o banco de dados Modelo
USE Modelo;
GO

-- Criar tabela Livro
CREATE TABLE Livro (
    Codl INT PRIMARY KEY IDENTITY,
    Titulo VARCHAR(40) NOT NULL,
    Editora VARCHAR(40) NOT NULL,
    Edicao VARCHAR(40) NOT NULL,
    AnoPublicacao VARCHAR(4) NOT NULL,
    Preco DECIMAL(10, 2) NOT NULL,
    FormaCompra VARCHAR(50) NOT NULL
);

-- Criar tabela Assunto
CREATE TABLE Assunto (
    CodAs INT PRIMARY KEY IDENTITY,
    Descricao VARCHAR(20) NOT NULL
);

-- Criar tabela Autor
CREATE TABLE Autor (
    CodAu INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(40) NOT NULL
);

-- Criar tabela relacional Livro_Assunto
CREATE TABLE Livro_Assunto (
    Codl INT NOT NULL,
    CodAs INT NOT NULL,
    PRIMARY KEY (Codl, CodAs),
    CONSTRAINT FK_Livro_LivroAssunto FOREIGN KEY (Codl) REFERENCES Livro (Codl) ON DELETE CASCADE,
    CONSTRAINT FK_Assunto_LivroAssunto FOREIGN KEY (CodAs) REFERENCES Assunto (CodAs) ON DELETE CASCADE
);

-- Índices para otimizar consultas na tabela Livro_Assunto
CREATE INDEX IDX_Livro_Assunto_Codl ON Livro_Assunto (Codl);
CREATE INDEX IDX_Livro_Assunto_CodAs ON Livro_Assunto (CodAs);

-- Criar tabela relacional Livro_Autor
CREATE TABLE Livro_Autor (
    Codl INT NOT NULL,
    CodAu INT NOT NULL,
    PRIMARY KEY (Codl, CodAu),
    CONSTRAINT FK_Livro_LivroAutor FOREIGN KEY (Codl) REFERENCES Livro (Codl) ON DELETE CASCADE,
    CONSTRAINT FK_Autor_LivroAutor FOREIGN KEY (CodAu) REFERENCES Autor (CodAu) ON DELETE CASCADE
);

-- Índices para otimizar consultas na tabela Livro_Autor
CREATE INDEX IDX_Livro_Autor_Codl ON Livro_Autor (Codl);
CREATE INDEX IDX_Livro_Autor_CodAu ON Livro_Autor (CodAu);

-- Stored Procedure para inserir um Livro
CREATE PROCEDURE InserirLivro
    @Titulo VARCHAR(40),
    @Editora VARCHAR(40),
    @Edicao VARCHAR(40),
    @AnoPublicacao VARCHAR(4),
    @Preco DECIMAL(10, 2),
    @FormaCompra VARCHAR(50)
AS
BEGIN
    INSERT INTO Livro (Titulo, Editora, Edicao, AnoPublicacao, Preco, FormaCompra)
    VALUES (@Titulo, @Editora, @Edicao, @AnoPublicacao, @Preco, @FormaCompra);
END;
GO

-- Stored Procedure para inserir um Assunto
CREATE PROCEDURE InserirAssunto
    @Descricao VARCHAR(20)
AS
BEGIN
    INSERT INTO Assunto (Descricao) VALUES (@Descricao);
END;
GO

-- Stored Procedure para inserir um Autor
CREATE PROCEDURE InserirAutor
    @Nome NVARCHAR(40)
AS
BEGIN
    INSERT INTO Autor (Nome) VALUES (@Nome);
END;
GO

--Script SQL para Criar a Stored Procedure de Atualização
CREATE PROCEDURE AtualizarAutor
    @CodAu INT,
    @Nome NVARCHAR(255)
AS
BEGIN
    UPDATE Autor
    SET Nome = @Nome
    WHERE CodAu = @CodAu;
END;


--Script SQL para Criar a Stored Procedure de Deleção
CREATE PROCEDURE DeletarAutor
    @CodAu INT
AS
BEGIN
    DELETE FROM Autor
    WHERE CodAu = @CodAu;
END;

--Script SQL para Criar a Stored Procedure de Listagem
CREATE PROCEDURE ListarAutores
AS
BEGIN
    SELECT CodAu, Nome
    FROM Autor;
END;

--Script SQL para Criar a Stored Procedure de Obtenção por ID
CREATE PROCEDURE ObterAutorPorId
    @CodAu INT
AS
BEGIN
    SELECT CodAu, Nome
    FROM Autor
    WHERE CodAu = @CodAu;
END;


-- Script SQL para Criar a Stored Procedure de Listagem
CREATE PROCEDURE ListarAssuntos
AS
BEGIN
    SELECT CodAs, Descricao
    FROM Assunto;
END;


 --Script SQL para Criar a Stored Procedure de Obtenção por ID
CREATE PROCEDURE ObterAssuntoPorId
    @CodAssunto INT
AS
BEGIN
    SELECT CodAs, Descricao
    FROM Assunto
    WHERE CodAs = @CodAssunto;
END;

--Script SQL para Criar a Stored Procedure de Atualização
CREATE PROCEDURE AtualizarAssunto
    @CodAssunto INT,
    @Descricao NVARCHAR(255)
AS
BEGIN
    UPDATE Assunto
    SET Descricao = @Descricao
    WHERE CodAs = @CodAssunto;
END;

--Script SQL para Criar a Stored Procedure de Deleção
CREATE PROCEDURE DeletarAssunto
    @CodAssunto INT
AS
BEGIN
    DELETE FROM Assunto
    WHERE CodAs = @CodAssunto;
END;

-- Stored Procedure para vincular Livro e Assunto
CREATE PROCEDURE VincularLivroAssunto
    @Codl INT,
    @CodAs INT
AS
BEGIN
    INSERT INTO Livro_Assunto (Codl, CodAs) VALUES (@Codl, @CodAs);
END;
GO

-- Stored Procedure para vincular Livro e Autor
CREATE PROCEDURE VincularLivroAutor
    @Codl INT,
    @CodAu INT
AS
BEGIN
    INSERT INTO Livro_Autor (Codl, CodAu) VALUES (@Codl, @CodAu);
END;
GO

-- Stored Procedure para listar livros
CREATE PROCEDURE ListarLivros
AS
BEGIN
    SELECT 
        Codl,
        Titulo,
        Editora,
        Edicao,
        AnoPublicacao,
        Preco,
        FormaCompra
    FROM Livro;
END;
GO

-- Stored Procedure para listar livros com seus assuntos
CREATE PROCEDURE ListarLivrosComAssuntos
AS
BEGIN
    SELECT 
        L.Codl,
        L.Titulo,
        L.Editora,
        L.Edicao,
        L.AnoPublicacao,
        L.Preco,
        L.FormaCompra,
        A.Descricao AS Assunto
    FROM Livro L
    INNER JOIN Livro_Assunto LA ON L.Codl = LA.Codl
    INNER JOIN Assunto A ON LA.CodAs = A.CodAs;
END;
GO

-- Stored Procedure para listar livros com seus autores
CREATE PROCEDURE ListarLivrosComAutores
AS
BEGIN
    SELECT 
        L.Codl,
        L.Titulo,
        L.Editora,
        L.Edicao,
        L.AnoPublicacao,
        L.Preco,
        L.FormaCompra,
        Au.Nome AS Autor
    FROM Livro L
    INNER JOIN Livro_Autor LA ON L.Codl = LA.Codl
    INNER JOIN Autor Au ON LA.CodAu = Au.CodAu;
END;
GO

-- Stored Procedure para obter um livro por ID
CREATE PROCEDURE ObterLivroPorId
    @Codl INT
AS
BEGIN
    SELECT 
        Codl,
        Titulo,
        Editora,
        Edicao,
        AnoPublicacao,
        Preco,
        FormaCompra
    FROM Livro
    WHERE Codl = @Codl;
END;
GO

-- Stored Procedure para atualizar um Livro
CREATE PROCEDURE AtualizarLivro
    @Codl INT,
    @Titulo VARCHAR(40),
    @Editora VARCHAR(40),
    @Edicao VARCHAR(40),
    @AnoPublicacao VARCHAR(4),
    @Preco DECIMAL(10, 2),
    @FormaCompra VARCHAR(50)
AS
BEGIN
    UPDATE Livro
    SET Titulo = @Titulo,
        Editora = @Editora,
        Edicao = @Edicao,
        AnoPublicacao = @AnoPublicacao,
        Preco = @Preco,
        FormaCompra = @FormaCompra
    WHERE Codl = @Codl;
END;
GO

-- Stored Procedure para deletar um Livro
CREATE PROCEDURE DeletarLivro
    @Codl INT
AS
BEGIN
    DELETE FROM Livro
    WHERE Codl = @Codl;
END;
GO
select * from Livro_Autor
--Stored Procedures para insert Livro_Autor
CREATE PROCEDURE spCreateLivroAutor
    @CodL INT,
    @CodAu INT
AS
BEGIN
    INSERT INTO Livro_Autor (CodL, CodAu)
    VALUES (@CodL, @CodAu);
END

--Stored Procedures para update Livro_Autor
CREATE PROCEDURE spUpdateLivroAutor 
    @CodL INT,
    @CodAu INT
AS
BEGIN
    UPDATE Livro_Autor
    SET CodAu = @CodAu
    WHERE CodL = @CodL;
END


CREATE PROCEDURE spDeleteLivro
    @codL INT
AS
BEGIN
    
	   DELETE FROM Livro WHERE CodL = @codL;
END

--Stored Procedures para delete Livro_Autor
CREATE PROCEDURE spDeleteLivroAutor
    @CodL INT
AS
BEGIN
    DELETE FROM Livro_Autor
    WHERE CodL = @CodL;
END

--Stored Procedures para insert Livro_Assunto
CREATE PROCEDURE spCreateLivroAssunto
    @CodL INT,
    @CodAs INT
AS
BEGIN
    INSERT INTO Livro_Assunto (CodL, CodAs)
    VALUES (@CodL, @CodAs);
END

--Stored Procedures para Update Livro_Assunto
CREATE PROCEDURE spUpdateLivroAssunto 
    @CodL INT,
    @CodAs INT
AS
BEGIN
    UPDATE Livro_Assunto
    SET CodAs = @CodAs
    WHERE CodL = @CodL;
END

--Stored Procedures para Delete Livro_Assunto
CREATE PROCEDURE dbo.spDeleteLivroAssunto
    @CodL INT
AS
BEGIN
    DELETE FROM Livro_Assunto
    WHERE CodL = @CodL;
END



CREATE PROCEDURE spGetLivros
AS
BEGIN
    SELECT l.CodL, l.Titulo, l.Editora, l.Edicao, l.AnoPublicacao, l.Preco, l.FormaCompra, 
           a.CodAu, a.Nome, s.CodAs, s.Descricao
    FROM Livro l
    LEFT JOIN Livro_Autor la ON l.CodL = la.CodL
    LEFT JOIN Autor a ON la.CodAu = a.CodAu
    LEFT JOIN Livro_Assunto ls ON l.CodL = ls.CodL
    LEFT JOIN Assunto s ON ls.CodAs = s.CodAs;
END

create PROCEDURE spGetLivroById
    @CodL INT
AS
BEGIN
    SELECT l.CodL, l.Titulo, l.Editora, l.Edicao, l.AnoPublicacao, l.Preco, l.FormaCompra, 
           a.CodAu, a.Nome, s.CodAs, s.Descricao
    FROM Livro l
    LEFT JOIN Livro_Autor la ON l.CodL = la.CodL
    LEFT JOIN Autor a ON la.CodAu = a.CodAu
    LEFT JOIN Livro_Assunto ls ON l.CodL = ls.CodL
    LEFT JOIN Assunto s ON ls.CodAs = s.CodAs
    WHERE l.CodL = @CodL;
END

CREATE PROCEDURE spCreateLivro
    @Titulo NVARCHAR(100),
    @Editora NVARCHAR(100),
    @Edicao NVARCHAR(100),
    @AnoPublicacao NVARCHAR(4),
    @Preco DECIMAL(18, 2),
    @FormaCompra NVARCHAR(50)
AS
BEGIN
         -- Insere o livro
        INSERT INTO Livro (Titulo, Editora, Edicao, AnoPublicacao, Preco, FormaCompra)
        VALUES (@Titulo, @Editora, @Edicao, @AnoPublicacao, @Preco, @FormaCompra);

        -- Recupera o ID do livro recém-criado
        DECLARE @CodL INT;
        SET @CodL = SCOPE_IDENTITY();
        -- Retorna o código do livro recém-criado
        SELECT @CodL AS CodL ;
   
END




CREATE PROCEDURE dbo.spUpdateLivro
    @CodL INT,
    @Titulo NVARCHAR(100),
    @Editora NVARCHAR(100),
    @Edicao NVARCHAR(100),
    @AnoPublicacao NVARCHAR(4),
    @Preco DECIMAL(18, 2),
    @FormaCompra NVARCHAR(50)

AS
BEGIN
    UPDATE Livro
    SET Titulo = @Titulo,
        Editora = @Editora,
        Edicao = @Edicao,
        AnoPublicacao = @AnoPublicacao,
        Preco = @Preco,
        FormaCompra = @FormaCompra
    WHERE CodL = @CodL;

   
END

CREATE PROCEDURE spCheckAutorExists
    @CodAu INT
AS
BEGIN
    SELECT COUNT(1)
    FROM Autor
    WHERE CodAu = @CodAu;
END

CREATE PROCEDURE spCheckAssuntoExists
    @CodAssunto INT
AS
BEGIN
    SELECT COUNT(1)
    FROM Assunto
    WHERE CodAs = @CodAssunto;
END

CREATE VIEW vw_LivroRelatorio AS

BEGIN
SELECT 
    l.Codl AS LivroId,
    l.Titulo AS Titulo,
    l.Editora AS Editora,
    l.Edicao AS Edicao,
    l.AnoPublicacao AS AnoPublicacao,
    l.Preco AS Preco,
    l.FormaCompra AS FormaCompra,
    a.Nome AS AutorNome,
    asu.Descricao AS AssuntoDescricao
FROM 
    Livro l
JOIN 
    Livro_Autor la ON l.Codl = la.Codl
JOIN 
    Autor a ON la.CodAu = a.CodAu
JOIN 
    Livro_Assunto las ON l.Codl = las.Codl
JOIN 
    Assunto asu ON las.CodAs = asu.CodAs;
END
