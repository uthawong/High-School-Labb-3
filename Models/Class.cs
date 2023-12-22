using System;
using System.Collections.Generic;

namespace High_School_Labb_3.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string Class1 { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
