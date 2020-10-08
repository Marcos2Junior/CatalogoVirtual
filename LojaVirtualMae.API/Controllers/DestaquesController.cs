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

namespace LojaVirtualMae.API.Controllers
{
    [Route("api/destaque")]
    [ApiController]
    public class DestaquesController : ControllerBase
    {
        private readonly LojaVirtualDbContexto _context;
        private readonly IMapper _mapper;

        public DestaquesController(LojaVirtualDbContexto context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Categorias
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<DestaqueModelo>>> GetDestaques()
        {
            try
            {
                return Ok(_mapper.Map<List<DestaqueModelo>>(await _context.Destaques.ToListAsync()));    
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Houve um erro ao processar os dados");
            }
        }
    }
}
