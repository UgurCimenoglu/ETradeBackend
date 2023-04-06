using ETradeBackend.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Domain.Entities.Identity;

namespace ETradeBackend.Domain.Entities
{
    public class Basket : BaseEntity
    {
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public Order Order { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }

    }
}
