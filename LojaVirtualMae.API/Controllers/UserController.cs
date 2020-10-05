using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LojaVirtualMae.API.Modelos;
using LojaVirtualMae.Dominio.Entidades;
using LojaVirtualMae.Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LojaVirtualMae.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _repositorio;

        public UserController(IConfiguration config,
                              UserManager<Usuario> userManager,
                              SignInManager<Usuario> signInManager,
                              IMapper mapper,
                              IUsuarioRepositorio usuarioRepositorio)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
            _repositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var usuarios = await _repositorio.GetAllUsuariosAsync();
                if (usuarios.Any())
                {
                    return Ok(_mapper.Map<UsuarioLoginModelo[]>(usuarios));
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return ErrorException(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            try
            {
                var usuario = await _repositorio.GetUsuarioByIdAsync(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<UsuarioModelo>(usuario));
            }
            catch (Exception ex)
            {
                return ErrorException(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioModelo usuarioModelo)
        {
            try
            {
                var usuario = await _repositorio.GetUsuarioByIdAsync(id);

                if (usuario != null)
                {
                    _ = _mapper.Map(usuarioModelo, usuario);

                    if (await _repositorio.AtualizarAsync(usuario))
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

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UsuarioModelo userModel)
        {
            try
            {
                var user = _mapper.Map<Usuario>(userModel);

                var result = await _userManager.CreateAsync(user, userModel.Password);

                var userToReturn = _mapper.Map<UsuarioModelo>(user);

                if (result.Succeeded)
                {
                    return Created("GetUser", userToReturn);
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return ErrorException(ex);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UsuarioLoginModelo userLoginModel)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userLoginModel.UserName);

                var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginModel.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users
                        .FirstOrDefaultAsync(u => u.NormalizedUserName == userLoginModel.UserName.ToUpper());

                    var userToReturn = _mapper.Map<UsuarioLoginModelo>(appUser);

                    return Ok(new
                    {
                        token = GenerateJWToken(appUser).Result,
                        user = userToReturn
                    });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return ErrorException(ex);
            }
        }

        private async Task<string> GenerateJWToken(Usuario user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private IActionResult ErrorException(Exception exception)
        {
            //adiionar log

            return StatusCode(500, "Ocorreu um erro interno com o tratamento dos dados.");
        }

    }
}
