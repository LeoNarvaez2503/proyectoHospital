using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Login.Models;
using CapaDatos;
using CapaEntidad;
using CapaEntidad;
using Microsoft.AspNetCore.Authorization;

namespace Login.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [Authorize]
    public List<CitasCLS> ListarCitas()
    {
        CitasDAL objCitasDAL = new CitasDAL();
        return objCitasDAL.ListarCitas();
    }
}
