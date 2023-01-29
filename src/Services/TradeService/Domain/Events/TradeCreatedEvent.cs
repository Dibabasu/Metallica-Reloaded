using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trades.Domain.Common;
using Trades.Domain.Entity;

namespace Trades.Domain.Events
{
    public class TradeCreatedEvent : BaseEvent
    {
        public TradeCreatedEvent(Trade trade)
        {
            Trade = trade;
        }
        public Trade Trade { get; }
    }
}
