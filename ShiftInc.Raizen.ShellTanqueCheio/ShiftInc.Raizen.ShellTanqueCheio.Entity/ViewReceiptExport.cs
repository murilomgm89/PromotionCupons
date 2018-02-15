namespace ShiftInc.Raizen.ShellTanqueCheio.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewReceiptExport")]
    public partial class ViewReceiptExport
    {
        [Key]
        public int idReceipt { get; set; }
         public string CNPJ_do_estabelecimento { get; set; }
         public string Produto { get; set; }
         public string Tipo_do_Produto { get; set; }
         public bool Premiado { get; set; }
         public bool? Validado { get; set; }
         public string Motivo_da_Validacao { get; set; }
         public DateTime Data_do_Cadastro_do_Recibo { get; set; }
         public string Nome_do_Participante { get; set; }
         public string cpf { get; set; }
         public string nascimento { get; set; }
         public string telefone { get; set; }
         public string email { get; set; }
         public string uf { get; set; }
         public string PersonCidade { get; set; }
         public string endereco { get; set; }
         public string PersonNumero { get; set; }
         public string PensonComplemento { get; set; }
         public string Bairro { get; set; }
         public string CEP { get; set; }
         public DateTime Data_de_Cadastro_do_Participante { get; set; }        
    }
}
