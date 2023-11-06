

namespace DienappApi.Models.SPModels;

public partial class GetAllJobs
{
    public int Jobid { get; set; }

    public int? Jobcategoryid { get; set; }

    public string? Jobname { get; set; }

    public string? Descripion { get; set; }

    public string? Remarks { get; set; }

    public string? Status { get; set; }

    public DateTime? Createddate { get; set; }
}
