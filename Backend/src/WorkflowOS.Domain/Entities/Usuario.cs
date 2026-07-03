namespace WorkflowOS.Domain.Entities;

public sealed class Usuario : BaseEntity
{
    private Usuario()
    {
    }

    private Usuario(
        string nome,
        string email,
        string senhaHash)
    {
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Ativo = true;
    }

    public string Nome { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string SenhaHash { get; private set; } = string.Empty;

    public bool Ativo { get; private set; }

    public static Usuario Criar(
        string nome,
        string email,
        string senhaHash)
    {
        return new Usuario(
            nome.Trim(),
            email.Trim().ToLowerInvariant(),
            senhaHash);
    }

    public void AlterarNome(string nome)
    {
        Nome = nome.Trim();
        AtualizarDataModificacao();
    }

    public void AlterarSenha(string senhaHash)
    {
        SenhaHash = senhaHash;
        AtualizarDataModificacao();
    }

    public void Ativar()
    {
        Ativo = true;
        AtualizarDataModificacao();
    }

    public void Desativar()
    {
        Ativo = false;
        AtualizarDataModificacao();
    }
}