using System;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Listagem()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            UsuarioService Lista = new UsuarioService();
            return View(Lista.Listar());
        }
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(Usuario u)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            UsuarioService us = new UsuarioService();
            u.Senha = Hash.Crypto(u.Senha);
            us.Inserir(u);
            return RedirectToAction("Listagem");
        }
        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            Usuario u = new UsuarioService().ListarPorId(id);
            return View(u);
        }
        [HttpPost]
        public IActionResult Edicao(Usuario editU)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            UsuarioService us = new UsuarioService();
            editU.Senha = Hash.Crypto(editU.Senha);
            us.Editar(editU);
            return RedirectToAction("Listagem");
        }
        public IActionResult Excluir(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
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
                return RedirectToAction("Listagem");
            }
        }
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/Login");
        }
        public IActionResult LoginError()
        {
            Autenticacao.CheckLogin(this);
            //Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }
    }

}