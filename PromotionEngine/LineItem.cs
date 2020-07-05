using System;
namespace PromotionEngine
{
    public class LineItem
    {
        public LineItem()
        {
        }

        public char ItemID { get; set; }
        public int Quantity { get; set; }
        public bool OfferApplied { get; set; }
    }
}
