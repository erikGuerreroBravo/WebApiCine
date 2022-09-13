using Microsoft.EntityFrameworkCore;
using WebApiCine.Servicios;

namespace WebApiCine
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IAlmacenadorArchivos, ArchivosLocales>();
            //agregamos la extension para trabajar con todas las peticiones del tipo http.
            services.AddHttpContextAccessor();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //agregamos newtonSoft como libreria a nuestro pipedLIne
            services.AddControllers().AddNewtonsoftJson();
            services.AddEndpointsApiExplorer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //utilizamos esta pipedline para establecer que nuestra api puede retornar o mostrar archivos staticos como imagenes.
            app.UseStaticFiles();

            app.UseRouting();
                        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
                        
        }
    }
}
