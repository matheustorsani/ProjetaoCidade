using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositorio;

namespace WebApplication1.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly LoginRepositorio _loginRepositorio;

        public UsuarioController(LoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _loginRepositorio.ObterUsuario(email);
            if (usuario != null && usuario.senha == senha)
            {
                return RedirectToAction("Index", "Produto");
            }

            ModelState.AddModelError("", "Email ou senha inválida!");
            return View();
        }
    }
}