using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Cross.Helpers;
using System.Collections.Generic;

namespace Modelo.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public void InserirLivro(LivroRequestModel livro)
        {
            try
            {
                ValidationHelper.ValidateString(livro.Titulo, "Título");
                ValidationHelper.ValidateString(livro.Editora, "Editora");
                ValidationHelper.ValidateString(livro.Edicao, "Edição");
                ValidationHelper.ValidateString(livro.AnoPublicacao, "Ano de Publicação");
                ValidationHelper.ValidateDecimalRange(livro.Preco, 0, 1000, "Preço");
                ValidationHelper.ValidateString(livro.FormaCompra, "Forma de Compra");

                _livroRepository.InserirLivro(livro);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao inserir o livro. Por favor, verifique os dados e tente novamente.", ex);
            }
        }

        public void AtualizarLivro(int codl, LivroRequestModel livro)
        {
            try
            {
                var livroExistente = _livroRepository.ObterLivroPorId(codl);
                if (livroExistente == null)
                {
                    throw new KeyNotFoundException("Livro não encontrado.");
                }

                // Atualizar apenas os campos fornecidos no LivroRequestModel
                var livroAtualizado = new LivroRequestModel
                {
                    Titulo = livro.Titulo ?? livroExistente.Titulo,
                    Editora = livro.Editora ?? livroExistente.Editora,
                    Edicao = livro.Edicao ?? livroExistente.Edicao,
                    AnoPublicacao = livro.AnoPublicacao ?? livroExistente.AnoPublicacao,
                    Preco = livro.Preco != 0 ? livro.Preco : livroExistente.Preco,
                    FormaCompra = livro.FormaCompra ?? livroExistente.FormaCompra
                };

                // Validar dados atualizados
                ValidationHelper.ValidateString(livroAtualizado.Titulo, "Título");
                ValidationHelper.ValidateString(livroAtualizado.Editora, "Editora");
                ValidationHelper.ValidateString(livroAtualizado.Edicao, "Edição");
                ValidationHelper.ValidateString(livroAtualizado.AnoPublicacao, "Ano de Publicação");
                ValidationHelper.ValidateDecimalRange(livroAtualizado.Preco, 0, 1000, "Preço");
                ValidationHelper.ValidateString(livroAtualizado.FormaCompra, "Forma de Compra");

                _livroRepository.AtualizarLivro(codl, livroAtualizado);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o livro. Por favor, verifique os dados e tente novamente.", ex);
            }
        }

        public void DeletarLivro(int codl)
        {
            try
            {
                _livroRepository.DeletarLivro(codl);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao deletar o livro. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public LivroResponseModel ObterLivroPorId(int codl)
        {
            try
            {
                return _livroRepository.ObterLivroPorId(codl);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter os detalhes do livro. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public IEnumerable<LivroResponseModel> ListarLivros()
        {
            try
            {
                return _livroRepository.ListarLivros();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao listar os livros. Por favor, tente novamente mais tarde.", ex);
            }
        }
    }
}
