﻿namespace ScreenSound.Core.Artistas;

public class AvaliacaoArtista
{
    public int ArtistaId { get; set; }
    public int PessoaId { get; set; }
    public virtual Artista? Artista { get; set; }
    public int Nota { get; set; }
}
