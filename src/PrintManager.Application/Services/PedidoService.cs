using PrintManager.Application.DTOs;
using PrintManager.Application.Interfaces;
using PrintManager.Domain.Entities;
using PrintManager.Domain.Enums;

namespace PrintManager.Application.Services;

public class PedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IFileStorageService _fileStorageService;

    public PedidoService(IPedidoRepository pedidoRepository, IFileStorageService fileStorageService)
    {
        _pedidoRepository = pedidoRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Guid> CriarPedidoAsync(CriarPedidoDto dto, CancellationToken cancellationToken = default)
    {
        var pedido = new Pedido
        {
            Id = Guid.NewGuid(),
            NomeCliente = dto.NomeCliente.Trim(),
            Telefone = dto.Telefone.Trim(),
            DescricaoServico = dto.DescricaoServico.Trim(),
            Quantidade = dto.Quantidade,
            Status = StatusPedido.Aguardando,
            DataCriacao = DateTime.UtcNow
        };

        foreach (var arquivo in dto.Arquivos)
        {
            var caminhoRelativo = await _fileStorageService.SalvarArquivoAsync(
                arquivo.Conteudo,
                arquivo.NomeArquivo,
                cancellationToken);

            pedido.Arquivos.Add(new PedidoArquivo
            {
                Id = Guid.NewGuid(),
                PedidoId = pedido.Id,
                NomeArquivo = arquivo.NomeArquivo,
                CaminhoArquivo = caminhoRelativo
            });
        }

        await _pedidoRepository.AdicionarAsync(pedido, cancellationToken);
        return pedido.Id;
    }

    public async Task<IReadOnlyList<PedidoListagemDto>> ListarAsync(
        StatusPedido? status,
        DateTime? periodoInicial,
        DateTime? periodoFinal,
        CancellationToken cancellationToken = default)
    {
        var pedidos = await _pedidoRepository.ListarAsync(status, periodoInicial, periodoFinal, cancellationToken);

        return pedidos
            .Select(p => new PedidoListagemDto
            {
                Id = p.Id,
                NomeCliente = p.NomeCliente,
                Telefone = p.Telefone,
                Status = p.Status,
                DataCriacao = p.DataCriacao
            })
            .ToList();
    }

    public async Task<PedidoDetalheDto?> ObterDetalheAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var pedido = await _pedidoRepository.ObterPorIdAsync(id, cancellationToken);
        if (pedido is null)
            return null;

        return new PedidoDetalheDto
        {
            Id = pedido.Id,
            NomeCliente = pedido.NomeCliente,
            Telefone = pedido.Telefone,
            DescricaoServico = pedido.DescricaoServico,
            Quantidade = pedido.Quantidade,
            ValorPago = pedido.ValorPago,
            Status = pedido.Status,
            DataCriacao = pedido.DataCriacao,
            DataFinalizacao = pedido.DataFinalizacao,
            ObservacaoInterna = pedido.ObservacaoInterna,
            Arquivos = pedido.Arquivos
                .Select(a => new PedidoArquivoDto
                {
                    Id = a.Id,
                    NomeArquivo = a.NomeArquivo,
                    CaminhoArquivo = a.CaminhoArquivo
                })
                .ToList(),
            WhatsAppUrl = WhatsAppHelper.GerarUrl(pedido.Telefone, pedido.NomeCliente)
        };
    }

    public async Task<bool> AtualizarPedidoAsync(Guid id, AtualizarPedidoDto dto, CancellationToken cancellationToken = default)
    {
        var pedido = await _pedidoRepository.ObterPorIdAsync(id, cancellationToken);
        if (pedido is null)
            return false;

        pedido.Status = dto.Status;
        pedido.ValorPago = dto.ValorPago;
        pedido.ObservacaoInterna = dto.ObservacaoInterna?.Trim();

        if (dto.Finalizar || dto.Status == StatusPedido.Finalizado)
        {
            pedido.Status = StatusPedido.Finalizado;
            pedido.DataFinalizacao ??= DateTime.UtcNow;
        }

        await _pedidoRepository.AtualizarAsync(pedido, cancellationToken);
        return true;
    }
}
