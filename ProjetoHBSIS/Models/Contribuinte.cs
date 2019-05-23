using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHBSIS.Models
{
    public class Contribuinte
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} o tamanho deve ser entre {2} and {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        [EmailAddress(ErrorMessage = "Entre com CPF válido!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]        
        [Display(Name = "Número de Dependentes")]
        public int NumeroDepentedentes { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        [Display(Name = "Renda Bruta Mensal")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double RendaBrutaMensal { get; set; }

        public ImpostodeRenda ImpostodeRenda { get; set; }

        public int PercDesconto;        
        
        public Contribuinte()
        {
        }

        public Contribuinte(string nome, string cPF, int numeroDepentedentes, double rendaBrutaMensal, ImpostodeRenda impostodeRenda)
        {
            Nome = nome;
            CPF = cPF;
            NumeroDepentedentes = numeroDepentedentes;
            RendaBrutaMensal = rendaBrutaMensal;            
            PercDesconto = 5;
            ImpostodeRenda = impostodeRenda;
        }

        public double DescPorDependentes()
        {
            var perctotal = NumeroDepentedentes * PercDesconto;

            return (ImpostodeRenda.SalarioMinimo.Valor * perctotal) / 100;
        }

        public double RendaLiquida()
        {
            return RendaBrutaMensal - ImpostodeRenda.SalarioMinimo.Valor;
        }


    }
}
