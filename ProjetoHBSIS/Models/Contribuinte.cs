using ProjetoHBSIS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoHBSIS.Models;
using SalesWebMvc.Services.Exceptions;

namespace ProjetoHBSIS.Models
{
    public class Contribuinte
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} - Campo obrigatório!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} o tamanho deve ser entre {2} and {1}!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} - Campo obrigatório!")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter {2} dígitos!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "{0} - Campo obrigatório!")]
        [Display(Name = "Número de Dependentes")]
        public int NumeroDepentedentes { get; set; }

        [Required(ErrorMessage = "{0} - Campo obrigatório!")]
        [Display(Name = "Renda Bruta Mensal")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double RendaBrutaMensal { get; set; }

        private readonly ContribuinteService _contribuinteService;

        public Contribuinte()
        {
        }

        public Contribuinte(string nome, string cPF, int numeroDepentedentes, double rendaBrutaMensal, ContribuinteService contribuinteService)
        {
            Nome = nome;
            CPF = cPF;
            NumeroDepentedentes = numeroDepentedentes;
            RendaBrutaMensal = rendaBrutaMensal;
            _contribuinteService = contribuinteService;
        }

        public double CalculaDescontoPorDependentes(Contribuinte contribuinte)
        {
            return _contribuinteService.CalculaDescontoPorDependentes(contribuinte);
        }

        public double CalculaRendaLiquida(Contribuinte contribuinte)
        {
            return _contribuinteService.CalculaRendaLiquida(contribuinte);
        }
    }
}
