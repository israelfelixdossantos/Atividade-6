using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exo.WebApi.Repositories
{
    public class ProjetoRepository
    {
        private readonly ExoContext _context;
        public ProjetoRepository(ExoContext context)
        {
            _context = context;
        }
        public List<Projeto> Listar()
        {
            return _context.Projetos.ToList();
        }
        public Projeto BuscarPorId(int id)
        {
            return _context.Projetos.Find(id);
        }
        public void Atualizar(int id, Projeto projetoAtualizado)
        {
            var projeto = _context.Projetos.Find(id);           
            
            projeto.NomeDoProjeto = projetoAtualizado.NomeDoProjeto;
            projeto.Area = projetoAtualizado.Area;
            projeto.Status = projetoAtualizado.Status;
            
            _context.SaveChanges();

        }
        public void Cadastra(Projeto projetoAtualizado)
        {
            _context.Projetos.Add(projetoAtualizado);

            _context.SaveChanges();

        }
        public void Deletar(int id)
        {

            var projetoBuscado = _context.Projetos.Find(id);

            _context.Projetos.Remove(projetoBuscado);

            _context.SaveChanges();
        }
    }
}