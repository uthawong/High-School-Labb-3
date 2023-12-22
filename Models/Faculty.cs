using System;
using System.Collections.Generic;

namespace High_School_Labb_3.Models;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public int FkRoleId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Role FkRole { get; set; } = null!;
}
