using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vitalitas.Models
{
    [Table("contrato")]
    public class Contrato
    {
        [Key]
        [Required]
        [Column("[id_assinatura]")]
        public int IdAssinatura { get; set; }

        [Required]
        [Column("id_aluno")]
        public int IdAluno { get; set; }

        [Required]
        [Column("[tipo]")]
        public string Tipo { get; set; }

        [Required]
        [Column("[data_assinatura]")]
        public DateOnly DataAssinatura { get; set; }

        [Required]
        [Column("[status]")]
        public string Status { get; set; } 
    }

    [Table("mensalidade")]
    public class Mensalidade
    {
        [Key]
        [Required]
        [Column("[id_mensalidade]")]
        public int IdMensalidade { get; set; }

        [Required]
        [Column("id_assinatura")]
        public int IdAssinatura { get; set; }

        [Required]
        [Column("[valor]")]
        public decimal Valor { get; set; }

        [Required]
        [Column("[status]")]
        public string Status { get; set; } 

        [Required]
        [Column("[data_vencimento]")]
        public DateOnly DataVencimento { get; set; }

        [Required]
        [Column("[data_pagamento]")]
        public DateOnly DataPagamento { get; set; }

        [Required]
        [Column("[referencia]")]
        public string Referencia { get; set; } 
    }
}