﻿using System.ComponentModel.DataAnnotations;
using webapi.Enum;
using webapi.Model.BaseEntities;
using webapi.Model.Lookup;

namespace webapi.Model.Products
{
    public class Brand : NamedSortableEnabledStateModel<Brand>, INamedSortableEnabledStateModel, IDescribedModel, ILookupModel
    {
        [Required]
        [MaxLength(LimitLength.FullName)]
        public override string Name { get; set; } = string.Empty;

        [MaxLength(LimitLength.ShortDescription)]
        public string? Description { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}