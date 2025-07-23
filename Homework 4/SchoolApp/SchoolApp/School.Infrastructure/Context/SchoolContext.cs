using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using School.Domain.Entities;



namespace School.Infrastructure.Context
{
    public class SchoolContext
    {
        public SchoolContext() : base("name=SchoolConnection") { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
