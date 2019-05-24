using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoHBSIS.Services;
using ProjetoHBSIS.Models;

namespace ProjetoHBSISTests
{
    [TestClass]
    public class ContribuinteTests
    {

        [TestMethod]
        public void CalculoCorretoDescontoPorDependentes()
        {
            //arranjo
            double rendaBruta = 2500;
            double salarioMinimo = 998;
            int numeroDependente = 2;
            double totalDescontoPorDependentes = 99.8;
            //descontoPorDependentes - Considerando um percentual de 5% de denconto por dependentes.

            Contribuinte contribuinte = new Contribuinte { Nome = "Bob Marley", CPF = "85695478523", NumeroDepentedentes = numeroDependente, RendaBrutaMensal = rendaBruta };

            // calcula
            ContribuinteService contribuinteService = new ContribuinteService();
            double totalDescontoPorDependentesEsperado = contribuinteService.CalculaDescontoPorDependentes(contribuinte, salarioMinimo);

            // assert
            Assert.AreEqual(totalDescontoPorDependentesEsperado, totalDescontoPorDependentes, 000.1, "Desconto correto!");
        }

        [TestMethod]
        public void CalculoIncorretoDescontoPorDependentes()
        {
            //arranjo            
            double rendaBruta = 5590;
            double salarioMinimo = 900;
            int numeroDependente = 1;
            double totalDescontoPorDependentes = 5500;
            //descontoPorDependentes - Considerando um percentual de 5% de denconto por dependentes.

            Contribuinte contribuinte = new Contribuinte { Nome = "Bette Almeida", CPF = "65248565547", NumeroDepentedentes = numeroDependente, RendaBrutaMensal = rendaBruta };

            // calcula
            ContribuinteService contribuinteService = new ContribuinteService();
            double totalDescontoPorDependentesEsperado = contribuinteService.CalculaDescontoPorDependentes(contribuinte, salarioMinimo);

            // assert
            Assert.AreEqual(totalDescontoPorDependentesEsperado, totalDescontoPorDependentes, 000.1,"Desconto incorreto!");                       
        }

        [TestMethod]
        public void CalculoCorretoRendaLiquida()
        {
            //arranjo
            double rendaBruta = 4528.5;
            double salarioMinimo = 1000;            
            double totalRendaLiquida = 4328.5;
            
            Contribuinte contribuinte = new Contribuinte { Nome = "Alex Souza", CPF = "12548563547", NumeroDepentedentes = 3, RendaBrutaMensal = rendaBruta };

            // calcula
            ContribuinteService contribuinteService = new ContribuinteService();
            double totalRendaLiquidaEsperado = contribuinteService.CalculaRendaLiquida(contribuinte, salarioMinimo);
            
            // assert
            Assert.AreEqual(totalRendaLiquidaEsperado, totalRendaLiquida, 000.1, "Calculado de Renda Liquida correto!");
        }

        [TestMethod]
        public void CalculoIncorretoRendaLiquida()
        {
            //arranjo
            double rendaBruta = 8500;
            double salarioMinimo = 950.18;
            double totalRendaLiquida = 8102.82;

            Contribuinte contribuinte = new Contribuinte { Nome = "Alfredo Menezes", CPF = "56968547785", NumeroDepentedentes = 5, RendaBrutaMensal = rendaBruta };

            // calcula
            ContribuinteService contribuinteService = new ContribuinteService();
            double totalRendaLiquidaEsperado = contribuinteService.CalculaRendaLiquida(contribuinte, salarioMinimo);

            // assert
            Assert.AreEqual(totalRendaLiquidaEsperado, totalRendaLiquida, 000.1, "Calculado de Renda Liquida correto!");
        }
        
    }
}
