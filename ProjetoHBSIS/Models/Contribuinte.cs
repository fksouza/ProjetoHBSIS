using System.ComponentModel.DataAnnotations;

namespace ProjetoHBSIS.Models
{
    public class Contribuinte
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} - Campo obrigatório!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} o tamanho deve ser entre {2} and {1}!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} - Campo obrigatório!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "{0} - Campo obrigatório!")]
        [Display(Name = "Número de Dependentes")]
        public int NumeroDepentedentes { get; set; }

        [Required(ErrorMessage = "{0} - Campo obrigatório!")]
        [Display(Name = "Renda Bruta Mensal")]       
        [DataType(DataType.Currency)]        
        public double RendaBrutaMensal { get; set; }

        public Contribuinte()
        {
        }

        public Contribuinte(string nome, string cpf, int numeroDepentedentes, double rendaBrutaMensal)
        {
            Nome = nome;
            CPF = cpf;
            NumeroDepentedentes = numeroDepentedentes;
            RendaBrutaMensal = rendaBrutaMensal;           
        }
    }
}
