using System;
using System.Collections.Generic;

namespace FirstCrudExample.Models;

public partial class Student
{
    public long StdId { get; set; }

    public string StdName { get; set; } = null!;

    public string? StdAddress { get; set; }

    public DateOnly JoinDate { get; set; }

    public int FacultyId { get; set; }

    public virtual Faculty Faculty { get; set; } = null!;
}
