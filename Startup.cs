using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresMed.Data;
using PresMed.Helper;
using PresMed.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresMed {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();

            services.AddDbContext<BancoContext>(options => options.UseMySql(Configuration.GetConnectionString("BancoContext"), builder =>
                builder.MigrationsAssembly("PresMed")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<SeedingService>();
            services.AddScoped<SeedingProcedure>();
            services.AddScoped<SeedingClinicOpening>();
            services.AddScoped<SeedingMedicine>();
            services.AddScoped<SeedingCid>();
            services.AddScoped<IDoctorServices, DoctorServices>();
            services.AddScoped<IAssistantServices, AssistantServices>();
            services.AddScoped<IPatientServices, PatientServices>();
            services.AddScoped<IProceduresServices, ProceduresServices>();
            services.AddScoped<ITimeServices, TimeServices>();
            services.AddScoped<ISchedulingServices, SchedulingServices>();
            services.AddScoped<IClinicSetingsServices, ClinicSetingsServices>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ISessionUser, Session>();
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<IAttendanceServices, AttendanceServices>();
            services.AddScoped<ICidServices, CidServices>();


            services.AddSession(o => {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService, SeedingProcedure seedingProcedure, SeedingClinicOpening seedingClinicOpening, SeedingMedicine seedingMedicine, SeedingCid seedingCid) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                seedingService.Seed();
                seedingProcedure.Seed();
                seedingClinicOpening.Seed();
                seedingMedicine.Seed();
                seedingCid.Seed();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                seedingProcedure.Seed();
                seedingClinicOpening.Seed();
                seedingCid.Seed();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
