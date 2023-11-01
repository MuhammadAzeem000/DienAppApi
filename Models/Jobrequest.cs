using System;
using System.Collections.Generic;

namespace DienappApi.Models;

public partial class Jobrequest
{
    public int Jobrequestid { get; set; }

    public int? Jobid { get; set; }

    public int? Seekerid { get; set; }

    public int? Rateid { get; set; }

    public string? Status { get; set; }

    public DateTime? Createddate { get; set; }
}
