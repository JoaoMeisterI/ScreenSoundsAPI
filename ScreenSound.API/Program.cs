using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//O nosso DAL se repete vamos fazer de modo que não precise ficar colacando ele toda vez
builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
//CONFIGURAMOS RECURSOS NATIVOS PARA CRIAR ESSES OBJETOS QUE SERÃO UTILIZADOS NA APLICAÇÃO
//ISSO É CHAMADO DE INJEÇÃO DE DEPENDÊNCIA


builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

//Adicionando o Dal diretamente dos serviços 
app.MapGet("/Artista", ([FromServices] DAL<Artista> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artista/{nome}", ([FromServices] DAL<Artista> dal,
    string nome) =>
{
    var artista = dal.RecuperarPorNome(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
 
    //Se o user não estiver no cadastro
    if(artista is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(artista);
});
//Metodo para registar artista
//Tu vai ter que utilizar os métodos da API, vai fazer isso via conexão
app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal,
    [FromBody] Artista artista) =>
{
    dal.Adicionar(artista);
    return Results.Ok();
});
//Para remover artista
app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
{
    var artista = dal.RecuperarPor(x => x.Id == id);
    if (artista is null)
    {
        return Results.NotFound();
    }
    dal.Deletar(artista);
    return Results.NoContent();
});
app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal,
    [FromBody] Artista artista) =>
{
    var artistaAtualizado = dal.RecuperarPor(x => x.Id == artista.Id);
    if (artistaAtualizado is null)
    {
        return Results.NotFound();
    }
    artista.Nome = artistaAtualizado.Nome;
    artista.Bio = artistaAtualizado.Bio;
    artista.FotoPerfil = artistaAtualizado.FotoPerfil;
    return Results.Ok();
});
app.Run();
