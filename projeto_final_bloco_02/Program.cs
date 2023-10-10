using Microsoft.EntityFrameworkCore;
using projeto_final_bloco_02.Models;
using FluentValidation;
using projeto_final_bloco_02.Data;
using projeto_final_bloco_02.Service;
using projeto_final_bloco_02.Service.Implements;
using projeto_final_bloco_02.Validator;



internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

        var connectionString = builder.Configuration.
            GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<FarmaciaDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        // Entidades
        builder.Services.AddTransient<IValidator<Produto>, ProdutoValidator>();
        builder.Services.AddTransient<IValidator<Categoria>, CategoriaValidator>();

        // Serviços
        builder.Services.AddScoped<IProdutoService, ProdutoService>();
        builder.Services.AddScoped<ICategoriaService, CategoriaService>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options => {
                options.AddPolicy(name: "MyPolicy",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

        var app = builder.Build();

        using (var scope = app.Services.CreateAsyncScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<FarmaciaDbContext>();
            dbContext.Database.EnsureCreated();
        }

        app.UseDeveloperExceptionPage();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("MyPolicy");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}