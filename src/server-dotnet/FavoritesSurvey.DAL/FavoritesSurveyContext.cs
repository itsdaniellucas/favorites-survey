using FavoritesSurvey.Core.Architecture.DAL;
using FavoritesSurvey.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoritesSurvey.DAL
{
    public class FavoritesSurveyContext : DbContext, IContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ComputedResponse> ComputedResponses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Survey> Surveys { get; set; }

        public FavoritesSurveyContext()
                : base(GetContextOptions())
        {
            Database.EnsureCreated();
            Database.SetCommandTimeout(300);
        }

        private static DbContextOptions GetContextOptions()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var env = config["Environment"];
            var connectionString = config[$"ConnectionStrings:{env}"];
            var options = new DbContextOptionsBuilder();
            options.UseSqlServer(connectionString);
            return options.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateNow = DateTime.Now;

            // similar to modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>() in EF6
            foreach (var rel in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                rel.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<Question>().HasData(new Question[]
            {
                new Question() { Id = 1, Name = "What's your favorite planet in the solar system?", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Question() { Id = 2, Name = "What's your favorite type of pet?", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Question() { Id = 3, Name = "What's your favorite continent?", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Question() { Id = 4, Name = "What's your favorite game genre?", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Question() { Id = 5, Name = "What's your favorite music genre?", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Question() { Id = 6, Name = "What's your favorite big tech company?", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
            });

            modelBuilder.Entity<Answer>().HasData(new Answer[]
            {
                new Answer() { Id = 1, QuestionId = 1, Name = "Earth", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 2, QuestionId = 1, Name = "Jupiter", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 3, QuestionId = 1, Name = "Venus", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 4, QuestionId = 1, Name = "Mercury", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 5, QuestionId = 1, Name = "Mars", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 6, QuestionId = 1, Name = "Saturn", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 7, QuestionId = 1, Name = "Neptune", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 8, QuestionId = 1, Name = "Uranus", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },

                new Answer() { Id = 9, QuestionId = 2, Name = "Cats", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 10, QuestionId = 2, Name = "Dogs", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 11, QuestionId = 2, Name = "Birds", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 12, QuestionId = 2, Name = "Fish", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 13, QuestionId = 2, Name = "Rabbits", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 14, QuestionId = 2, Name = "Guinea Pigs", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },

                new Answer() { Id = 15, QuestionId = 3, Name = "Europe", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 16, QuestionId = 3, Name = "South America", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 17, QuestionId = 3, Name = "Africa", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 18, QuestionId = 3, Name = "Asia", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 19, QuestionId = 3, Name = "North America", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 20, QuestionId = 3, Name = "Oceania", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 21, QuestionId = 3, Name = "Antarctica", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },

                new Answer() { Id = 22, QuestionId = 4, Name = "FPS", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 23, QuestionId = 4, Name = "MMO", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 24, QuestionId = 4, Name = "MOBA", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 25, QuestionId = 4, Name = "RTS", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 26, QuestionId = 4, Name = "Turn-based Strategy", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 27, QuestionId = 4, Name = "Roguelikes", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 28, QuestionId = 4, Name = "Open World", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 29, QuestionId = 4, Name = "Sports", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 30, QuestionId = 4, Name = "Simulation", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },

                new Answer() { Id = 31, QuestionId = 5, Name = "Rock", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 32, QuestionId = 5, Name = "Pop", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 33, QuestionId = 5, Name = "Hip Hop", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 34, QuestionId = 5, Name = "RnB", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 35, QuestionId = 5, Name = "Electronic", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 36, QuestionId = 5, Name = "Reggae", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },

                new Answer() { Id = 37, QuestionId = 6, Name = "Microsoft", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 38, QuestionId = 6, Name = "Apple", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 39, QuestionId = 6, Name = "Google", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 40, QuestionId = 6, Name = "Amazon", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 41, QuestionId = 6, Name = "IBM", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 42, QuestionId = 6, Name = "Intel", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Answer() { Id = 43, QuestionId = 6, Name = "Oracle", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
            });
        }

        public void Commit()
        {
            this.SaveChanges();
        }

        public Task CommitAsync()
        {
            return this.SaveChangesAsync();
        }

        public DbContext GetContext()
        {
            return this;
        }

        public void RunTransaction(Action transaction)
        {
            using (var context = this)
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        transaction.Invoke();
                        context.Commit();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task RunTransactionAsync(Func<Task> transaction)
        {
            using (var context = this)
            {
                using (var trans = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await transaction.Invoke();
                        await context.CommitAsync();
                        await trans.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        await trans.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        DbSet<T> IContext.Get<T>()
        {
            return this.Set<T>();
        }
    }
}
