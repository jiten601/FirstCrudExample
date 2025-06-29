using System;
using System.Collections.Generic;

namespace FirstCrudExample.Models;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public string FacultyName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
