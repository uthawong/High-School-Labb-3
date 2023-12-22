using System;
using System.Collections.Generic;

namespace High_School_Labb_3.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Major { get; set; } = null!;

    public int FkClassId { get; set; }

    public virtual Class FkClass { get; set; } = null!;
}
