using Microsoft.EntityFrameworkCore;
using ProjetoHBSIS.Models;
using SalesWebMvc.Services.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoHBSIS.Services
{
    public class ContribuinteService
    {
        private readonly ProjetoHBSISContext _context;
        private readonly SalarioMinimoService _salarioMinimoService;

        //Pecentual de 5% de desconto por dependente.
        public int PercDesconto;

        public ContribuinteService()
        {
        }

        public ContribuinteService(ProjetoHBSISContext context, SalarioMinimoService salarioMinimoService)
        {
            _context = context;
            _salarioMinimoService = salarioMinimoService;
            PercDesconto = 5;
        }

        public async Task<List<Contribuinte>> ListarContribuinteAsync()
        {
            return await _context.Contribuinte.ToListAsync();
        }

        public async Task InsereContribuinteAsync(Contribuinte obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Contribuinte> ListarContribuintePorIdAsync(int id)
        {
            return await _context.Contribuinte.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task ExcluirContribuinteAsync(int id)
        {
            try
            {
                var obj = await _context.Contribuinte.FindAsync(id);
                _context.Contribuinte.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Não foi possível deletar o contribuinte!" + e.Message);
            }
        }

        public async Task AtualizarContribuinteAsync(Contribuinte obj)
        {
            bool hasAny = await _context.Contribuinte.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado!");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public double CalculaDescontoPorDependentes(Contribuinte contribuinte, double salarioMinimo)
        {
            var percTotal = contribuinte.NumeroDepentedentes * PercDesconto;                        
            double descontoTotal = (salarioMinimo * percTotal) / 100;

            return descontoTotal;
        }

        public double CalculaRendaLiquida(Contribuinte contribuinte, double salarioMinimo)
        {
            double rendaLiquida = contribuinte.RendaBrutaMensal - CalculaDescontoPorDependentes(contribuinte, salarioMinimo);

            return rendaLiquida;
        }
    }
}
