using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Exo.WebApi.Controllers


{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository
        _usuarioRepository;
        public UsuariosController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;//erro em usuarioRepository
        }
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_usuarioRepository.Listar());
        }
        [HttpPost("login")]
        public IActionResult Post(Usuario usuario)
        {
            Usuario usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);
            {
                if (usuarioBuscado == null)
                {
                    return NotFound("E-mail ou senha inválidos");
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString())
                };
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao-1234567890-strong-key"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "exo.webapi",
                    audience: "exo.webapi",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return Ok(
                    new { token = new JwtSecurityTokenHandler().WriteToken(token) }
                );

            }
        }
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario usuario)
        {
            _usuarioRepository.BuscarPorId(id);
            _usuarioRepository.Atualizar(id, usuario);
            return StatusCode(204);
            
        }
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            _usuarioRepository.Cadastrar(usuario);
            return StatusCode(204);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Deleter(int id)
        {
            _usuarioRepository.BuscarPorId(id);
            _usuarioRepository.Deletar(id);
            return StatusCode(204);
        }
    }
}
