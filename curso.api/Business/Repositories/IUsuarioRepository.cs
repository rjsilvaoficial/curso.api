using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public interface IUsuarioRepository
    {
        public void Adicionar(Usuario usuario);
        public void Commit();
        public Usuario ObterUsuario(string login);
    }
}
