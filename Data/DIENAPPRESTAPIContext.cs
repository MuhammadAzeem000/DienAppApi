﻿using DienappApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DienappApi.Models.SPModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;


namespace DienappApi.Data;

public partial class DIENAPPRESTAPIContext : IdentityDbContext<ApplicationUser>
{
    public DIENAPPRESTAPIContext(DbContextOptions<DIENAPPRESTAPIContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<GetAllJobs> GetAllJobs { get; set; }

    public virtual DbSet<Jobcategory> Jobcategories { get; set; }

    public virtual DbSet<Jobpayment> Jobpayments { get; set; }

    public virtual DbSet<Jobrequest> Jobrequests { get; set; }

    public virtual DbSet<Managmentcompany> Managmentcompanies { get; set; }

    public virtual DbSet<Navigationjob> Navigationjobs { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Providerdetail> Providerdetails { get; set; }

    public virtual DbSet<Provideremployee> Provideremployees { get; set; }

    public virtual DbSet<Rate> Rates { get; set; }

    public virtual DbSet<Requestotp> Requestotps { get; set; }

    public virtual DbSet<Seeker> Seekers { get; set; }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedRole(modelBuilder);

        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<ApplicationUser>()
        .Property(e => e.Name)
        .HasMaxLength(100);

        modelBuilder.Entity<GetAllJobs>(entity => entity.HasKey(e => e.Jobid));

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Jobid).HasName("PRIMARY");

            entity
                .ToTable("jobs")
                .UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.Jobcategoryid, "jobcategoryid");

