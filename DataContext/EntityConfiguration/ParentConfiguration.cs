using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.EntityConfiguration
{
    public  class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {      
            public void Configure(EntityTypeBuilder<Parent> builder)
            {
                builder.HasKey(p => p.Id);
            }        
    }
}
