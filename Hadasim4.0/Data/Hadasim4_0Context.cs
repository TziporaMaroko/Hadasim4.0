using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hadasim4._0.Models;

namespace Hadasim4._0.Data;

public class Hadasim4_0Context : DbContext
{
    public Hadasim4_0Context (DbContextOptions<Hadasim4_0Context> options)
        : base(options)
    {
    }

    public DbSet<MemberVaccinationRelation> MemberVaccinationRelation { get; set; }
    public DbSet<Vaccination> Vaccination { get; set; }
    public DbSet<Member> Member { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MemberVaccinationRelation>()
            .HasKey(mvr => new { mvr.VaccinationId, mvr.MemberId });

  
    }

}

    

    


