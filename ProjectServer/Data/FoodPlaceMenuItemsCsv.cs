using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectServer.Data
{
    public class FoodPlaceMenuItemsCsv
    {

        public string MenuItem { get; set; } = null!;
        //public string MenuItem_ascii { get; set; } = null!;
        public string FoodPlace { get; set; } = null!;
        public decimal? ItemsPurchased { get; set; }

        [Column("Price", TypeName = "numeric(18, 2)")]
        public decimal Price { get; set; }

        public string PlaceInitials2 { get; set; } = null!;

        public long Id { get; set; }
    }
}
