using Domain.Enums;
using Domain.ValueObjects;
using Domain.Features.Fichas.Ficha.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Shared.Entities
{
    public class Treino
    {
        public Guid IdTreino { get; private set; }
        public Guid IdFicha { get; private set; }
        public Dictionary<string, List<Exercicio>> Exercicio { get; private set; } = new();
        public TipoTreino Tipo { get; private set; }
        public string NomeTreino { get; private set; }

        //converter para o banco
        //string jsonParaBanco = JsonSerializer.Serialize(meuTreino.Exercicios);
        public Treino() { }

        public Treino(Guid idficha, string nometreino, TipoTreino tipo)
        {
            IdTreino = Guid.NewGuid();
            IdFicha = idficha;
            NomeTreino = nometreino;
            Tipo = tipo;
        }
    }
}
