using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaVirtualMae.Dominio.DbContexto;
using LojaVirtualMae.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using LojaVirtualMae.API.Modelos;
using AutoMapper;
using LojaVirtualMae.Dominio.Interfaces;

namespace LojaVirtualMae.API.Controllers
{
    [Route("api/categoria")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepositorio _repo;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Categorias
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetCategorias()
        {
            try
            {
                var categorias = await _repo.SelecionarTodosAsync<Categoria>();
                return Ok(_mapper.Map<CategoriaModelo[]>(categorias));
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Houve um erro com o processamento dos dados");
            }
        }

         [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoria(int id)
        {
            try
            {
                var categoria = await _repo.GetCategoriaByIdAsync(id);

                if (categoria == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<CategoriaModelo>(categoria));
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro com o processamento dos dados");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCategorias(CategoriaModelo categoriaModelo) {

            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaModelo);

                if(await _repo.AdicionarAsync(categoria))
                {
                     var categoriaRetorno = _mapper.Map<CategoriaModelo>(categoria);

                    return CreatedAtAction("GetCategorias", new { id = categoria.Id }, categoriaRetorno);
                }

                return BadRequest();
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Houve um erro com o processamento dos dados");
            }
        }
    }
}
