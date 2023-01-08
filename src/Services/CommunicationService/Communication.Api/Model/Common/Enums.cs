namespace Communications.Api.Model.Common
{
    public enum NotificaitonStatus
    {
        Sent,
        Pending,
        Enqueue,
        Failed,
    }
    public enum Side
    {
        BUY,
        SELL
    }
    public enum TradeStatus
    {
        CREATE,
        OPEN,
        NOMINATED,
        VALIDATED,
        PROCESSING,
        SETTLED,
        CANCELLED
    }
}
