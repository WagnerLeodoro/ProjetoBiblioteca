using System.Collections.Generic;
using System.Linq;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {
            if (string.IsNullOrEmpty(controller.HttpContext.Session.GetString("user")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }
        public static bool verificaLoginSenha(string login, string senha, Controller controller)

        {
            using (BibliotecaContext bc = new BibliotecaContext())

            {
                verificaSeUsuarioAdminExiste(bc);//se nao tiver admin ele cria;

                Usuario u = new Usuario();
                u.Senha = senha;
                IQueryable<Usuario> usuario = bc.Usuarios.Where(u => u.Login == login && u.Senha == senha);
                List<Usuario> ListaUsuarios = usuario.ToList();

                if (ListaUsuarios.Count == 0)
                {
                    return false;//usuario nao encontrado
                }
                else//usuario encontrado e com atribuicao de elementos
                {
                    controller.HttpContext.Session.SetString("login", ListaUsuarios[0].Login);//tem a ver com o array/list;
                    controller.HttpContext.Session.SetString("nome", ListaUsuarios[0].Nome);
                    controller.HttpContext.Session.SetInt32("tipo", ListaUsuarios[0].Tipo);
                    return true;
                }

            }

        }

        public static void verificaSeUsuarioAdminExiste(BibliotecaContext bc)//verifica se existe, e caso nao cria.
        {

            IQueryable<Usuario> usuario = bc.Usuarios.Where(u => u.Login == "admin");
            if (usuario.ToList().Count == 0)

            {

                Usuario admin = new Usuario();
                admin.Login = "admin";
                admin.Senha = "123";
                admin.Tipo = 0;
                admin.Nome = "Administrador";
                bc.Usuarios.Add(admin);
                bc.SaveChanges();
            }

        }

        public static void verificaSeUsuarioEAdmin(Controller controller)//verifica se é o adm;

        {
            if (!(controller.HttpContext.Session.GetInt32("tipo") == 0))

            {
                controller.Request.HttpContext.Response.Redirect("/Usuario/LoginError");//Redireciona para essa Controller/pagina caso o usuario nao seja o adm;
            }
        }
    }
}