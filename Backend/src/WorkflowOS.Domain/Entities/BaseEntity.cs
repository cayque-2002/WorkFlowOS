namespace WorkflowOS.Domain.Entities;

public abstract class BaseEntity
{
    public long Id { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAlteracao { get; set; }

    protected BaseEntity()
    {
        DataCriacao = DateTime.UtcNow;
    }
}
