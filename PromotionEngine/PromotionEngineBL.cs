using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class PromotionEngineBL : IItemDetailsProvider
    {
        //public Dictionary<int, CompaignDetails> compaignDetails;
        public Dictionary<int, IOffer> availableOffers = new Dictionary<int, IOffer>();
        public Dictionary<char, SaleLineItem> orders = new Dictionary<char, SaleLineItem>();
        public Dictionary<char, ItemDetail> ItemDetails = new Dictionary<char, ItemDetail>();
        //offer id and price
        private Dictionary<int, float> PriceForItemsOnOffer = new Dictionary<int, float>();
        
        public PromotionEngineBL()
        {
            RegisterAllOffer();
            RegisterItemDetails();
        }

        void RegisterAllOffer()
        {
            NItemsOffer offer1 = new NItemsOffer();
            offer1.DefineOffer(new List<OfferRule> { new OfferRule('A', 3) }, 130.0f);
            offer1.RegisterProvider(this);

            NItemsOffer offer2 = new NItemsOffer();
            offer2.DefineOffer(new List<OfferRule> { new OfferRule('B', 2) }, 45.0f);
            offer2.RegisterProvider(this);

            CombinationOffer offer3 = new CombinationOffer();
            offer3.DefineOffer(new List<OfferRule> {
                                                        new OfferRule('C', 1),
                                                        new OfferRule('D', 1)
                                                    }, 30.0f);
            offer3.RegisterProvider(this);

            RegisterOffer(offer1);
            RegisterOffer(offer2);
            RegisterOffer(offer3);

        }

        void RegisterItemDetails()
        {
            ItemDetails.Add('A', new ItemDetail('A', 50.0f));
            ItemDetails.Add('B', new ItemDetail('B', 30.0f));
            ItemDetails.Add('C', new ItemDetail('C', 20.0f));
            ItemDetails.Add('D', new ItemDetail('D', 15.0f));
        }

        public void RegisterOffer(IOffer offer)
        {
            var id = availableOffers.Count + 1;
            availableOffers.Add(id, offer);
        }

        //public int RegisterCompaign(CompaignDetails compaign)
        //{
        //    var id = compaignDetails.Count + 1;
        //    compaignDetails.Add(id, compaign);
        //    return id;
        //}

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
                totalPrice += ItemDetails[eachOrder.Key].Price * (eachOrder.Value.Quantity - eachOrder.Value.OfferAppliedQuantity);
            }

            foreach(var promotionItemPrice in PriceForItemsOnOffer)
            {
                totalPrice += promotionItemPrice.Value;
            }

            orders.Clear();
            PriceForItemsOnOffer.Clear();

            return totalPrice;
        }

        public float GetItemPrice(char ID)
        {
            return ItemDetails[ID].Price;
        }
    }
}
