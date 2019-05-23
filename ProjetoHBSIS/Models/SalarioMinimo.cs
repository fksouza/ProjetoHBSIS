using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHBSIS.Models
{
    public class SalarioMinimo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        [Display(Name = "Salário Mínimo")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        [Display(Name = "Ano")]                
        public string Ano { get; set; }

        public SalarioMinimo()
        {            
        }

        public SalarioMinimo(int id, double valor, string ano)
        {
            Id = id;
            Valor = valor;
            Ano = ano;
        }
    }
}
