using System;
using System.Collections.Generic;

namespace FirstCrudExample.Models;

public partial class Userlist
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string UserPassword { get; set; } = null!;

    public bool? LoginStatus { get; set; }

    public int UserTypeId { get; set; }

    public virtual Usertype UserType { get; set; } = null!;
}
