using Domain.Enums;
using Domain.Features.Academia.Entities;
using Domain.Features.Planos.Licenca.Entities;
using Domain.Features.Usuarios.Common.Entities;
using Domain.Features.Usuarios.Gestor.Entities;
using Domain.ValueObjects;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Seed
{
    /// <summary>
    /// Seed mínimo de desenvolvimento — substitui o INSERT.sql manual. Cria só o suficiente
    /// para conseguir logar (1 Academia + 1 Usuario Gestor) e criar o resto pela própria API.
    /// Roda condicionalmente, só quando o banco está vazio, nunca em Staging/Production.
    /// </summary>
    public static class DevelopmentSeeder
    {
        public static async Task SeedAsync(VitalitasDbContext context)
        {
            if (await context.Academias.AnyAsync())
                return; // já tem dado — não duplica seed a cada start.

            var licenca = new Licenca(
                idPlano: Guid.Empty,
                mensalidade: new Monetario("199.90"),
                status: StatusLicenca.Ativa,
                tipo: TipoLicenca.Premium,
                dataFim: DateOnly.FromDateTime(DateTime.UtcNow.AddYears(1)),
                dateAssinatura: DateOnly.FromDateTime(DateTime.UtcNow),
                caminhoPdf: string.Empty);

            // Academia.IdGestor é preenchido com um placeholder aqui — não há FK de banco
            // impondo consistência com Gestor.IdGestor (decisão da Fase 2: sem constraints
            // de FK entre entidades, só colunas). Ajustado abaixo depois do Gestor existir.
            var academia = new Academia(
                idlicenca: licenca.IdLicenca,
                idgestor: Guid.NewGuid(),
                nomeacademia: Nome.Create("Academia Vitalitas (seed)"),
                cnpj: new CNPJ("00000000000100"),
                quadra: "Q1", rua: "Rua A", bairro: "Centro", cidade: "Brasília", estado: "DF", cep: "70000000",
                tipoacademia: TipoAcademia.Musculacao,
                emailinstitucional: new Email("contato@vitalitas.seed"));

            var usuarioGestor = new Usuario(
                idUsuario: Guid.NewGuid(),
                idAcademia: academia.IdAcademia,
                nome: "Gestor Seed",
                email: new Email("gestor@vitalitas.seed"),
                senha: "trocar-no-primeiro-login",
                dataNascimento: new DateOnly(1990, 1, 1),
                cpf: new CPF("00000000000"),
                tipoUsuario: TipoUsuario.Gestor,
                ativo: true,
                flag: true,
                quadra: "Q1", rua: "Rua A", bairro: "Centro", cidade: "Brasília", estado: "DF", cep: "70000000");

            var gestor = new Gestor(usuarioGestor.IdUsuario);

            context.Licencas.Add(licenca);
            context.Academias.Add(academia);
            context.Usuarios.Add(usuarioGestor);
            context.Gestores.Add(gestor);

            await context.SaveChangesAsync();

            // Ajusta o placeholder de IdGestor da Academia pro Id real gerado pelo Gestor,
            // e propaga o IdAcademia denormalizado pro Gestor (sem setter público em nenhum dos dois).
            academia.GetType().GetProperty(nameof(Academia.IdGestor))!
                .SetValue(academia, gestor.IdGestor);
            gestor.GetType().GetProperty(nameof(Gestor.IdAcademia))!
                .SetValue(gestor, academia.IdAcademia);
            await context.SaveChangesAsync();
        }
    }
}
