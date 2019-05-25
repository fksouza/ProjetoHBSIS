using System.ComponentModel.DataAnnotations;

namespace ProjetoHBSIS.Models
{
    public class ImpostodeRenda
    {
        public int Id { get; set; }

        [Display(Name = "Imposto de Renda")]
        [DataType(DataType.Currency)]
        public double Valor { get; set; }

        public Contribuinte Contribuinte { get; set; }
        public int ContribuinteId { get; set; }
        public SalarioMinimo SalarioMinimo { get; set; }
        public int SalarioMinimoId { get; set; }

        public ImpostodeRenda()
        {
        }

        public ImpostodeRenda(double valor)
        {
            Valor = valor;                                   
        }

    }
}
