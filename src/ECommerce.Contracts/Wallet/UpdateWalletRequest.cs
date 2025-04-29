namespace ECommerce.Contracts.Wallet;

public record UpdateWalletRequest
    (Guid walletId, int amount);