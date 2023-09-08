using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedicalRecordsSystem.WebService.Core.Models.Db.MedicalPractioner;
using MedicalRecordsSystem.WebService.Core.Models.Db.Patient;
using MedicalRecordsSystem.WebService.Core.Models.Db.MedicalRecord;

namespace MedicalRecordsSystem.WebService.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public SystemDbContext([NotNull] DbContextOptions options) : base(options) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<MedicalPractionerAccount>(new MedicalPractionerAccountEntity());
            modelBuilder.ApplyConfiguration<PatientAccount>(new PatientAccountEntity());
            modelBuilder.ApplyConfiguration<MedicalRecord>(new MedicalRecordsAccountEntity());

            base.OnModelCreating(modelBuilder);
        }

        readonly private struct MedicalPractionerAccountEntity : IEntityTypeConfiguration<MedicalPractionerAccount>
        {
            public MedicalPractionerAccountEntity() { }

            readonly void IEntityTypeConfiguration<MedicalPractionerAccount>.Configure(EntityTypeBuilder<MedicalPractionerAccount> entityTypeBuilder)
            {
                entityTypeBuilder.HasKey(column => column.ID);
                entityTypeBuilder.Property(column => column.CRM).IsRequired().HasMaxLength(13).HasColumnType("varchar(13)").HasColumnName("crm");
                entityTypeBuilder.Property(column => column.CPF).IsRequired().HasMaxLength(11).HasColumnType("varchar(11)").HasColumnName("cpf");
                entityTypeBuilder.Property(column => column.Name).IsRequired().HasColumnType("varchar(100)").HasColumnName("name");
                entityTypeBuilder.Property(column => column.Password).IsRequired().HasColumnType("varchar(100)").HasColumnName("password");
                entityTypeBuilder.Property(column => column.Email).IsRequired().HasColumnType("varchar(100)").HasColumnName("email");
                entityTypeBuilder.Property(column => column.Role).IsRequired().HasColumnType("char(18)").HasColumnName("role");
            }
        }

        readonly private struct PatientAccountEntity : IEntityTypeConfiguration<PatientAccount>
        {
            public  PatientAccountEntity() { }

            readonly void IEntityTypeConfiguration<PatientAccount>.Configure(EntityTypeBuilder<PatientAccount> entityTypeBuilder)
            {
                entityTypeBuilder.HasKey(column => column.ID);
                entityTypeBuilder.Property(column => column.CPF).IsRequired().HasMaxLength(11).HasColumnType("varchar(11)").HasColumnName("cpf");
                entityTypeBuilder.Property(column => column.Name).IsRequired().HasColumnType("varchar(100)").HasColumnName("name");
                entityTypeBuilder.Property(column => column.Password).IsRequired().HasColumnType("varchar(100)").HasColumnName("password");
                entityTypeBuilder.Property(column => column.Email).IsRequired().HasColumnType("varchar(100)").HasColumnName("email");
                entityTypeBuilder.Property(column => column.Role).IsRequired().HasColumnType("char(18)").HasColumnName("role");
            }
        }

        readonly private struct MedicalRecordsAccountEntity : IEntityTypeConfiguration<MedicalRecord>
        {
            public MedicalRecordsAccountEntity() { }

            readonly void IEntityTypeConfiguration<MedicalRecord>.Configure(EntityTypeBuilder<MedicalRecord> entityTypeBuilder)
            {
                entityTypeBuilder.HasKey(column => column.ID);
                entityTypeBuilder.Property(column => column.CPF).IsRequired().HasMaxLength(11).HasColumnType("varchar(11)").HasColumnName("cpf");
                entityTypeBuilder.Property(column => column.Name).IsRequired().HasMaxLength(100).HasColumnType("varchar(100)").HasColumnName("name");
                entityTypeBuilder.Property(column => column.PhoneNumber).IsRequired().HasMaxLength(14).HasColumnType("char(14)").HasColumnName("phone_number");
                entityTypeBuilder.Property(column => column.Address).IsRequired().HasColumnType("varchar(100)").HasColumnName("address");
                entityTypeBuilder.Property(column => column.PictureLocation).IsRequired().HasColumnType("varchar(200)").HasColumnName("picture_location");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<MedicalPractionerAccount> MedicalPractionerAccounts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<PatientAccount> PatientsAccounts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
    }
}