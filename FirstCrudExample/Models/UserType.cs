using System;
using System.Collections.Generic;

namespace FirstCrudExample.Models;

public partial class Usertype
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Userlist> Userlists { get; set; } = new List<Userlist>();
}