            entity.Property(e => e.Jobid)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("jobid");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Descripion)
                .HasMaxLength(250)
                .HasColumnName("descripion");
            entity.Property(e => e.Jobcategoryid)
                .HasColumnType("int(11)")
                .HasColumnName("jobcategoryid");
            entity.Property(e => e.Jobname)
                .HasMaxLength(50)
                .HasColumnName("jobname");
            entity.Property(e => e.LatValue)
                .HasMaxLength(50)
                .HasColumnName("lat_value");
            entity.Property(e => e.LongValue)
                .HasMaxLength(50)
                .HasColumnName("long_value");
            entity.Property(e => e.ProviderId)
                .HasColumnType("int(11)")
                .HasColumnName("providerId");
            entity.Property(e => e.Remarks)
                .HasMaxLength(100)
                .HasColumnName("remarks");
            entity.Property(e => e.SeekerId)
                .HasColumnType("int(11)")
                .HasColumnName("seekerId");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Jobcategory>(entity =>
        {
            entity.HasKey(e => e.Jobcategoryid).HasName("PRIMARY");

            entity
                .ToTable("jobcategories")
                .UseCollation("utf8mb4_bin");

            entity.Property(e => e.Jobcategoryid)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("jobcategoryid");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(100)
                .HasColumnName("imagepath");
            entity.Property(e => e.Jobcategoryname)
                .HasMaxLength(50)
                .HasColumnName("jobcategoryname");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Jobpayment>(entity =>
        {
            entity.HasKey(e => e.JobpaymentId).HasName("PRIMARY");

            entity
                .ToTable("jobpayment")
                .UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.Jobid, "jobid");

            entity.HasIndex(e => e.Sekkerid, "sekkerid");

            entity.Property(e => e.JobpaymentId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("jobpaymentId");
            entity.Property(e => e.Amount)
                .HasMaxLength(50)
                .HasColumnName("amount");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Jobid)
                .HasColumnType("int(11)")
                .HasColumnName("jobid");
            entity.Property(e => e.Sekkerid)
                .HasColumnType("int(11)")
                .HasColumnName("sekkerid");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Jobrequest>(entity =>
        {
            entity.HasKey(e => e.Jobrequestid).HasName("PRIMARY");

            entity
                .ToTable("jobrequests")
                .UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.Jobid, "jobid");

            entity.HasIndex(e => e.Rateid, "rateid");

            entity.HasIndex(e => e.Seekerid, "seekerid");

            entity.Property(e => e.Jobrequestid)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("jobrequestid");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Jobid)
                .HasColumnType("int(11)")
                .HasColumnName("jobid");
            entity.Property(e => e.Rateid)
                .HasColumnType("int(11)")
                .HasColumnName("rateid");
            entity.Property(e => e.Seekerid)
                .HasColumnType("int(11)")
                .HasColumnName("seekerid");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Managmentcompany>(entity =>
        {
            entity.HasKey(e => e.ManagmentId).HasName("PRIMARY");

            entity
                .ToTable("managmentcompany")
                .UseCollation("utf8mb4_bin");

            entity.Property(e => e.ManagmentId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("managmentId");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Logopath)
                .HasMaxLength(100)
                .HasColumnName("logopath");
            entity.Property(e => e.ManagmentName)
                .HasMaxLength(50)
                .HasColumnName("managmentName");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Navigationjob>(entity =>
        {
            entity.HasKey(e => e.NavigationjobId).HasName("PRIMARY");

            entity
                .ToTable("navigationjob")
                .UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.JobId, "jobId");

            entity.Property(e => e.NavigationjobId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("navigationjobId");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.JobId)
                .HasColumnType("int(11)")
                .HasColumnName("jobId");
            entity.Property(e => e.LatValue)
                .HasMaxLength(50)
                .HasColumnName("lat_value");
            entity.Property(e => e.LongValue)
                .HasMaxLength(50)
                .HasColumnName("long_value");
            entity.Property(e => e.NavigationName)
                .HasMaxLength(50)
                .HasColumnName("navigationName");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("PRIMARY");

            entity
                .ToTable("providers")
                .UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.ManagmentId, "managmentId");

            entity.Property(e => e.ProviderId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("providerId");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Createdate)
                .HasColumnType("datetime")
                .HasColumnName("createdate");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.ManagmentId)
                .HasColumnType("int(11)")
                .HasColumnName("managmentId");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Providername)
                .HasMaxLength(50)
                .HasColumnName("providername");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Providerdetail>(entity =>
        {
            entity.HasKey(e => e.Providerdetailsid).HasName("PRIMARY");

            entity
                .ToTable("providerdetails")
                .UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.Providerid, "providerid");

            entity.Property(e => e.Providerdetailsid)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("providerdetailsid");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Ein)
                .HasMaxLength(100)
                .HasColumnName("ein");
            entity.Property(e => e.Facebbok)
                .HasMaxLength(100)
                .HasColumnName("facebbok");
            entity.Property(e => e.Instagram)
                .HasMaxLength(100)
                .HasColumnName("instagram");
            entity.Property(e => e.Insuranceinfo)
                .HasMaxLength(100)
                .HasColumnName("insuranceinfo");
            entity.Property(e => e.LatValue)
                .HasMaxLength(50)
                .HasColumnName("lat_value");
            entity.Property(e => e.Linkdin)
                .HasMaxLength(100)
                .HasColumnName("linkdin");
            entity.Property(e => e.LongValue)
                .HasMaxLength(50)
                .HasColumnName("long_value");
            entity.Property(e => e.Providerid)
                .HasColumnType("int(11)")
                .HasColumnName("providerid");
            entity.Property(e => e.Rateid)
                .HasColumnType("int(11)")
                .HasColumnName("rateid");
            entity.Property(e => e.Referrals)
                .HasMaxLength(100)
                .HasColumnName("referrals");
            entity.Property(e => e.Taxcertificate)
                .HasMaxLength(100)
                .HasColumnName("taxcertificate");
            entity.Property(e => e.Twitter)
                .HasMaxLength(100)
                .HasColumnName("twitter");
            entity.Property(e => e.W9)
                .HasMaxLength(100)
                .HasColumnName("w-9");
        });

        modelBuilder.Entity<Provideremployee>(entity =>
        {
            entity.HasKey(e => e.Provideremployeeid).HasName("PRIMARY");

            entity
                .ToTable("provideremployees")
                .UseCollation("utf8mb4_bin");

            entity.HasIndex(e => e.Providerid, "providerid");

            entity.Property(e => e.Provideremployeeid)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("provideremployeeid");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Employeename)
                .HasMaxLength(50)
                .HasColumnName("employeename");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Photopath)
                .HasMaxLength(100)
                .HasColumnName("photopath");
            entity.Property(e => e.Providerid)
                .HasColumnType("int(11)")
                .HasColumnName("providerid");
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasKey(e => e.Rateid).HasName("PRIMARY");

            entity
                .ToTable("rates")
                .UseCollation("utf8mb4_bin");

            entity.Property(e => e.Rateid)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("rateid");
            entity.Property(e => e.Createdate)
                .HasColumnType("datetime")
                .HasColumnName("createdate");
            entity.Property(e => e.Price)
                .HasColumnType("int(11)")
                .HasColumnName("price");
            entity.Property(e => e.Ratename)
                .HasMaxLength(50)
                .HasColumnName("ratename");
        });

        modelBuilder.Entity<Requestotp>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("requestotp")
                .UseCollation("utf8mb4_bin");

            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Isactive).HasColumnType("bit(1)");
            entity.Property(e => e.Otpid)
                .HasColumnType("int(11)")
                .HasColumnName("otpid");
            entity.Property(e => e.Otpphone)
                .HasMaxLength(50)
                .HasColumnName("otpphone");
        });

        modelBuilder.Entity<Seeker>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("seekers")
                .UseCollation("utf8mb4_bin");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.IsOtpverified)
                .HasColumnType("bit(1)")
                .HasColumnName("IsOTPVerified");
            entity.Property(e => e.LatValue)
                .HasMaxLength(50)
                .HasColumnName("lat_value");
            entity.Property(e => e.LongValue)
                .HasMaxLength(50)
                .HasColumnName("long_value");
            entity.Property(e => e.ManagmentId)
                .HasColumnType("int(11)")
                .HasColumnName("managmentId");
            entity.Property(e => e.OptId)
                .HasColumnType("int(11)")
                .HasColumnName("optId");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Skeerid)
                .HasColumnType("int(11)")
                .HasColumnName("skeerid");
            entity.Property(e => e.Skeername)
                .HasMaxLength(100)
                .HasColumnName("skeername");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    private void SeedRole(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData
        (
            new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
            new IdentityRole() { Name = "Provider", ConcurrencyStamp = "2", NormalizedName = "PROVIDER" },
            new IdentityRole() { Name = "Seeker", ConcurrencyStamp = "3", NormalizedName = "SEEKER" }
        );
    }
}
