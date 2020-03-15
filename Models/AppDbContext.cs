using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMvc.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //初始化数据
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id =1,
                    Name="Jonty",
                    ClassName = ClassNameEnum.ThreeGrade,
                    Email = "501211312@qq.com"
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
