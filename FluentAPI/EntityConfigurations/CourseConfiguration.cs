using DataAnnotations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAPI.EntityConfigurations
{
    public class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        // The order of the propertiess it's up to you, instructor suggest the following order:
        public CourseConfiguration()
        {
            // 1. Override the table name (Imagine we do so, but if you need to do so, do it just as commented below)
            //ToTable("tbl_course"); // always at the very beginning

            // 2. Override the primary keys
            HasKey(c => c.Id);

            // 3. Override property configurations (alphabetically ordered)
            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(2000);

            Property(c => c.Name)
                .IsRequired() //makes the Name property required (NOT NULL)
                .HasMaxLength(255); //sets the maximum length of the Name property to 255 characters

            // 4. Configure relationships (alphabetically ordered)
            HasRequired(c => c.Author) // each Course has one Author (required)
                .WithMany(a => a.Courses) // each Author has many Courses
                .HasForeignKey(c => c.AuthorId) // AuthorId property was needed to be created in Course class
                .WillCascadeOnDelete(false); // disables cascade delete, now if we delete an Author, it won't delete the related Courses

            // configuring one-to-one relationship between Course and Cover to solve: Unable to determine the principal
            // end of an association between the types
            HasRequired(course => course.Cover)
                .WithRequiredPrincipal(cover => cover.Course);

            // configuring the many-to-many relationship between Tags and Courses to rename the join table to CourseTags
            HasMany(c => c.Tags) // each Course has many Tags
                .WithMany(t => t.Courses) // each Tag has many Courses
                .Map(m =>
                {
                    m.ToTable("CourseTags"); // specifies the name of the join table
                    m.MapLeftKey("CourseId"); // specifies the left key name (Course_Id)
                    m.MapRightKey("TagId"); // specifies the right key name (Tag_Id)
                });

        }
    }
}
