using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public Usuario ListarPorId(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }
        public List<Usuario> Listar()
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
        }
        public void Inserir(Usuario u)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Add(u);
                bc.SaveChanges();
            }
        }
        public void Editar(Usuario editU)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario u = ListarPorId(editU.Id);
                u.Login = editU.Login;
                u.Nome = editU.Nome;
                u.Senha = editU.Senha;
                u.Tipo = editU.Tipo;
                bc.SaveChanges();
            }
        }
        public void Excluir(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Remove(ListarPorId(id));
                bc.SaveChanges();
            }
        }
    }
}