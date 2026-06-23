using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using TP2.Domain.Entities;

namespace TP2.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // On définit un DbSet pour chaque entité du Domain
        public DbSet<User> Users { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<TaxDeclaration> TaxDeclarations { get; set; }
        public DbSet<SupportingDocument> SupportingDocuments { get; set; }
        public DbSet<NoticeOfAssessment> NoticesOfAssessment { get; set; }
        public DbSet<CanadaIntegrationLog> CanadaIntegrationLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ici, on peut configurer les contraintes (clés primaires, index, etc.)
            modelBuilder.Entity<User>().HasIndex(u => u.Nas).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
