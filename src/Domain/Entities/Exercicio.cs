using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Exercicio
    {
        public string Nome { get; private set;  }
        public int Series { get; private set; }
        public int Repeticoes { get; private set; }

        [JsonExtensionData]
        public Dictionary<string, object> Extras { get; private set; } = new Dictionary<string, object>();
        public Exercicio(string nome, int series, int repeticoes)
        {
            Nome = nome;
            Series = series;
            Repeticoes = repeticoes;
        }

        public void AdicionarExtra(string chave, object valor)
        {
            Extras[chave] = valor;
        }
    }
}
