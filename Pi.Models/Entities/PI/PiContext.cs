using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pi.Models.Entities.PI
{
    public partial class PiContext : DbContext
    {
        public PiContext()
        {
        }

        public PiContext(DbContextOptions<PiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PiUser> PiUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=nattapon-th.cykchd0rv0ms.us-east-1.rds.amazonaws.com,1433;Database=PiSecuritiesAssignment;User Id=adminAWS01;Password=e&8!tm!ukFVVfIOo;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;Integrated Security=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PiUser>(entity =>
            {
                entity.ToTable("pi_users");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GivenName).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
