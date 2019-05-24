using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProjetoHBSIS.Services;
using ProjetoHBSIS.Models;


namespace ProjetoHBSISTests
{
    [TestClass]
    class ImpostodeRendaTests
    {

        [TestMethod]
        public void CalculoCorretoAliquota()
        {
            //arranjo
            double rendaBruta = 2158.35;
            double salarioMinimo = 998;
            int numeroDependente = 2;
            double totalImpostodeRenda = 154.39;
            //descontoPorDependentes - Considerando um percentual de 5% de denconto por dependentes.

            Contribuinte contribuinte = new Contribuinte { Nome = "Sidney Sampaio", CPF = "58696547854", NumeroDepentedentes = numeroDependente, RendaBrutaMensal = rendaBruta };

            // calcula
            ImpostodeRendaService contribuinteService = new ImpostodeRendaService();
            double totalImpostodeRendaEsperado = contribuinteService.CalcularAliquota(contribuinte, salarioMinimo);
            
            // assert
            Assert.AreEqual(Math.Round(totalImpostodeRendaEsperado), Math.Round(totalImpostodeRenda), 000.1, "Imposto de Renda correto!");
        }

        [TestMethod]
        public void CalculoIncorretoAliquota()
        {
            //arranjo
            double rendaBruta = 1980.5;
            double salarioMinimo = 1105.2;
            int numeroDependente = 1;
            double totalImpostodeRenda = 433.17;            

            Contribuinte contribuinte = new Contribuinte { Nome = "Mirella Rocha", CPF = "63254785245", NumeroDepentedentes = numeroDependente, RendaBrutaMensal = rendaBruta };

            // calcula
            ImpostodeRendaService contribuinteService = new ImpostodeRendaService();
            double totalImpostodeRendaEsperado = contribuinteService.CalcularAliquota(contribuinte, salarioMinimo);

            // assert
            Assert.AreEqual(Math.Round(totalImpostodeRendaEsperado), Math.Round(totalImpostodeRenda), 000.1, "Imposto de Renda correto!");
        }

    }
}
