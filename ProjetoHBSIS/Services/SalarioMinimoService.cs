using Microsoft.EntityFrameworkCore;
using ProjetoHBSIS.Models;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHBSIS.Services
{
    public class SalarioMinimoService
    {
        private readonly ProjetoHBSISContext _context;

        public SalarioMinimoService(ProjetoHBSISContext context)
        {
            _context = context;
        }

        public async Task<List<SalarioMinimo>> FindAllAsync()
        {
            return await _context.SalarioMinimo.ToListAsync();
        }

        public async Task InsertAsync(SalarioMinimo obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<SalarioMinimo> FindByIdAsync(int id)
        {
            return await _context.SalarioMinimo.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public double ValorSalarioMinimo()
        {
            return _context.SalarioMinimo.Sum(v => v.Valor);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.SalarioMinimo.FindAsync(id);
                _context.SalarioMinimo.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Não foi possível deletar o Salário Mínimo!" + e.Message);
            }
        }

        public async Task UpdateAsync(SalarioMinimo obj)
        {
            bool hasAny = await _context.SalarioMinimo.AnyAsync(x => x.Id == obj.Id);
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
       
    }
}
