using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGeneratorv1
{
    public class TestGeneratorContext: IdentityDbContext<User>
    {
        public TestGeneratorContext(DbContextOptions<TestGeneratorContext> options) : base(options) {
        }
        public TestGeneratorContext()
        {

        }

        // Dodavanjem klasa iz modela kao DbSet iste će biti mapirane u bazu podataka
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<DifficultyLevel> DifficultyLevel { get; set; }
        public DbSet<NumberOfPoints> NumberOfPoints { get; set; }
        public DbSet<Question> Predmet { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<TestQuestion> TestQuestion { get; set; }
        public DbSet<UserSubjects> UserSubjects { get; set; }
        public DbSet<Visibility> Visibility { get; set; }
        public DbSet<YearOfStudy> YearOfStudy { get; set; }
        public DbSet<TestArea> TestArea { get; set; }
      //  public DbSet<User> User { get; set; }
      

    }
}
