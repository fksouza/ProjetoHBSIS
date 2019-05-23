using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoHBSIS.Services;

namespace ProjetoHBSIS.Controllers
{
    public class ImpostodeRendaController : Controller
    {
        private readonly ImpostodeRendaService _impostodeRendaService;

        public ImpostodeRendaController(ImpostodeRendaService impostodeRendaService)
        {
            _impostodeRendaService = impostodeRendaService;
        }

        public async Task<IActionResult> Index()
        {
            var listaImpostodeRenda = _impostodeRendaService.FindAllAsync();

            return View(await listaImpostodeRenda);
        }
    }
}