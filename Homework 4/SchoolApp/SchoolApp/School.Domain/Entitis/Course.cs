using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Entitis
{
    public class Course
    {
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
    }
}
