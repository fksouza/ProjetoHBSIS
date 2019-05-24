using Microsoft.EntityFrameworkCore;
using ProjetoHBSIS.Models;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHBSIS.Services
{
    public class ContribuinteService
    {
        private readonly ProjetoHBSISContext _context;
        private readonly SalarioMinimoService _salarioMinimoService;
        
        public int PercDesconto;

        public ContribuinteService(ProjetoHBSISContext context, SalarioMinimoService salarioMinimoService)
        {
            _context = context;            
            _salarioMinimoService = salarioMinimoService;
            PercDesconto = 5;
        }

        public async Task<List<Contribuinte>> FindAllAsync()
        {
            return await _context.Contribuinte.ToListAsync();
        }

        public async Task InsertAsync(Contribuinte obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Contribuinte> FindByIdAsync(int id)
        {
            return await _context.Contribuinte.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
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

        public async Task UpdateAsync(Contribuinte obj)
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

        public double CalculaDescontoPorDependentes(Contribuinte contribuinte)
        {
            var perctotal = contribuinte.NumeroDepentedentes * PercDesconto;
            var ValorSalarioMinimo = _salarioMinimoService.ValorSalarioMinimo();
            return (ValorSalarioMinimo * perctotal) / 100;
        }

        public double CalculaRendaLiquida(Contribuinte contribuinte)
        {
            double rendaLiquida = contribuinte.RendaBrutaMensal - CalculaDescontoPorDependentes(contribuinte);

            return rendaLiquida;
        }
    }
}
