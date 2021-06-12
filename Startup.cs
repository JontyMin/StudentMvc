using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentMvc.Models;

namespace StudentMvc
{
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //mvc
            services.AddMvc();

            //ʹ�ø���չ����
            services.AddHealthChecks();

            //services.AddControllersWithViews();
            services.AddDbContextPool<AppDbContext>(
                options=>options.UseSqlServer(Configuration.GetConnectionString("StudentMVCConnection"))
                );
            services.AddScoped<IStudentRepository, SQLStudentRepository>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // ��������Ĭ������

                // ��������ظ��ַ���
                options.Password.RequiredUniqueChars = 3;
                // ������һ����������ĸ�ַ�
                options.Password.RequireNonAlphanumeric = false;
                // ��С������֤
                options.Password.RequiredLength = 6;
                // �������Сд��ĸ
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;// ��д��ĸ
                options.Password.RequireDigit = false;// �����������
            }).AddEntityFrameworkStores<AppDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");//�����쳣

                app.UseStatusCodePagesWithRedirects("/Error/{0}");//404
            }

            

            app.UseStaticFiles();

            // ��Ȩ
            app.UseAuthentication();

            app.UseRouting();
            //app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
