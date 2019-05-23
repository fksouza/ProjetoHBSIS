using Microsoft.EntityFrameworkCore;
using ProjetoHBSIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHBSIS.Services
{
    public class ImpostodeRendaService
    {
        private readonly ProjetoHBSISContext _context;

        public ImpostodeRendaService(ProjetoHBSISContext context)
        {
            _context = context;
        }

        public async Task<List<ImpostodeRenda>> FindAllAsync()
        {
            return await _context.ImpostodeRenda.ToListAsync();
        }

        public async Task InsertAsync(ImpostodeRenda obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<ImpostodeRenda> FindByIdAsync(int id)
        {
            return await _context.ImpostodeRenda.FirstOrDefaultAsync(obj => obj.Id == id);
        }
    }
}
