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

        public virtual DbSet<PlayerDatum> PlayerData { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<SeasonName> SeasonNames { get; set; }
        public virtual DbSet<SeasonSplit> SeasonSplits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:dbml");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Japanese_CI_AS");

            modelBuilder.Entity<PlayerDatum>(entity =>
            {
                entity.ToTable("player_data");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AverageDamage).HasColumnName("average_damage");

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
                    .WithMany(p => p.PlayerData)
                    .HasForeignKey(d => d.RankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__INIT_DATA__rank___70DDC3D8");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.PlayerData)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__INIT_DATA__seaso__6FE99F9F");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("ranks");

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
                    .HasName("PK__season_n__0A99E331893E51B0");

                entity.ToTable("season_names");

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
                    .HasName("PK__season_s__3B93D96EB5EA8738");

                entity.ToTable("season_splits");

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
