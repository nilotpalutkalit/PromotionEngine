using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine
{
    public class NItemsOffer : IOffer
    {
        List<LineItem> ItemsOnOffer { get; set; } = new List<LineItem>();

        //Incase of NItems rule this list shall contain a single entry.
        //Each entry is a mandatory set of items for offer to complete
        OfferRule offerDefinition { get; set; } = new OfferRule();
        IItemDetailsProvider ItemDetailsProvider { get; set; }
        CompaignDetails CompaignDetails { get; set; } = new CompaignDetails();

        public NItemsOffer()
        {
        }

        public bool ApplyOffer(List<LineItem> lineItems)
        {
            bool offerApplied = false;
            foreach (var eachLineItem in lineItems)
            {
                if(eachLineItem.ItemID != offerDefinition.ItemIDToBuy || eachLineItem.OfferApplied)
                {
                    continue;
                }

                var quantityOnOffer = 0;
                var quantity = eachLineItem.Quantity;
                while(quantity >= offerDefinition.QuantityToBuy)
                {
                    quantityOnOffer += offerDefinition.QuantityToBuy;
                    quantity -= offerDefinition.QuantityToBuy;
                }

                eachLineItem.Quantity = quantityOnOffer;
                eachLineItem.OfferApplied = true;
                ItemsOnOffer.Add(eachLineItem);
                offerApplied = true;
            }

            return offerApplied;
        }

        public List<LineItem> GetPromotionAppliedItems()
        {


            return ItemsOnOffer;
        }

        public float GetTotalPriceAfterOffer()
        {
            float price = 0.0f;
            foreach (var items in ItemsOnOffer)
            {
                price += (items.Quantity % offerDefinition.QuantityToBuy) * CompaignDetails.Value;
            }

            return price;

        }



        public void RegisterProvider(IItemDetailsProvider provider)
        {
            ItemDetailsProvider = provider;
        }

        public void DefineOffer(List<OfferRule> offerRules, float value)
        {
            offerDefinition = offerRules.FirstOrDefault();
            CompaignDetails.Value = value;
            CompaignDetails.Type = CompaignType.FlatPrice;
        }
    }
}
