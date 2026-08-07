# 0008 — Gestão de segredos em `appsettings`

## Status

**Superseded pelo [ADR-0011](0011-adocao-user-secrets.md)** — com a centralização do banco no Azure SQL, o time passou a usar `dotnet user-secrets` como método padrão de configurar segredos locais. Este documento é mantido como histórico da decisão original.

Implementado (com um item pendente de confirmação humana — ver Consequências).

## Contexto

Durante a análise inicial da arquitetura, dois problemas de segurança/configuração foram encontrados no histórico de commits do colega (`iuri.gp@sempreceub.com`):

1. `src/API/appsettings.json` (arquivo **versionado**, ao contrário de `appsettings.Development.json`) tinha marcadores de conflito de merge não resolvidos do Visual Studio (`<<<<<<< Updated upstream` / `>>>>>>> Stashed changes`), com uma connection string contendo um usuário de banco e um valor de senha commitados no repositório.
2. `appsettings.Development.json` estava sendo versionado (continha um valor de chave JWT), apesar do `.gitignore` já ter uma linha comentada sugerindo ignorá-lo.

O commit `870147f` do próprio Iuri já corrigiu os dois problemas antes desta revisão: zerou a connection string em `appsettings.json` e removeu `appsettings.Development.json` do versionamento, descomentando a entrada correspondente no `.gitignore`. O que faltava era um jeito de um novo desenvolvedor saber quais chaves precisa preencher localmente, já que o arquivo de exemplo não existia.

## Decisão

Criado `src/API/appsettings.Development.json.example`, com a mesma estrutura de `appsettings.json`, mas só com os campos que esse arquivo deixa vazios de propósito (`ConnectionStrings:ConexaoPadrao` e `Jwt:Key`) preenchidos com placeholders auto-explicativos. Documentado em `docs/Arquitetura_backend.md` o comando para copiar o exemplo e o que preencher em cada campo.

## Alternativas consideradas

- **Usar `dotnet user-secrets`** em vez de `appsettings.Development.json.example` — mais alinhado com a prática recomendada da Microsoft para segredos locais, mas descartado por ora para não introduzir uma ferramenta nova no fluxo de onboarding do time no meio da revisão de arquitetura; fica como possível ADR futuro se o time achar valioso.
- **Não versionar exemplo nenhum, só documentar em texto quais chaves existem** — descartada; um arquivo `.example` pronto para copiar reduz erro humano em relação a digitar a estrutura JSON do zero a partir de uma descrição em prosa.

## Consequências

- Onboarding de um novo dev (ou reconfiguração de máquina) agora é `cp appsettings.Development.json.example appsettings.Development.json` + preencher 2 campos, em vez de adivinhar a partir do código-fonte quais chaves o `Program.cs` espera.
- **Pendência que este ADR não resolve**: ainda falta confirmar com o Iuri se o valor de senha que apareceu no conflito de merge do `appsettings.json` era um placeholder literal ou uma senha real do SQL Server dele digitada por engano. Se for real, a senha do banco local dele deve ser trocada por precaução — isso não é algo que dá para verificar só pelo código, depende de resposta humana.
