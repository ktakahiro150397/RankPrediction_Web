using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RankPrediction_Web.Models
{
    public partial class RankPredictionContext : DbContext
    {
        public RankPredictionContext()
        {
        }

        public RankPredictionContext(DbContextOptions<RankPredictionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PredictionDatum> PredictionData { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<SeasonName> SeasonNames { get; set; }
        public virtual DbSet<SeasonSplit> SeasonSplits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Japanese_CI_AS");

            modelBuilder.Entity<PredictionDatum>(entity =>
            {
                entity.ToTable("prediction_data", "ml_predict");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AverageDamage)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("average_damage");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsParty).HasColumnName("is_party");

                entity.Property(e => e.KillDeathRatio)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("kill_death_ratio");

                entity.Property(e => e.MatchCounts).HasColumnName("match_counts");

                entity.Property(e => e.RankId).HasColumnName("rank_id");

                entity.Property(e => e.SeasonId).HasColumnName("season_id");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.PredictionData)
                    .HasForeignKey(d => d.RankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__predictio__rank___2B3F6F97");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.PredictionData)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__predictio__seaso__2C3393D0");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("ranks", "ml_predict");

                entity.Property(e => e.RankId)
                    .ValueGeneratedNever()
                    .HasColumnName("rank_id");

                entity.Property(e => e.RankName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("rank_name");
            });

            modelBuilder.Entity<SeasonName>(entity =>
            {
                entity.HasKey(e => e.SeasonId)
                    .HasName("PK__season_n__0A99E331CC2A21E0");

                entity.ToTable("season_names", "ml_predict");

                entity.Property(e => e.SeasonId)
                    .ValueGeneratedNever()
                    .HasColumnName("season_id");

                entity.Property(e => e.SeasonName1)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("season_name");
            });

            modelBuilder.Entity<SeasonSplit>(entity =>
            {
                entity.HasKey(e => e.SplitNoId)
                    .HasName("PK__season_s__3B93D96ED14E868B");

                entity.ToTable("season_splits", "ml_predict");

                entity.Property(e => e.SplitNoId)
                    .ValueGeneratedNever()
                    .HasColumnName("split_no_id");

                entity.Property(e => e.SplitName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("split_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
