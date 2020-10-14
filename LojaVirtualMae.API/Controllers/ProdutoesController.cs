using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LojaVirtualMae.Dominio.Entidades;
using LojaVirtualMae.Dominio.Interfaces;
using LojaVirtualMae.API.Modelos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using LojaVirtualMae.API.Classes;

namespace LojaVirtualMae.API.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoesController : AcessoController
    {
        private readonly IProdutoRepositorio _repositorio;
        private readonly IMapper _mapper;

        public ProdutoesController(IProdutoRepositorio produtoRepositorio, IMapper mapper, ILogger<ProdutoesController> logger) : base(logger)
        {
            _repositorio = produtoRepositorio;
            _mapper = mapper;
        }

        // GET: api/Produtoes
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProdutos()
        {
            try
            {
                NewLog(nameof(GetProdutos), 0);
                var produtos = await _repositorio.GetAllProdutosAsync();
                if (produtos.Any())
                {
                    NewLog(nameof(GetProdutos), 4, $"Total de {produtos.Count} produtos");
                    var produtosModelos = _mapper.Map<ProdutoModelo[]>(produtos);

                    NewLog(nameof(GetProdutos), 1);
                    return Ok(produtosModelos);
                }

                NewLog(nameof(GetProdutos), 2, "Nenhum produto encontrado");
                return NotFound();
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(GetProdutos));
            }
        }

        // GET: api/Produtoes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProduto(int id)
        {
            try
            {
                NewLog(nameof(GetProduto), 0, $"Id:{id}");
                var produto = await _repositorio.GetProdutoByIdAsync(id);

                if (produto == null)
                {
                    NewLog(nameof(GetProduto), 2, $"Id: {id} nao encontrado");
                    return NotFound();
                }

                NewLog(nameof(GetProduto), 1, $"Id: {id}");
                return Ok(_mapper.Map<ProdutoModelo>(produto));
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(GetProduto), id);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, ProdutoModelo produtoModelo)
        {
            try
            {
                NewLog(nameof(PutProduto), 1, $"Id: {id}");
                var produto = await _repositorio.GetProdutoByIdAsync(id);

                if (produto != null)
                {
                    NewLog(nameof(PutProduto), 3, $"Id: {id} iniciando mapeamento");
                    _ = _mapper.Map(produtoModelo, produto);

                    NewLog(nameof(PutProduto), 3, $"Id: {id} iniciando repositorio atualizar");
                    if (await _repositorio.AtualizarAsync(produto))
                    {
                        NewLog(nameof(PutProduto), 1, $"Id: {id}");
                        return NoContent();
                    }
                    else
                    {
                        NewLog(nameof(PutProduto), 2, $"Id: {id} metodo repositorio retornou false");
                        return BadRequest();
                    }
                }

                NewLog(nameof(PutProduto),2, $"Id: {id} nao encontrado");
                return NotFound();
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(PutProduto), id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostProduto(ProdutoModelo produtoModelo)
        {
            try
            {
                NewLog(nameof(PostProduto), 0);
                var produto = _mapper.Map<Produto>(produtoModelo);
                produto.DataCadastro = DateTime.Now;
                produto.CategoriaId = produto.Categoria.Id;
                produto.Categoria = null;

                NewLog(nameof(PostProduto), 3, "iniciando metodo adicionar repositorio");
                if (await _repositorio.AdicionarAsync(produto))
                {
                    NewLog(nameof(PostProduto), 3, "Adicionado com sucesso, fazendo o mapeamento.");
                    var produtoModeloRetorno = _mapper.Map<ProdutoModelo>(produto);

                    NewLog(nameof(PostProduto), 1);
                    return CreatedAtAction("GetProduto", new { id = produto.Id }, produtoModeloRetorno);
                }

                NewLog(nameof(PostProduto), 2, "Metodo adicionar repositorio retornou false");
                return BadRequest();
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(PostProduto));
            }
        }

        // DELETE: api/Produtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                NewLog(nameof(DeleteProduto), 0, $"Id: {id}");
                var produto = await _repositorio.GetProdutoByIdAsync(id);

                if (produto != null)
                {
                    NewLog(nameof(DeleteProduto), 3, $"Id: {id} iniciando metodo deletar repositorio");
                    if (await _repositorio.DeletarAsync(produto))
                    {
                        NewLog(nameof(DeleteProduto), 1, $"Id: {id}");
                        return NoContent();
                    }
                    else
                    {
                        NewLog(nameof(DeleteProduto), 2, $"Id: {id} metodo deletar repositorio retornou false");
                        return BadRequest();
                    }
                }

                NewLog(nameof(DeleteProduto), 2, $"Id: {id} nao identificado");
                return NotFound();
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(DeleteProduto), id);
            }
        }
    }
}
