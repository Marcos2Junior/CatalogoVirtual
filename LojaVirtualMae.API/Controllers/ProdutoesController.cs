using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LojaVirtualMae.Dominio.Entidades;
using LojaVirtualMae.Dominio.Interfaces;
using LojaVirtualMae.API.Modelos;
using AutoMapper;

namespace LojaVirtualMae.API.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoesController : ControllerBase
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IMapper _mapper;

        public ProdutoesController(IProdutoRepositorio produtoRepositorio, IMapper mapper)
        {
            _produtoRepositorio = produtoRepositorio;
            _mapper = mapper;
        }

        // GET: api/Produtoes
        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            try
            {
                var produtos = await _produtoRepositorio.ObterTodosAsync();
                if (produtos.Any())
                {
                    return Ok(_mapper.Map<ProdutoModelo[]>(produtos));
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return ErrorException(ex);
            }
        }

        // GET: api/Produtoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduto(int id)
        {
            try
            {
                var produto = await _produtoRepositorio.ObterPorIdAsync(id);

                if (produto == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ProdutoModelo>(produto));
            }
            catch (Exception ex)
            {
                return ErrorException(ex);
            }
        }

        // PUT: api/Produtoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            try
            {
                if (await _produtoRepositorio.Existe(id))
                {
                    await _produtoRepositorio.AtualizarAsync(produto);

                    return NoContent();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return ErrorException(ex);
            }
        }

        // POST: api/Produtoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostProduto(ProdutoModelo produtoModelo)
        {

            try
            {
                var produto = _mapper.Map<Produto>(produtoModelo);

                if (await _produtoRepositorio.AdicionarAsync(produto))
                {
                    var produtoModeloRetorno = _mapper.Map<ProdutoModelo>(produto);

                    return CreatedAtAction("GetProduto", new { id = produto.Id }, produtoModeloRetorno);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return ErrorException(ex);
            }
        }

        // DELETE: api/Produtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                var produto = await _produtoRepositorio.ObterPorIdAsync(id);

                if (produto != null)
                {
                    if (await _produtoRepositorio.DeletarAsync(produto))
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return ErrorException(ex);
            }
        }

        private IActionResult ErrorException(Exception exception)
        {
            //adiionar log

            return StatusCode(500, "Ocorreu um erro interno com o tratamento dos dados.");
        }
    }
}
