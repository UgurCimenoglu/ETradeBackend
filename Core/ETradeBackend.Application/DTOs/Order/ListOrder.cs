using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.DTOs.Order
{
    public class ListOrder
    {
        public int TotalCount { get; set; }
        public List<OrderList> Orders { get; set; }
    }

    public class OrderList
    {
        public string Id { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
