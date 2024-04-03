using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.EndPoints;

public static class ArtistaExtensions
{
    // basicamente a gente adiciona esse método ao web application
    public static void AddEndPointsArtistas(this WebApplication app)
    {
        app.MapGet("/Artista", ([FromServices] DAL<Artista> dal) =>
        {
            return Results.Ok(dal.Listar());
        });

        app.MapGet("/Artista/{nome}", ([FromServices] DAL<Artista> dal,
            string nome) =>
        {
            var artista = dal.RecuperarPorNome(a => a.Nome.ToUpper().Equals(nome.ToUpper()));


            if (artista is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(artista);
        });

        app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal,
            [FromBody] Artista artista) =>
        {
            dal.Adicionar(artista);
            return Results.Ok();
        });

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
            artistaAtualizado.Nome = artista.Nome;
            artistaAtualizado.Bio = artista.Bio;
            artistaAtualizado.FotoPerfil = artista.FotoPerfil;
            dal.Atualizar(artistaAtualizado);
            return Results.Ok();
        });
    }
}
