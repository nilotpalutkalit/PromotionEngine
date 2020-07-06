using System;
namespace PromotionEngine
{
    public class OfferRule
    {
        public OfferRule(char ItemId, int quantity)
        {
            QuantityToBuy = quantity;
            ItemIDToBuy = ItemId;
        }

        public int QuantityToBuy { get; set; }
        public char ItemIDToBuy { get; set; }
    }
}
