using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GSMA.DataProvider.Data
{
    public partial class GSMAContext : DbContext
    {
        public GSMAContext()
        {
        }

        public GSMAContext(DbContextOptions<GSMAContext> options)
            : base(options)
        {
        }

        public GSMAContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        public virtual DbSet<Egmdetails> Egmdetails { get; set; }
        public virtual DbSet<Egmseals> Egmseals { get; set; }
        public virtual DbSet<SealDetails> SealDetails { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=BANKUMARKD-M\\SQLEXPRESS;Initial Catalog=GMSA;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Egmdetails>(entity =>
            {
                entity.ToTable("EGMDetails");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Egmid).HasColumnName("EGMID");

                entity.Property(e => e.Egmname)
                    .IsRequired()
                    .HasColumnName("EGMName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EgmserialNumber)
                    .IsRequired()
                    .HasColumnName("EGMSerialNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Egmseals>(entity =>
            {
                entity.ToTable("EGMSeals");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Captureddatetime).HasColumnType("datetime");

                entity.Property(e => e.JobCompleateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.AssaignedUser)
                    .WithMany(p => p.EgmsealsAssaignedUser)
                    .HasForeignKey(d => d.AssaignedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EGMSeals_User1");

                entity.HasOne(d => d.CapturedUser)
                    .WithMany(p => p.EgmsealsCapturedUser)
                    .HasForeignKey(d => d.CapturedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EGMSeals_User");

                entity.HasOne(d => d.Egm)
                    .WithMany(p => p.Egmseals)
                    .HasForeignKey(d => d.Egmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EGMSeals_EGMDetails");

                entity.HasOne(d => d.Seal)
                    .WithMany(p => p.InverseSeal)
                    .HasForeignKey(d => d.SealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EGMSeals_EGMSeals1");
            });

            modelBuilder.Entity<SealDetails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CapturedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserType");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
