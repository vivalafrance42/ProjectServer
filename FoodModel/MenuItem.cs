using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodModel;

[Table("MenuItem")]
public partial class MenuItem
{
    [Key]
    public int MenuItemId { get; set; }

    public int FoodPlaceId { get; set; }

    
    public string ItemName { get; set; } = null!;

    public int ItemsPurchased { get; set; }

    [ForeignKey("FoodPlaceId")]
    [InverseProperty("MenuItems")]
    public virtual FoodPlace FoodPlace { get; set; } = null!;
}
