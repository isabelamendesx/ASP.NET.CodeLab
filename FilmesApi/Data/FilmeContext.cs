using FilmesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> opts)
        : base(opts)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Sessao tem uma chave dupla (id do filme, id cinema)
        builder.Entity<Sessao>()
            .HasKey(sessao => new { sessao.FilmeId, sessao.CinemaId } );

        // Sessao tem um Cinema - O Cinema tem uma ou mais Sessões - A chave estrangeira da Sessao é o id do Cinema        
        builder.Entity<Sessao>()
            .HasOne(sessao => sessao.Cinema)
            .WithMany(cinema => cinema.Sessoes)
            .HasForeignKey(sessao => sessao.CinemaId);

        // Sessao tem um Filme - O Filme tem uma ou mais Sessões - A chave estrangeira é o id do Filme
        builder.Entity<Sessao>()
            .HasOne(sessao => sessao.Filme)
            .WithMany(cinema => cinema.Sessoes)
            .HasForeignKey(sessao => sessao.FilmeId);

        // Endereco tem um Cinema - Cinema tem um Endereco - A deleção não deve ser em cascata
        builder.Entity<Endereco>()
            .HasOne(endereco => endereco.Cinema)
            .WithOne(cinema => cinema.Endereco)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }

    public DbSet<Sessao> Sessoes { get; set; }
}
