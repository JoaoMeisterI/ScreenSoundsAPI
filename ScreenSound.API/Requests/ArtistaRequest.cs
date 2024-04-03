namespace ScreenSound.API.Requests;

//Somente as infos que serão utilizadas
public record ArtistaRequest(string nome, string bio);

//Agora temos que definir no EndPoints o objeto artistaRequest no lugar de artista no body