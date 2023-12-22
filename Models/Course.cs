using System;
using System.Collections.Generic;

namespace High_School_Labb_3.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int Classroom { get; set; }

    public int FkFacultyId { get; set; }

    public virtual Faculty FkFaculty { get; set; } = null!;
}
