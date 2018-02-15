namespace ShiftInc.Raizen.ShellTanqueCheio.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using ShiftInc.Raizen.ShellTanqueCheio.Entity;

    public partial class ShellTanqueCheioModel : DbContext
    {
        public ShellTanqueCheioModel()
            : base("name=ShellTanqueCheioDataModel")
        {
            Database.SetInitializer<ShellTanqueCheioModel>(null);
        }
        
        public virtual DbSet<BlockedCPF> BlockedCPFs { get; set; }       
        public virtual DbSet<LuckyCode> LuckyCodes { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }       
        public virtual DbSet<AdminAccount> AdminAccounts { get; set; }       
        public virtual DbSet<NewsSending> NewsSending { get; set; }
        public virtual DbSet<ViewReceiptExport> ViewReceiptExport { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {           

            modelBuilder.Entity<LuckyCode>()
                .Property(e => e.code);               

            modelBuilder.Entity<LuckyCode>()
               .Property(e => e.dtWinner)
               .IsOptional();

            modelBuilder.Entity<Person>()
                .Property(e => e.cpf)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.email)
                .IsUnicode(false);           

            modelBuilder.Entity<Product>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Receipts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NewsSending>()
                .Property(e => e.dtSending)
                .IsOptional();

            modelBuilder.Entity<NewsSending>()
                .Property(e => e.idReceipt)
                .IsOptional();
            modelBuilder.Entity<NewsSending>()
                .Property(e => e.idPerson)
                .IsOptional();

            modelBuilder.Entity<Receipt>()
                .Property(e => e.isValidated)
                .IsOptional();           
        }
    }
}
