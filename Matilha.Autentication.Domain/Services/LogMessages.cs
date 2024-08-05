namespace Matilha.Autentication.Domain.Services
{
    public static class LogMessages
    {
        public static readonly Dictionary<string, string> Messages = new Dictionary<string, string>
        {
            { "LoginAttempt", "Tentativa de login para o usuário {Username}" },
            { "LoginFailed", "Falha no login para o usuário {Username}" },
            { "LoginSuccessful", "Login bem-sucedido para o usuário {Username}" },
            { "GenerateRefreshTokenAttempt", "Tentativa de gerar token de atualização para o usuário {UserId}" },
            { "GenerateRefreshTokenSuccessful", "Token de atualização gerado com sucesso para o usuário {UserId}" },
            { "ValidateRefreshTokenAttempt", "Tentativa de validar token de atualização" },
            { "ValidateRefreshTokenFailed", "Falha na validação do token de atualização" },
            { "ValidateRefreshTokenSuccessful", "Token de atualização validado com sucesso" },
            { "InvalidateRefreshTokenAttempt", "Tentativa de invalidar token de atualização" },
            { "InvalidateRefreshTokenSuccessful", "Token de atualização invalidado com sucesso" },
            { "CreateSessionAttempt", "Tentativa de criar sessão para o usuário {UserId}" },
            { "CreateSessionSuccessful", "Sessão criada com sucesso para o usuário {UserId}" },
            { "GetSessionAttempt", "Tentativa de obter sessão com ID {SessionId}" },
            { "GetSessionNotFound", "Sessão não encontrada com ID {SessionId}" },
            { "GetSessionSuccessful", "Sessão obtida com sucesso com ID {SessionId}" },
            { "InvalidateSessionAttempt", "Tentativa de invalidar sessão com ID {SessionId}" },
            { "InvalidateSessionSuccessful", "Sessão invalidada com sucesso com ID {SessionId}" },
            { "CreateSessionServiceAttempt", "Tentativa de criar sessão no serviço para o usuário {UserId}" },
            { "CreateSessionServiceSuccessful", "Sessão criada com sucesso no serviço para o usuário {UserId}" },
            { "GetSessionServiceAttempt", "Tentativa de obter sessão no serviço com ID {SessionId}" },
            { "GetSessionServiceNotFound", "Sessão não encontrada no serviço com ID {SessionId}" },
            { "GetSessionServiceSuccessful", "Sessão obtida com sucesso no serviço com ID {SessionId}" },
            { "InvalidateSessionServiceAttempt", "Tentativa de invalidar sessão no serviço com ID {SessionId}" },
            { "InvalidateSessionServiceSuccessful", "Sessão invalidada com sucesso no serviço com ID {SessionId}" },
            { "AuthenticateUserAttempt", "Tentativa de autenticar o usuário {Username}" },
            { "AuthenticateUserFailed", "Falha na autenticação do usuário {Username}" },
            { "AuthenticateUserSuccessful", "Autenticação bem-sucedida para o usuário {Username}" },
            { "GenerateJwtToken", "Token JWT gerado para o usuário {UserId}" },
            { "GenerateSessionAttempt", "Tentativa de gerar sessão para o usuário {UserId}" },
            { "GenerateSessionSuccessful", "Sessão gerada com sucesso para o usuário {UserId}" },
            { "GenerateRefreshTokenForSessionAttempt", "Tentativa de gerar token de atualização para a sessão {SessionId}" },
            { "GenerateRefreshTokenForSessionSuccessful", "Token de atualização gerado com sucesso para a sessão {SessionId}" }
        };
    }
}
