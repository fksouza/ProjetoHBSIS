using Microsoft.EntityFrameworkCore;
using ProjetoHBSIS.Models;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHBSIS.Services
{
    public class ImpostodeRendaService
    {
        private readonly ProjetoHBSISContext _context;
        private readonly ContribuinteService _contribuinteService;
        private readonly SalarioMinimoService _salarioMinimoService;

        public ImpostodeRendaService()
        {
        }

        public ImpostodeRendaService(ProjetoHBSISContext context, ContribuinteService contribuinteService, SalarioMinimoService salarioMinimoService)
        {
            _context = context;
            _contribuinteService = contribuinteService;
            _salarioMinimoService = salarioMinimoService;
        }

        public async Task<List<ImpostodeRenda>> ListarImpostodeRendaAsync()
        {
            return await _context.ImpostodeRenda.ToListAsync();
        }

        public async Task IncluirImpostodeRendaAsync(ImpostodeRenda obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirImpostodeRendaAsync(List<ImpostodeRenda> impostodeRenda)
        {
            try
            {
                _context.ImpostodeRenda.RemoveRange(impostodeRenda);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task<List<ImpostodeRenda>> CalcularImpostodeRenda()
        {
            try
            {
                //Remove todos os itens da tabela Imposto de Renda e recalcula.
                await ExcluirImpostodeRenda();

                var impostodeRenda = new ImpostodeRenda();

                var salarioMinimo = _salarioMinimoService.ListarSalarioMinimoAsync();
                int salarioMinimoId = salarioMinimo.Result.Select(sl => sl.Id).FirstOrDefault();
                double salarioMinimoValor = salarioMinimo.Result.Select(sl => sl.Valor).Sum();

                var listaContribuinte = _contribuinteService.ListarContribuinteAsync();

                for (int i = 0; i < listaContribuinte.Result.Count; i++)
                {
                    //Inicia o cálculo do Imposto de renda do contribuinte.
                    impostodeRenda = new ImpostodeRenda { ContribuinteId = listaContribuinte.Result[i].Id, SalarioMinimoId = salarioMinimoId, Valor = CalcularAliquota(listaContribuinte.Result[i], salarioMinimoValor) };
                    await IncluirImpostodeRendaAsync(impostodeRenda);
                }

                var listaImpostodeRenda = ListarImpostodeRendaAsync();

                return await listaImpostodeRenda;
            }
            catch (ApplicationException e)
            {

                throw new ApplicationException(e.Message);
            }

        }

        public double CalcularAliquota(Contribuinte contribuinte, double salarioMinimo)
        {
            double aliquota = 0;
            double rendaliquida = _contribuinteService.CalculaRendaLiquida(contribuinte, salarioMinimo);

            if ((salarioMinimo * 2) >= rendaliquida)
            {
                aliquota = 0;
            }
            else if (rendaliquida > (salarioMinimo * 2) && rendaliquida <= (salarioMinimo * 4))
            {
                aliquota = rendaliquida * 0.075;
            }
            else if (rendaliquida > (salarioMinimo * 4) && rendaliquida <= (salarioMinimo * 5))
            {
                aliquota = rendaliquida * 0.15;
            }
            else if (rendaliquida > (salarioMinimo * 5) && rendaliquida <= (salarioMinimo * 7))
            {
                aliquota = rendaliquida * 0.225;
            }
            else if (rendaliquida > (salarioMinimo * 7))
            {
                aliquota = rendaliquida * 0.275;
            }

            return aliquota;
        }

        public async Task<List<ImpostodeRenda>> ListaImpostodeRenda()
        {
            return await CalcularImpostodeRenda();
        }

        public async Task ExcluirImpostodeRenda()
        {
            try
            {
                var listaImpostodeRenda = await ListarImpostodeRendaAsync();
                await ExcluirImpostodeRendaAsync(listaImpostodeRenda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
