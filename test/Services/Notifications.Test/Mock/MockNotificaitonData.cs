using Notifications.Domain.Common;
using Notifications.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Test.Mock
{
    public class MockNotificaitonData
    {
        public static IQueryable<Notification> MockNotificationsData()
        {
            return new List<Notification>
            {
                new Notification {
                TradeId=new Guid("26f0b3af-0d7a-4285-80ac-c4928375e8e1"),
                SMSStatus=NotificaitonStatus.Sent,
                EmailStatus=NotificaitonStatus.Sent,
                EmailRetries=0,
                SentDate=DateTime.Now,
                Created=DateTime.Now,
                CreatedBy="AB",
                Id=new Guid("4636775b-b411-4b9a-9384-9be8278b7bd2"),
                LastModified=DateTime.Now,
                LastModifiedBy="AB"
                },
                new Notification {
                TradeId=new Guid("3caf131b-5393-4e09-b6ec-5fc8cd559574"),
                SMSStatus=NotificaitonStatus.Enqueue,
                EmailStatus=NotificaitonStatus.Failed,
                EmailRetries=3,
                SentDate=DateTime.Now,
                Created=DateTime.Now,
                CreatedBy="ABC",
                Id=new Guid("eac45248-09f9-4006-ab4a-721eb09cf019"),
                LastModified=DateTime.Now,
                LastModifiedBy="ABC"
                },
                new Notification {
                TradeId=new Guid("5efae644-1329-42e5-adf9-6b89796171ef"),
                SMSStatus=NotificaitonStatus.Failed,
                EmailStatus=NotificaitonStatus.Sent,
                EmailRetries=1,
                    SentDate=DateTime.Now,
                Created=DateTime.Now,
                CreatedBy="ABD",
                Id=new Guid("795e66a0-0862-4e4f-a336-c78a7da847d7"),
                LastModified=DateTime.Now,
                LastModifiedBy="ABD"
                }
            }.AsQueryable();
        }
        public static IQueryable<TradeNotification> MockTradeNotificationsData()
        {
            return new List<TradeNotification> {
         new TradeNotification {
             TradeId = new Guid("26f0b3af-0d7a-4285-80ac-c4928375e8e1"),
             IsActive =true
         },
         new TradeNotification {
             TradeId = new Guid("3caf131b-5393-4e09-b6ec-5fc8cd559574"),
             IsActive =true
         },
         new TradeNotification {
             TradeId = new Guid("5efae644-1329-42e5-adf9-6b89796171ef"),
             IsActive =true },
         new TradeNotification {
             TradeId = new Guid("0b449316-39e9-4db7-b136-59549471fbdf"),
             IsActive =false
         },
         new TradeNotification {
             TradeId = new Guid("e023e450-7675-4428-9d21-b4b19469cffe"),
             IsActive =true
         },
         new TradeNotification {
             TradeId = new Guid("63edbb65-063c-4f08-9669-d924d645f15a"),
             IsActive =true
         },

         }.AsQueryable();
        }
    }
}
