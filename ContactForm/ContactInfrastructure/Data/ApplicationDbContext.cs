namespace ContactInfrastructure.Data
{
    using ContactCore.Entities;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageText> LanguageTexts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactForm>()
                .Property(c => c.RowVersion)
                .IsRowVersion();

            // Seed data for Languages
            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Code = "en", Name = "English" },
                new Language { Id = 2, Code = "es", Name = "Spanish" }
            );

            // Seed data for LanguageTexts
            modelBuilder.Entity<LanguageText>().HasData(
                new LanguageText { Id = 1, LanguageId = 1, Key = "form_title", Text = "Contact Form" },
                new LanguageText { Id = 2, LanguageId = 1, Key = "submit_button", Text = "Submit" },
                new LanguageText { Id = 3, LanguageId = 2, Key = "form_title", Text = "Formulario de Contacto" },
                new LanguageText { Id = 4, LanguageId = 2, Key = "submit_button", Text = "Enviar" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
