using System.Data.Entity;

namespace DataAnnotations
{
    public class PlutoContext : DbContext
    {
        public PlutoContext()
            : base("name=PlutoContext")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        // we override this method since we want to customize the model
        // thi is called fluent API since we are using method calls to configure the model fluently like a story
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .IsRequired() //makes the Name property required (NOT NULL)
                .HasMaxLength(255); //sets the maximum length of the Name property to 255 characters

            modelBuilder.Entity<Course>()
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(2000);

            // configuring relationship to change Author_id to AuthorId
            modelBuilder.Entity<Course>()
                .HasRequired(c => c.Author) // each Course has one Author (required)
                .WithMany(a => a.Courses) // each Author has many Courses
                .HasForeignKey(c => c.AuthorId) // AuthorId property was needed to be created in Course class
                .WillCascadeOnDelete(false); // disables cascade delete, now if we delete an Author, it won't delete the related Courses

            base.OnModelCreating(modelBuilder);
        }
    }
}