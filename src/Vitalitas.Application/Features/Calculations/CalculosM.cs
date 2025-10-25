using System.ComponentModel.DataAnnotations;

namespace Vitalitas.Calculations
{
    public class CalculosMasculino
    {

        public double Imc;
        public double Soma_Das_Dobras;
        public double Densidade_Corporal;
        public double Percentual_De_Gordura;
        public double Massa_Gorda;
        public double Percentual_De_Massa_Magra;
        public double Massa_Magra;

        public CalculosMasculino(double altura, double peso,

                        double tr, double cx, double si, double ab, double ax,
                        double pt, double se, 
                        
                        int idade) 
        {
            Imc = IMC(altura , peso);
            double sete = Somadobras(tr, se, si, ab, cx, pt, ax);
            Soma_Das_Dobras = sete;
            double densidade = DensidadeCorporal(sete, idade);
            Densidade_Corporal = densidade;
            double pgordura = PercentualGordura(densidade);
            Percentual_De_Gordura = pgordura;
            Massa_Gorda = MassaGorda(peso, pgordura);
            double pmagro = PercentualMaassaMagra(pgordura);
            Percentual_De_Massa_Magra = pmagro;
            Massa_Magra = MassaMagra(peso, pmagro);
        }

        public double IMC(double altura, double peso)
        {
            return (peso / (altura * altura));
        }

        public double Somadobras(double tr, double se, double si, double ab,
                                double cx, double pt, double ax)
        {
            return (tr + se + si + ab + cx + pt + ax);
        }

        public double DensidadeCorporal(double setedobras, double idade)
        {
            return (1.112 - ((0.00043499 * setedobras) + (0.00000055 * (setedobras * setedobras)) - (0.00028826 * idade)));
        }

        public double PercentualGordura(double densidade)
        {
            return ((495 / densidade) - 450);
        }

        public double MassaGorda(double peso, double pgordo)
        {
            return (peso * (pgordo / 100));
        }

        public double PercentualMaassaMagra(double pgordo)
        {
            return (100 - pgordo);
        }

        public double MassaMagra(double peso, double pmagro)
        {
            return (peso * (pmagro / 100));
        }
    }
}
