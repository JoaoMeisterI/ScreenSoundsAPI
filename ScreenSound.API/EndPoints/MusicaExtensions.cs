using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.EndPoints;

public static class MusicaExtensions
{
    public static void AddEndPointsMusicas(this WebApplication app)
    {
        app.MapGet("/Musica", ([FromServices] DAL<Musica> dal) =>
        {
            return Results.Ok(dal.Listar());
        });

        app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal,
            string nome) =>
        {
            var musica = dal.RecuperarPorNome(a => a.Nome.ToUpper().Equals(nome.ToUpper()));


            if (musica is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(musica);
        });

        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal,
            [FromBody] Musica musica) =>
        {
            dal.Adicionar(musica);
            return Results.Ok();
        });

        app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
        {
            var musica = dal.RecuperarPor(x => x.Id == id);
            if (musica is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(musica);
            return Results.NoContent();
        });
        app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal,
            [FromBody] Musica musica) =>
        {
            var musicaAtualizado = dal.RecuperarPor(x => x.Id == musica.Id);
            if (musicaAtualizado is null)
            {
                return Results.NotFound();
            }
            musicaAtualizado.Nome = musica.Nome;
            musicaAtualizado.Artista = musicaAtualizado.Artista;
            musicaAtualizado.AnoLancamento = musica.AnoLancamento;
            dal.Atualizar(musicaAtualizado);
            return Results.Ok();
        });

    }
}
 
