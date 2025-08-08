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
            // The content was moved to the pertinen Configuration file
            base.OnModelCreating(modelBuilder);
        }
    }
}