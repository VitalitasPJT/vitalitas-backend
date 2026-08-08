using Domain.Enums;
using Domain.ValueObjects;
using Domain.Features.Records.TrainingSheet.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Shared.Entities
{
    public class Workout
    {
        public Guid IdTreino { get; private set; }
        public Guid IdFicha { get; private set; }
        public Dictionary<string, List<Exercise>> Exercicio { get; private set; } = new();
        public WorkoutType Tipo { get; private set; }
        public string NomeTreino { get; private set; }

        //converter para o banco
        //string jsonParaBanco = JsonSerializer.Serialize(meuTreino.Exercicios);
        public Workout() { }

        public Workout(Guid idficha, string nometreino, WorkoutType tipo)
        {
            IdTreino = Guid.NewGuid();
            IdFicha = idficha;
            NomeTreino = nometreino;
            Tipo = tipo;
        }
    }
}
