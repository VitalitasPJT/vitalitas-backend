using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Vitalitas.Models;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<UsuarioC> Usuarios { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<FichaDeTreino> FichasDeTreinos { get; set; }
    public DbSet<Treino> Treinos { get; set; }
    public DbSet<TreinoExercicio> TreinoExercicios { get; set; }
    public DbSet<Agenda> Agendas { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Perimetro> Perimetros { get; set; }
    public DbSet<Cutaneas> Cutaneass { get; set; }
    public DbSet<Resultado> Resultado { get; set; }

}
