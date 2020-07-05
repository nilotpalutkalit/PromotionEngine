using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class PromotionEngineBL : IItemDetailsProvider
    {
        public Dictionary<int, CompaignDetails> compaignDetails;
        public Dictionary<int, IOffer> availableOffers;
        public Dictionary<char, SaleLineItem> orders;
        public Dictionary<char, ItemDetail> ItemDetails;
        //offer id and price
        private Dictionary<int, float> PriceForItemsOnOffer;
        
        public PromotionEngineBL()
        {
        }

        public void RegisterOffer(IOffer offer)
        {
            var id = availableOffers.Count + 1;
            availableOffers.Add(id, offer);
        }

        public int RegisterCompaign(CompaignDetails compaign)
        {
            var id = compaignDetails.Count + 1;
            compaignDetails.Add(id, compaign);
            return id;
        }

        public float GetCheckoutPrice(List<LineItem> order)
        {
            foreach(var eachOrder in order )
            {
       
                orders.Add(eachOrder.ItemID, new SaleLineItem(eachOrder));
            }

            foreach(var offer in availableOffers)
            {
                var offerApplied = offer.Value.ApplyOffer(order);
                if(offerApplied)
                {
                    //Gets item and quantity of items on which this is offer is applicable
                    var offerAppliedItems = offer.Value.GetPromotionAppliedItems();
                    var offerPrice = offer.Value.GetTotalPriceAfterOffer();
                    PriceForItemsOnOffer.Add(offer.Key, offerPrice);
                    foreach(var eachOfferItems in offerAppliedItems)
                    {
                        orders[eachOfferItems.ItemID].OfferAppliedQuantity = eachOfferItems.Quantity;
                    }
                }
            }

            float totalPrice = 0.0f;
            foreach(var eachOrder in orders)
            {
                totalPrice += ItemDetails[eachOrder.Key].price * eachOrder.Value.Quantity - eachOrder.Value.OfferAppliedQuantity;
            }

            foreach(var promotionItemPrice in PriceForItemsOnOffer)
            {
                totalPrice += promotionItemPrice.Value;
            }

            return totalPrice;
        }

        public float GetItemPrice(char ID)
        {
            return ItemDetails[ID].price;
        }
    }
}
