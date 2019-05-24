using ProjetoHBSIS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHBSIS.Models
{
    public class ImpostodeRenda
    {
        public int Id { get; set; }

        [Display(Name = "Imposto de Renda")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Valor { get; set; }

        public Contribuinte Contribuinte { get; set; }
        public int ContribuinteId { get; set; }
        public SalarioMinimo SalarioMinimo { get; set; }
        public int SalarioMinimoId { get; set; }

        public ImpostodeRenda()
        {
        }

        public ImpostodeRenda(double valor, SalarioMinimo salarioMinimo)
        {
            Valor = valor;
            SalarioMinimo = salarioMinimo;                        
        }

    }
}
