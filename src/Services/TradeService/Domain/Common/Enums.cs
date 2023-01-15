﻿namespace Trades.Domain.Common;

public enum Side
{
    BUY = 1,
    SELL = 2
}
public enum TradeStatus
{
    CREATE = 1,
    OPEN = 2,
    NOMINATED = 3,
    VALIDATED = 4,
    PROCESSING = 5,
    SETTLED = 6,
    CANCELLED = 7
}
