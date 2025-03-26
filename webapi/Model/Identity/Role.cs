using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using webapi.Model.BaseEntities;

namespace webapi.Model.Identity
{
    public class Role : IdentityRole<int>, IAuditEntity, INotifyPropertyChanged
    {
        public DateTime CreationTime { get; set; } = DateTime.UtcNow.AddHours(1);
        public int? CreatorUserId { get; set; }

        [MaxLength(100)]
        public override string Name { get; set; }

        [MaxLength(100)]
        public override string NormalizedName { get; set; }

        [MaxLength(50)]
        public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public string? ChangedByUser { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IList<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}