using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LojaVirtualMae.API.Classes;
using LojaVirtualMae.API.Modelos;
using LojaVirtualMae.Dominio.Entidades;
using LojaVirtualMae.Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LojaVirtualMae.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : AcessoController
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
                              IUsuarioRepositorio usuarioRepositorio,
                              ILogger<UserController> logger) : base(logger)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
            _repositorio = usuarioRepositorio;
        }
        /// <summary>
        /// Seleciona usuario que está autenticado
        /// </summary>
        /// <returns></returns>
        [HttpGet("auth")]
        public async Task<IActionResult> GetUserAuth()
        {
            int userId = 0;
            try
            {
                NewLog(nameof(GetUserAuth), 0);
                var usuario = await _userManager.GetUserAsync(User);

                userId = usuario != null ? usuario.Id : 0;

                NewLog(nameof(GetUserAuth), 4);
                var usuarioModel = _mapper.Map<UsuarioViewModel>(usuario);

                NewLog(nameof(GetUserAuth), 1);
                return Ok(usuarioModel);
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(GetUserAuth), userId);
            }
        }
        /// <summary>
        /// Selecionta todos os registros
        /// Definir Role Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                NewLog(nameof(GetAll), 0);
                var usuarios = await _repositorio.GetAllUsuariosAsync();
                if (usuarios.Any())
                {
                    NewLog(nameof(GetAll), 4, $"Total de {usuarios.Count} usuarios");
                    var usersModels = _mapper.Map<UsuarioLoginModelo[]>(usuarios);

                    NewLog(nameof(GetAll), 1);
                    return Ok(usersModels);
                }

                NewLog(nameof(GetAll), 2);
                return NotFound(new UsuarioInsertModel());
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(GetAll));
            }
        }

        /// <summary>
        /// Seleciona usuario por id
        /// Utilizar apenas quando for exibir detalhes sobre o usuario, ou seja qualquer usuario pode soliticar o perfil de outro usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            try
            {
                NewLog(nameof(GetUsuario), 0, $"Id: {id}");
                var usuario = await _repositorio.GetUsuarioByIdAsync(id);

                if (usuario == null)
                {
                    NewLog(nameof(GetUsuario), 2, $"Id: {id} nao identificado");
                    return NotFound();
                }

                NewLog(nameof(GetUsuario), 4, $"Id: {id}");
                var userModel = _mapper.Map<UsuarioViewModel>(usuario);

                NewLog(nameof(GetUsuario), 1, $"Id: {id}");
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(GetUsuario), id);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUsuario(int id, [FromBody] JsonPatchDocument<UsuarioInsertModel> pathUsuarioModelo)
        {
            try
            {
                NewLog(nameof(PatchUsuario), 0, $"Id: {id}");
                var usuario = await _repositorio.GetUsuarioByIdAsync(id);

                if (usuario != null)
                {
                    NewLog(nameof(PatchUsuario), 4, $"Id: {id}");
                    var usuarioModelo = _mapper.Map<UsuarioInsertModel>(usuario);
                    pathUsuarioModelo.ApplyTo(usuarioModelo);

                    usuario = _mapper.Map(usuarioModelo, usuario);

                    NewLog(nameof(PatchUsuario), 3, $"Id: {id} iniciando atualizar repositorio");
                    if (await _repositorio.AtualizarAsync(usuario))
                    {
                        NewLog(nameof(PatchUsuario), 1, $"Id: {id}");
                        return NoContent();
                    }

                    NewLog(nameof(PatchUsuario), 2, $"Id: {id} metodo atualizar repositorio retornou false");
                    return BadRequest();
                }

                NewLog(nameof(PatchUsuario), 2, $"Id: {id} nao identificado");
                return NotFound();
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(PatchUsuario), id);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioViewModel usuarioModelo)
        {
            try
            {
                NewLog(nameof(PutUsuario), 0, $"Id: {id}");
                var usuario = await _repositorio.GetUsuarioByIdAsync(id);

                if (usuario != null)
                {
                    NewLog(nameof(PutUsuario), 4, $"Id: {id}");
                    _ = _mapper.Map(usuarioModelo, usuario);

                    NewLog(nameof(PutUsuario), 3, $"Id: {id} iniciando metodo atualizar repositorio");
                    if (await _repositorio.AtualizarAsync(usuario))
                    {
                        NewLog(nameof(PutUsuario), 1, $"Id: {id}");
                        return NoContent();
                    }

                    NewLog(nameof(PutUsuario), 2, $"Id: {id} metodo repositorio retornou false");
                    return BadRequest();
                }

                NewLog(nameof(PutUsuario), 2, $"Id: {id} nao identificado");
                return NotFound();
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(PutUsuario), id);
            }
        }

        [HttpPost("upload")]
        [AllowAnonymous]
        public IActionResult Upload()
        {
            try
            {
                NewLog(nameof(Upload), 0);
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "images", "users");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());

                    NewLog(nameof(Upload), 3, "copiando arquivo");
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    NewLog(nameof(Upload), 1);
                    return Ok();
                }
                else
                {
                    NewLog(nameof(Upload), 2, "Nenhum arquivo identificado");
                    return BadRequest("Erro ao tentar realizar upload");
                }
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(Upload));
            }
        }

        private Usuario AlteraImage(Usuario usuario, bool excluir = false)
        {
            if (usuario.Imagem != null)
            {
                var fileInfo = new FileInfo(Path.Combine("Resources", "images", "users", usuario.Imagem));

                if (fileInfo.Exists)
                {
                    if (excluir)
                    {
                        System.IO.File.Delete(fileInfo.FullName);
                    }
                    else if (!string.IsNullOrEmpty(usuario.UserName))
                    {
                        usuario.Imagem = $"{usuario.UserName}{fileInfo.Extension}";
                        System.IO.File.Move(fileInfo.FullName, Path.Combine("Resources", "images", "users", usuario.Imagem));
                        System.IO.File.Delete(fileInfo.FullName);
                    }
                }
            }

            return usuario;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UsuarioInsertModel userModel)
        {
            try
            {
                NewLog(nameof(Register), 0);
                var user = _mapper.Map<Usuario>(userModel);

                NewLog(nameof(Register), 3, "Selecionando new userName");
                user.UserName = await _repositorio.GetNewUserNameAsync(userModel.Nome);
                user.DataCadastro = DateTime.Now;

                NewLog(nameof(Register), 3, "verificando cpf duplicado");
                bool CPFDuplicado = userModel.CPF != null && await _repositorio.GetUsuarioByCPFAsync(userModel.CPF) != null;

                IEnumerable<IdentityError> identityErrors = null;

                if (CPFDuplicado)
                {
                    identityErrors = new IdentityError[]
                    {
                        new IdentityError
                        {
                            Code = "DuplicateCPF",
                            Description = $"Ja existe registro do CPF {userModel.CPF} em nossa base de dados"
                        }
                    };
                }
                else
                {
                    NewLog(nameof(Register), 3, "AlteraImagem");
                    user = AlteraImage(user);

                    NewLog(nameof(Register), 3, "Criando novo usuario");
                    IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);


                    NewLog(nameof(Register), 4);
                    var userToReturn = _mapper.Map<UsuarioViewModel>(user);

                    if (result.Succeeded)
                    {
                        NewLog(nameof(Register), 1);
                        return Created("GetUser", userToReturn);
                    }

                    NewLog(nameof(Register), 3, "usuario nao criado, iniciando metodo remover imagem");

                    //exclui imagem
                    _ = AlteraImage(user, true);

                    identityErrors = result.Errors;
                }

                foreach (var item in identityErrors)
                {
                    switch (item.Code)
                    {
                        case "DuplicateEmail":
                            item.Description = $"Ja existe registro do email {userModel.Email} em nossa base de dados";
                            break;
                        default:
                            break;
                    }
                }

                NewLog(nameof(Register), 2);
                return BadRequest(identityErrors);
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(Register));
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UsuarioLoginModelo userLoginModel)
        {
            try
            {
                NewLog(nameof(Login), 0);
                bool GetByCPF = !userLoginModel.EmailCPF.Where(x => char.IsLetter(x)).Any();

                Usuario usuario = GetByCPF ?
                    await _repositorio.GetUsuarioByCPFAsync(userLoginModel.EmailCPF) :
                    await _userManager.FindByEmailAsync(userLoginModel.EmailCPF);

                if (usuario != null)
                {
                    NewLog(nameof(Login), 3, $"Id: {usuario.Id} verificando senha");
                    var result = await _signInManager.CheckPasswordSignInAsync(usuario, userLoginModel.Password, false);

                    if (result.Succeeded)
                    {
                        NewLog(nameof(Login), 1, $"Id: {usuario.Id}");
                        return Ok(new
                        {
                            token = GenerateJWToken(usuario).Result
                        });
                    }
                }

                NewLog(nameof(Login), 2, "nao autorizado");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(Login));
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

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
