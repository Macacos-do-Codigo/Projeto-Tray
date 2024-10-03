using Eplace;
using Eplace.Models;
using Microsoft.EntityFrameworkCore;
using Eplace.Module;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configurar todos os servi�os antes de construir o app
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });

        // Adicionar DbContext e outros servi�os
        builder.Services.AddDbContext<eplaceDbContext>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Agora, cria o app
        var app = builder.Build();

        // Configura��o do middleware (ap�s a constru��o do app)
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Habilitar CORS
        app.UseCors("AllowAll");

        // Mapear rotas
        app.MapGet("/", () => "O Retorno!");

        // Rotas personalizadas
        app.EndpointsUsuario();
        app.EndpointsProduto();

        // Executar a aplica��o
        app.Run();
    }
}
