using Microsoft.EntityFrameworkCore;
using SistemaDeTarefa.Data;
using SistemaDeTarefa.Models;
using SistemaDeTarefa.Repositorios.Interfaces;

namespace SistemaDeTarefa.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefaDBContex _dbContext;
        public UsuarioRepositorio(SistemaTarefaDBContex sistemaTarefaDBContex) 
        {
            _dbContext = sistemaTarefaDBContex;
        }
        public async Task<UsuarioModel> BuscarPorId(int Id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
           await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);
            
            if (usuarioPorId == null) 
            {
                throw new Exception($"Usuario para o ID: {id} nao foi encontrado no banco de dados");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioPorId);
           await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o ID: {id} nao foi encontrado no banco de dados");
            }

            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        
    }
}
