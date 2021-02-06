using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Survey.Models;
using Oqtane.Survey.Server.Repository;

namespace Oqtane.Survey.Repository
{
    public partial class SurveyContext : DBContextBase, IService
    {
        public virtual DbSet<OqtaneSurvey> OqtaneSurvey { get; set; }
        public virtual DbSet<OqtaneSurveyAnswer> OqtaneSurveyAnswer { get; set; }
        public virtual DbSet<OqtaneSurveyItem> OqtaneSurveyItem { get; set; }
        public virtual DbSet<OqtaneSurveyItemOption> OqtaneSurveyItemOption { get; set; }

        public SurveyContext(ITenantResolver tenantResolver, IHttpContextAccessor accessor) : base(tenantResolver, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OqtaneSurvey>(entity =>
            {
                entity.HasKey(e => e.SurveyId);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SurveyName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<OqtaneSurveyAnswer>(entity =>
            {
                entity.Property(e => e.AnswerValue).HasMaxLength(500);

                entity.Property(e => e.AnswerValueDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.SurveyItem)
                    .WithMany(p => p.OqtaneSurveyAnswer)
                    .HasForeignKey(d => d.SurveyItemId)
                    .HasConstraintName("FK_OqtaneSurveyAnswer_SurveyItem");
            });

            modelBuilder.Entity<OqtaneSurveyItem>(entity =>
            {
                entity.Property(e => e.ItemLabel)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ItemType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ItemValue).HasMaxLength(50);

                entity.HasOne(d => d.SurveyNavigation)
                    .WithMany(p => p.OqtaneSurveyItem)
                    .HasForeignKey(d => d.Survey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OqtaneSurveyItem_OqtaneSurvey");
            });

            modelBuilder.Entity<OqtaneSurveyItemOption>(entity =>
            {
                entity.Property(e => e.OptionLabel)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.SurveyItemNavigation)
                    .WithMany(p => p.OqtaneSurveyItemOption)
                    .HasForeignKey(d => d.SurveyItem)
                    .HasConstraintName("FK_OqtaneSurveyItemOption_SurveyItem");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
