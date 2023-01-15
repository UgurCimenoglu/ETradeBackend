using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        //virtual ile işaretlediğimiz prop ları override ile ezebiliyoruz, bu işlemi şunun için yaptık:
        //Bütün entity'lerimize kalıtım ile updatedDate prop'u geliyor fakat biz bazı entitylerimizde olmasını istemiyoruz
        //bunun için UpdateDate prop'unu virtual olarak ayarlayıp kalıtım verdiğimiz yerde override edip üzerine [NotMapped] attibute
        //koyarsak migration bastıgımız zaman veritabanında ilgili tabloda UpdatedDate kolonu oluşmayacak, örneği File adlı entity'de var.
        virtual public DateTime UpdatedDate { get; set; }
    }
}
