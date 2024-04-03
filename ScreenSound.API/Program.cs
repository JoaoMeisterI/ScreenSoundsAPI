using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using ScreenSound.API.EndPoints;


var builder = WebApplication.CreateBuilder(args);
//O nosso DAL se repete vamos fazer de modo que não precise ficar colacando ele toda vez
builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
//Adicionando documentação pelo swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//CONFIGURAMOS RECURSOS NATIVOS PARA CRIAR ESSES OBJETOS QUE SERÃO UTILIZADOS NA APLICAÇÃO
//ISSO É CHAMADO DE INJEÇÃO DE DEPENDÊNCIA


builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

//ARTISTAS
app.AddEndPointsArtistas();
app.AddEndPointsMusicas();
//MUSICAS
//Adicionando para usar o swagger
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
