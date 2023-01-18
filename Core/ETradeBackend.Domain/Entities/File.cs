using ETradeBackend.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETradeBackend.Domain.Entities
{
    public class File : BaseEntity
    {
        [NotMapped]  //NotMapped attribute migration bastiğimizda veritabanı tablosuna işaretlenmiş prop'u eklemeyecektir.Bu prop un virtual olup override edilmesi gerekmektedir. BaseEntity'e bak ve açıklamasını oku.
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }

        public string FileName { get; set; }
        public string Path { get; set; }
        public string StorageType { get; set; }
    }
}
