using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedicalStaff.WebService.Core.Models.Db.Physician;
using MedicalStaff.WebService.Core.Models.Db.Patient;
using MedicalStaff.WebService.Core.Models.Db.Records;

namespace MedicalStaff.WebService.Core.Data
{
    /// <summary>
    /// Provides a mechanism for accessing and performing databe operations.
    /// </summary>
    public class SystemDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SystemDbContext"/> with the <see cref="DbContextOptions"/> specifications.
        /// </summary>
        /// <param name="options"></param>
        public SystemDbContext([NotNull] DbContextOptions options) : base(options) { }


        #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            modelBuilder.ApplyConfiguration<PhysicianAccount>(new PhysicianAccountEntity());
            modelBuilder.ApplyConfiguration<PatientAccount>(new PatientAccountEntity());
            modelBuilder.ApplyConfiguration<PatientRecords>(new PatientRecordsAccountEntity());

            base.OnModelCreating(modelBuilder);
        }

        readonly private struct PhysicianAccountEntity : IEntityTypeConfiguration<PhysicianAccount>
        {
            public PhysicianAccountEntity() { }

            readonly void IEntityTypeConfiguration<PhysicianAccount>.Configure(EntityTypeBuilder<PhysicianAccount> entityTypeBuilder)
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

        readonly private struct PatientRecordsAccountEntity : IEntityTypeConfiguration<PatientRecords>
        {
            public PatientRecordsAccountEntity() { }

            readonly void IEntityTypeConfiguration<PatientRecords>.Configure(EntityTypeBuilder<PatientRecords> entityTypeBuilder)
            {
                entityTypeBuilder.HasKey(column => column.ID);
                entityTypeBuilder.Property(column => column.CPF).IsRequired().HasMaxLength(11).HasColumnType("varchar(11)").HasColumnName("cpf");
                entityTypeBuilder.Property(column => column.Name).IsRequired().HasMaxLength(100).HasColumnType("varchar(100)").HasColumnName("name");
                entityTypeBuilder.Property(column => column.Birth).IsRequired().HasColumnType("datetime").HasColumnName("birth_date");
                entityTypeBuilder.Property(column => column.Email).IsRequired().HasColumnType("varchar(100)").HasColumnName("mail_address");
                entityTypeBuilder.Property(column => column.Phone).IsRequired().HasMaxLength(14).HasColumnType("char(14)").HasColumnName("phone_number");
                entityTypeBuilder.Property(column => column.Address).IsRequired().HasColumnType("varchar(100)").HasColumnName("address");
                entityTypeBuilder.Property(column => column.PictureLocation).IsRequired().HasColumnType("varchar(200)").HasColumnName("picture_location");
                entityTypeBuilder.Property(column => column.Created).IsRequired().HasColumnType("datetime").HasColumnName("record_creation_date");
            }
        }

        /// <summary>
        /// The <see cref="PhysicianAccount"/> table.
        /// </summary>
        public DbSet<PhysicianAccount> PhysicianAccounts { get; set; }

        /// <summary>
        /// The <see cref="PatientAccount"/> table.
        /// </summary>
        public DbSet<PatientAccount> PatientsAccounts { get; set; }

        /// <summary>
        /// The <see cref="PatientRecords"/> table.
        /// </summary>
        public DbSet<PatientRecords> PatientRecords { get; set; }
    }
}