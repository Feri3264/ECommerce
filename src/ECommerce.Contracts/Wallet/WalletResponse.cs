namespace ECommerce.Contracts.Wallet;

public record WalletResponse
    (Guid walletId,
        Guid userId,
        int LastCredit,
        int CurrentCredit,
        int DiffCredit);