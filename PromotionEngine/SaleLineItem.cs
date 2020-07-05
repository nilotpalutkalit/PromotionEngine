using System;
namespace PromotionEngine
{
    public class SaleLineItem : LineItem
    {
        //This is the quantity on which offer  is applied already.
        public int OfferAppliedQuantity { get; set; } = 0;
        public SaleLineItem(LineItem lineItem)
        {
            this.ItemID = lineItem.ItemID;
            this.Quantity = lineItem.Quantity;
        }
    }
}
