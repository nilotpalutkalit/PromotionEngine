using System;
namespace PromotionEngine
{
    public class ItemDetail
    {
        public ItemDetail(char itemID, float price)
        {
            ItemID = itemID;
            Price = price;
        }

        public char ItemID { get; set; }
        public float Price { get; set; }
    }
}
