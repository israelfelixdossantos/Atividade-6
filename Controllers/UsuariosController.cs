using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
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
        [HttpDelete("{id}")]
        public IActionResult Deleter(int id)
        {
            _usuarioRepository.BuscarPorId(id);
            _usuarioRepository.Deletar(id);
            return StatusCode(204);
        }
    }
}
