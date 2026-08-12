namespace API.Authorization
{
    // Nomes centralizados de AuthorizationPolicy — usados em [Authorize(Policy = ...)]
    // nos controllers e registrados em Program.cs (AddAuthorization). Existir aqui
    // como constante, em vez de "Gestor,Administrador" solto em cada atributo,
    // é o que evita o erro de digitação/esquecimento de role em endpoint novo.
    public static class AuthorizationPolicies
    {
        public const string PodeGerenciarUsuarios = nameof(PodeGerenciarUsuarios);
        public const string PodeGerenciarInstrutores = nameof(PodeGerenciarInstrutores);
        public const string PodeGerenciarAlunos = nameof(PodeGerenciarAlunos);
        public const string PodeEditarFichaMedica = nameof(PodeEditarFichaMedica);
        public const string PodeTrocarSenha = nameof(PodeTrocarSenha);
        public const string PodeAtualizarObjetivoAluno = nameof(PodeAtualizarObjetivoAluno);
        public const string PodeVerLogs = nameof(PodeVerLogs);
    }
}
