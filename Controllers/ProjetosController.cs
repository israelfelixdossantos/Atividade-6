using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
namespace Exo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly ProjetoRepository
        _projetoRepository;
        public ProjetosController(ProjetoRepository
        projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_projetoRepository.Listar());
        }
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_projetoRepository.BuscarPorId(id));
        }
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Projeto projeto)
        {
            _projetoRepository.BuscarPorId(id);
            _projetoRepository.Atualizar(id, projeto);
            return StatusCode(204);
            
        }
        [HttpPost]
        public IActionResult Cadastra(Projeto projeto)
        {
            _projetoRepository.Cadastra(projeto);
            return StatusCode(204);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projetoRepository.BuscarPorId(id);
            _projetoRepository.Deletar(id);
            return StatusCode(204);
        }
    }
}
