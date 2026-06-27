using Microsoft.EntityFrameworkCore;
using PrintManager.Domain.Entities;
using PrintManager.Domain.Enums;

namespace PrintManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PedidoArquivo> PedidoArquivos => Set<PedidoArquivo>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NomeCliente).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Telefone).HasMaxLength(20).IsRequired();
            entity.Property(e => e.DescricaoServico).HasMaxLength(2000).IsRequired();
            entity.Property(e => e.ValorPago).HasPrecision(18, 2);
            entity.Property(e => e.ObservacaoInterna).HasMaxLength(2000);
            entity.Property(e => e.Status).HasConversion<int>();
            entity.HasMany(e => e.Arquivos)
                .WithOne(a => a.Pedido)
                .HasForeignKey(a => a.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PedidoArquivo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NomeArquivo).HasMaxLength(500).IsRequired();
            entity.Property(e => e.CaminhoArquivo).HasMaxLength(1000).IsRequired();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Login).HasMaxLength(100).IsRequired();
            entity.HasIndex(e => e.Login).IsUnique();
            entity.Property(e => e.SenhaHash).HasMaxLength(500).IsRequired();
        });
    }
}
