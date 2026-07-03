namespace WorkflowOS.Domain.Entities;

public abstract class BaseEntity
{
    public long Id { get; protected set; }

    public DateTime DataCriacao { get; protected set; } = DateTime.UtcNow;

    public DateTime? DataAtualizacao { get; protected set; }

    protected void AtualizarDataModificacao()
    {
        DataAtualizacao = DateTime.UtcNow;
    }
}