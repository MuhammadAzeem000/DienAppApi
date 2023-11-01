using System;
using System.Collections.Generic;

namespace DienappApi.Models;

public partial class Provider
{
    public int ProviderId { get; set; }

    public int? ManagmentId { get; set; }

    public string? Providername { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Status { get; set; }

    public DateTime? Createdate { get; set; }
}
