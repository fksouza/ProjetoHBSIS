using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoHBSIS.Models;
using ProjetoHBSIS.Models.ViewModels;
using ProjetoHBSIS.Services;
using SalesWebMvc.Services.Exceptions;

namespace ProjetoHBSIS.Controllers
{
    public class SalarioMinimoController : Controller
    {
        private readonly SalarioMinimoService _salarioMinimoService;

        public SalarioMinimoController(SalarioMinimoService salarioMinimoService)
        {
            _salarioMinimoService = salarioMinimoService;
        }

        public async Task<IActionResult> Index()
        {
            var listaSalarioMinimo = _salarioMinimoService.ListarSalarioMinimoAsync();

            return View(await listaSalarioMinimo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalarioMinimo salarioMinimo)
        {
            if (ModelState.IsValid)
            {
                await _salarioMinimoService.IncluirSalarioMinimoAsync(salarioMinimo);
                return RedirectToAction(nameof(Index));
            }
            return View(salarioMinimo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
            }

            var obj = await _salarioMinimoService.ListarSalarioMinimoPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SalarioMinimo salarioMinimo)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            if (id != salarioMinimo.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id incompatível!" });
            }
            try
            {
                await _salarioMinimoService.AtualizarSalarioMinimoAsync(salarioMinimo);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
            }

            var obj = await _salarioMinimoService.ListarSalarioMinimoPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });
            }

            return View(obj);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _salarioMinimoService.ExcluirSalarioMinimoAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}