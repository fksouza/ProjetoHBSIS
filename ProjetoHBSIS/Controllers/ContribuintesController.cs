using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoHBSIS.Models;
using ProjetoHBSIS.Models.ViewModels;
using ProjetoHBSIS.Services;
using SalesWebMvc.Services.Exceptions;

namespace ProjetoHBSIS.Controllers
{
    public class ContribuintesController : Controller
    {
        private readonly ContribuinteService _contribuinteService;

        public ContribuintesController(ContribuinteService contribuinteService)
        {
            _contribuinteService = contribuinteService;
        }
                
        public async Task<IActionResult> Index()
        {
            var listacontribuinte = _contribuinteService.ListarContribuinteAsync();

            return View(await listacontribuinte);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contribuinte contribuinte)
        {
            if (ModelState.IsValid)
            {
                await _contribuinteService.InsereContribuinteAsync(contribuinte);
                return RedirectToAction(nameof(Index));
            }
            return View(contribuinte);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
            }

            var obj = await _contribuinteService.ListarContribuintePorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contribuinte contribuinte)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            if (id != contribuinte.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id incompatível!" });
            }
            try
            {
                await _contribuinteService.AtualizarContribuinteAsync(contribuinte);
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

            var obj = await _contribuinteService.ListarContribuintePorIdAsync(id.Value);
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
                await _contribuinteService.ExcluirContribuinteAsync(id);
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
