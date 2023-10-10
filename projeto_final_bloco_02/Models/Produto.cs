using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_final_bloco_02.Models
{
    public class Produto
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Nome { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(700)]
        public string Descricao { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Fabricante { get; set; } = string.Empty;

        [Column(TypeName =  "decimal(10,2)")]
        public decimal Valor { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(5000)]
        public string Foto { get; set; } = string.Empty;

    }
}