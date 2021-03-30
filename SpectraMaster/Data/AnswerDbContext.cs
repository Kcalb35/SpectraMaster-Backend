using Microsoft.EntityFrameworkCore;

namespace SpectraMaster.Data
{
    public class AnswerDbContext : DbContext
    {
        public DbSet<SpectraAnswer> SpectraAnswers { get; set; }
        public DbSet<NMRProblem> NmrProblems { get; set; }
        public DbSet<MassProblem> MassProblems { get; set; }
        public DbSet<AnswerPicture> AnswerPics { get; set; }
        public DbSet<ProblemPicture> ProblemPics { get; set; }

        public AnswerDbContext(DbContextOptions<AnswerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnswerPicture>()
                .HasOne(pic => pic.SpectraAnswer)
                .WithMany(a => a.AnswerPictures)
                .HasForeignKey(pic => pic.AnswerId);
            modelBuilder.Entity<ProblemPicture>()
                .HasOne(pic => pic.SpectraAnswer)
                .WithMany(a => a.ProblemPictures)
                .HasForeignKey(pic => pic.AnswerId);
            modelBuilder.Entity<MassProblemAnswer>().HasKey(x => new {x.AnswerId, x.MassProblemId});
            modelBuilder.Entity<NMRProblemAnswer>().HasKey(x => new {x.AnswerId, x.NMRProblemId});
        }
    }
}