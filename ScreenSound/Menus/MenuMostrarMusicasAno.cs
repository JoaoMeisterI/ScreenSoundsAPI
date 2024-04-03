using ScreenSound.Banco;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Menus;

internal class MenuMostrarMusicasAno : Menu
{
    public override void Executar(DAL<Artista> artistaDAL)
    {
        base.Executar(artistaDAL);
        ExibirTituloDaOpcao("Musicas filtradas por ano: ");
        var musicaDAL = new DAL<Musica>(new ScreenSoundContext());
        Console.Write("Digite o ano de lançamento das musicas: ");
        string anoMusica = Console.ReadLine()!;
        //Agora passa o select dentro dos parenteses como condição
        var listaMusicas = musicaDAL.RecuperarPorNome(a => a.AnoLancamento == Convert.ToInt32(anoMusica));
        if (listaMusicas.Any())
        {
            //esse artistaRecuperado não deixa de ser da classe artista
            Console.WriteLine($"\nMusicas do ano {anoMusica}:");
            foreach(var musica in listaMusicas)
            {
                Console.WriteLine($"Musica {musica} / Ano: {anoMusica} ");
            }
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO Ano {anoMusica} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
