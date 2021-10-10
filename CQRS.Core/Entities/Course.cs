using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class Course : BaseEntity
    {
        public Course(Guid? campaignId, string title, decimal price)
        {
            CampaignId = campaignId;
            Title = title;
            Price = price;
        }
        // For Unit Test
        public Course(Guid id, Guid? campaignId, string title, decimal price)
        {
            Id = id;
            CampaignId = campaignId;
            Title = title;
            Price = price;
        }
        public Guid? CampaignId { get; protected set; }
        public string Title { get; protected set; }
        public decimal Price { get; protected set; }
        public virtual ICollection<Order> Orders { get; protected set; }
        public virtual Campaign Campaign{ get; protected set; }

        public void UpdateCampaign(Guid? campaignId)
        {
            CampaignId = campaignId;
        }
        public void UpdateTitle(string title)
        {
            Title = title;
        }
        public void UpdatePrice(decimal price)
        {
            Price = price;
        }
        public void UpdateIsActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}

