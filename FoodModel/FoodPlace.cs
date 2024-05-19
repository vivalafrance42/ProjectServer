using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodModel;

[Table("FoodPlace")]
public partial class FoodPlace
{
    [Key]
    public int FoodPlaceId { get; set; }

    
    [Unicode(false)]
    public string PlaceName { get; set; } = null!;

    [Column("PlaceInitials2")]
    [StringLength(2)]
    [Unicode(false)]
    public string PlaceInitials2 { get; set; } = null!;

    [InverseProperty("FoodPlace")]
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}
