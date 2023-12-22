using System;
using System.Collections.Generic;

namespace High_School_Labb_3.Models;

public partial class Enrollment
{
    public int FkCourseId { get; set; }

    public int FkStudentId { get; set; }

    public int GradeInfo { get; set; }

    public DateTime DateOfGrade { get; set; }

    public virtual Course FkCourse { get; set; } = null!;

    public virtual Student FkStudent { get; set; } = null!;
}
