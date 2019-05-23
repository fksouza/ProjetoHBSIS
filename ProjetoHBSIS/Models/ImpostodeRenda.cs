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
        private double rendaliquida = 0;

        public ImpostodeRenda()
        {
        }

        public ImpostodeRenda(SalarioMinimo salarioMinimo, Contribuinte contribuinte)
        {
            SalarioMinimo = salarioMinimo;
            Contribuinte = contribuinte;
            //rendaliquida = Contribuinte.RendaLiquida();
        }

        public double CalcularIR()
        {
            return rendaliquida * CalcularAliquota();

        }

        public double CalcularAliquota()
        {
            double aliquota = 0;

            if ((SalarioMinimo.Valor * 2) <= rendaliquida)
            {
                aliquota = 0;
            }
            else if ((SalarioMinimo.Valor * 2) > rendaliquida && (SalarioMinimo.Valor * 4) <= rendaliquida)
            {
                aliquota = rendaliquida * 0.075;
            }
            else if ((SalarioMinimo.Valor * 4) > rendaliquida && (SalarioMinimo.Valor * 5) <= rendaliquida)
            {
                aliquota = rendaliquida * 0.15;
            }
            else if ((SalarioMinimo.Valor * 5) > rendaliquida && (SalarioMinimo.Valor * 7) <= rendaliquida)
            {
                aliquota = rendaliquida * 0.225;
            }
            else if ((SalarioMinimo.Valor * 7) > rendaliquida)
            {
                aliquota = rendaliquida * 0.275;
            }

            return aliquota;
        }

    }
}
