namespace WorkflowOS.Domain.Entities;

public class Usuario : BaseEntity
{
    public string Nome { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string SenhaHash { get; private set; } = string.Empty;

    public bool Ativo { get; private set; }

    protected Usuario()
    {
    }

    public Usuario(
        string nome,
        string email,
        string senhaHash)
    {
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Ativo = true;

        DataCriacao = DateTime.UtcNow;
    }
}