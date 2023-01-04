using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trades.Application.Trades.Queries;
using Trades.Domain.Common;
using Trades.Domain.Entity;

namespace Trades.Test.Mocks
{
    public class MockTradeData    {
        public static IQueryable<Trade> MockQueryableTradeData()
        {
            return new List<Trade>
            {
                new Trade {
                CommoditiesIdentifier="AU",
                CounterpartiesIdentifier="AAPL",
                Created=DateTime.Now,
                CreatedBy="AB",
                Id=new Guid("26f0b3af-0d7a-4285-80ac-c4928375e8e1"),
                IsDeleted=false,
                IsSubmitted=false,
                LastModified=DateTime.Now,
                LastModifiedBy="AB",
                LocationIdentifier="Kol",
                Price=11,
                Quantity=1,
                Side=Side.BUY,
                TradeDate=DateTime.Now,
                TradeStatus =TradeStatus.NOMINATED
                },
                new Trade {
                CommoditiesIdentifier="AG",
                CounterpartiesIdentifier="AAPL",
                Created=DateTime.Now,
                CreatedBy="ABC",
                Id=new Guid("3caf131b-5393-4e09-b6ec-5fc8cd559574"),
                IsDeleted=false,
                IsSubmitted=false,
                LastModified=DateTime.Now,
                LastModifiedBy="ABC",
                LocationIdentifier="Bom",
                Price=113,
                Quantity=31,
                Side=Side.SELL,
                TradeDate=DateTime.Now,
                TradeStatus =TradeStatus.OPEN
                },
                new Trade {
                CommoditiesIdentifier="FE",
                CounterpartiesIdentifier="AAPL",
                Created=DateTime.Now,
                CreatedBy="ABD",
                Id=new Guid("5efae644-1329-42e5-adf9-6b89796171ef"),
                IsDeleted=false,
                IsSubmitted=false,
                LastModified=DateTime.Now,
                LastModifiedBy="ABD",
                LocationIdentifier="DHL",
                Price=121,
                Quantity=21,
                Side=Side.BUY,
                TradeDate=DateTime.Now,
                TradeStatus =TradeStatus.SETTLED
                },
            }.AsQueryable();
        }
    }
}
