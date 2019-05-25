using System.ComponentModel.DataAnnotations;

namespace ProjetoHBSIS.Models
{
    public class SalarioMinimo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} - Campo obrigatório!")]
        [Display(Name = "Salário Mínimo")]
        [DataType(DataType.Currency)]
        public double Valor { get; set; }

        [Required(ErrorMessage = "{0} - Campo obrigatório!")]
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
