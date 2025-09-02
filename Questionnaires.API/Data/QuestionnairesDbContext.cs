using Microsoft.EntityFrameworkCore;
using Questionnaires.API.Data.Models;

namespace Questionnaires.API.Data
{
    public class QuestionnairesDbContext : DbContext
    {
        public QuestionnairesDbContext(DbContextOptions<QuestionnairesDbContext> options) : base(options)
        {
        }

        // Define DbSets for your entities here
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Response> Responses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Questionnaire>()
                .HasOne(q => q.User)
                .WithMany(u => u.Questionnaires)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Response>()
                .HasOne(r => r.User)
                .WithMany(u => u.Responses)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Response>()
                .HasOne(r => r.Questionnaire)
                .WithMany(q => q.Responses)
                .HasForeignKey(r => r.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Questionnaire)
                .WithMany(qr => qr.Questions)
                .HasForeignKey(q => q.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Response)
                .WithMany(r => r.Answers)
                .HasForeignKey(a => a.ResponseId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
