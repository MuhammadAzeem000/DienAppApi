using System;
using System.Collections.Generic;

namespace DienappApi.Models;

public partial class Rate
{
    public int Rateid { get; set; }

    public string? Ratename { get; set; }

    public int? Price { get; set; }

    public DateTime? Createdate { get; set; }
}
