using System;
using System.Collections.Generic;

namespace DienappApi.Models;

public partial class Requestotp
{
    public int Otpid { get; set; }

    public string? Otpphone { get; set; }

    public ulong? Isactive { get; set; }

    public DateTime? Createddate { get; set; }
}
