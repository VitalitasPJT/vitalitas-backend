using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Vitalitas.Backend.Domain.Entities;

namespace Vitalitas.Backend.Infrastructure.Persistence.Contexts
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<TreinoExercicio> TreinoExercicios { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
    }
}