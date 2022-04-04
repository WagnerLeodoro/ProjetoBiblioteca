using System;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Listagem()
        {
            UsuarioService Lista = new UsuarioService();
            return View(Lista.Listar());
        }
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(Usuario u)
        {
            UsuarioService us = new UsuarioService();
            u.Senha = u.Senha;
            us.Inserir(u);
            return RedirectToAction("Listagem");
        }
        public IActionResult Editar(int id)
        {
            Usuario u = new UsuarioService().ListarPorId(id);
            return View(u);
        }
        [HttpPost]
        public IActionResult Editar(Usuario editU)
        {
            UsuarioService us = new UsuarioService();
            us.Editar(editU);
            return RedirectToAction("Listagem");
        }
        public IActionResult Excluir(int id)
        {
            return View(new UsuarioService().ListarPorId(id));
        }
        [HttpPost]
        public IActionResult Excluir(string decisao, int Id)
        {
            if (decisao.ToLower() == "excluir")
            {
                ViewData["Mensagem"] = "Exclusão do usuário " + new UsuarioService().ListarPorId(Id).Nome + " realiza com sucesso";
                new UsuarioService().Excluir(Id);
                return RedirectToAction("Listagem");
            }
            else
            {
                ViewData["Mensagem"] = "Exclusao cancelada";
                return RedirectToAction("ListaDeUsuarios");
            }
        }
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return View("../Home/Login");
        }
    }

}