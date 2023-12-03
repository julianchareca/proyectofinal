using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_CreandoLuz_PF.Models;

namespace TP_CreandoLuz_PF.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult CerrarSesion()
    {
    HttpContext.Session.Clear();
    return RedirectToAction("Login");
    }

    public IActionResult Admin()
    {
        var userMail = HttpContext.Session.GetString("UserMail");
        List<Form> forms = BD.ObtenerForms();
        return View(forms);
    }

    public IActionResult Login()
    {
        var userMail = HttpContext.Session.GetString("UserMail");
        var userNombre = HttpContext.Session.GetString("UserNombre");
        var userApellido = HttpContext.Session.GetString("UserApellido");
        var userTelefono = HttpContext.Session.GetString("UserTelefono");
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        Usuario usuario = BD.ObtenerUsuarioPorEmail(email);

        if (usuario != null && usuario.Password == password)
        {
            HttpContext.Session.SetString("UserMail", usuario.Email);
            HttpContext.Session.SetString("UserNombre", usuario.Nombre);
            HttpContext.Session.SetString("UserApellido", usuario.Apellido);
            HttpContext.Session.SetString("UserTelefono", usuario.Telefono);
            TempData["MensajeExito"] = "¡Inicio de sesión exitoso!";
            return RedirectToAction("Index", "Home");
        }
        TempData["MensajeError"] = "Los datos ingresados son incorrectos.";
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(Usuario usuario)
    {
        bool registrado = BD.RegistrarUsuario(usuario);

        if (registrado)
        {
            HttpContext.Session.SetString("UserMail", usuario.Email);
            HttpContext.Session.SetString("UserNombre", usuario.Nombre);
            HttpContext.Session.SetString("UserApellido", usuario.Apellido);
            HttpContext.Session.SetString("UserTelefono", usuario.Telefono);
            TempData["MensajeExito"] = "¡Inicio de sesión exitoso!";
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Form(){
        return View();
    }
    
    public IActionResult Agradecer()
    {
        return View();
    }
    
    public IActionResult formDonar(){
        return View();
    }

    public IActionResult Guardar(Form Frm){
        BD.MandarForm(Frm);
        return RedirectToAction("Agradecer");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}