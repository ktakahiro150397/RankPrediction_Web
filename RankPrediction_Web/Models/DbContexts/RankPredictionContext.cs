using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RankPrediction_Web.Models.DbContexts
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
        public virtual DbSet<PyRankPrediction> PyRankPredictions { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<RankPyModel> RankPyModels { get; set; }
        public virtual DbSet<Saying> Sayings { get; set; }
        public virtual DbSet<SeasonName> SeasonNames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:dbml");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<PredictionDatum>(entity =>
            {
                entity.ToTable("prediction_data", "ml_predict");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AverageDamage).HasColumnName("average_damage");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsParty).HasColumnName("is_party");

                entity.Property(e => e.KillDeathRatio).HasColumnName("kill_death_ratio");

                entity.Property(e => e.MatchCounts).HasColumnName("match_counts");

                entity.Property(e => e.RankId).HasColumnName("rank_id");

                entity.Property(e => e.SeasonId).HasColumnName("season_id");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.PredictionData)
                    .HasForeignKey(d => d.RankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__predictio__rank___29572725");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.PredictionData)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__predictio__seaso__2A4B4B5E");
            });

            modelBuilder.Entity<PyRankPrediction>(entity =>
            {
                entity.ToTable("py_rank_predictions", "ml_predict");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PredictResultRankId).HasColumnName("predict_result_rank_id");

                entity.Property(e => e.SourceDataId).HasColumnName("source_data_id");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("ranks", "ml_predict");

                entity.Property(e => e.RankId)
                    .ValueGeneratedNever()
                    .HasColumnName("rank_id");

                entity.Property(e => e.DisplaySeq).HasColumnName("display_seq");

                entity.Property(e => e.RankDesc)
                    .IsRequired()
                    .HasColumnName("rank_desc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RankDescJa)
                    .IsRequired()
                    .HasColumnName("rank_desc_ja")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RankName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("rank_name");

                entity.Property(e => e.RankNameJa)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("rank_name_ja")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RankPic).HasColumnName("rank_pic");
            });

            modelBuilder.Entity<RankPyModel>(entity =>
            {
                entity.HasKey(e => new { e.SeasonId, e.IsMatchcountsContain })
                    .HasName("PK__rank_py___0E8878E67BCEBA20");

                entity.ToTable("rank_py_models", "ml_predict");

                entity.Property(e => e.SeasonId).HasColumnName("season_id");

                entity.Property(e => e.IsMatchcountsContain).HasColumnName("is_matchcounts_contain");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ModelData).HasColumnName("model_data");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.RankPyModels)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SEASON_ID");
            });

            modelBuilder.Entity<Saying>(entity =>
            {
                entity.ToTable("sayings", "ml_predict");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Saying1)
                    .IsRequired()
                    .HasColumnName("saying")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SayingBy)
                    .IsRequired()
                    .HasColumnName("saying_by")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SayingByJa)
                    .IsRequired()
                    .HasColumnName("saying_by_ja")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SayingJa)
                    .IsRequired()
                    .HasColumnName("saying_ja")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SeasonName>(entity =>
            {
                entity.HasKey(e => e.SeasonId)
                    .HasName("PK__season_n__0A99E33119C9B1DC");

                entity.ToTable("season_names", "ml_predict");

                entity.Property(e => e.SeasonId)
                    .ValueGeneratedNever()
                    .HasColumnName("season_id");

                entity.Property(e => e.DisplaySeq).HasColumnName("display_seq");

                entity.Property(e => e.SeasonName1)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("season_name");

                entity.Property(e => e.SeasonNameJa)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("season_name_ja")
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
