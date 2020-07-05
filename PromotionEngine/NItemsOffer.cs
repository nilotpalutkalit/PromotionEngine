using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine
{
    public class NItemsOffer : IOffer
    {
        List<LineItem> ItemsOnOffer { get; set; }

        //Incase of NItems rule this list shall contain a single entry.
        //Each entry is a mandatory set of items for offer to complete
        OfferRule offerDefinition { get; set; }

        public NItemsOffer()
        {
        }

        public bool ApplyOffer(List<LineItem> lineItems)
        {
            bool offerApplied = false;
            foreach (var eachLineItem in lineItems)
            {
                if(eachLineItem.ItemID != offerDefinition.ItemIDToBuy)
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
                ItemsOnOffer.Add(eachLineItem);
                offerApplied = true;
            }

            return offerApplied;
        }

        public List<LineItem> GetPromotionAppliedItems()
        {
            throw new NotImplementedException();
        }

        public float GetTotalPriceAfterOffer()
        {
            throw new NotImplementedException();
        }

        public void RegisterCompaign(IItemDetailsProvider provider)
        {
            throw new NotImplementedException();
        }

        public void DefineOffer(List<OfferRule> offerRules)
        {
            offerDefinition = offerRules.FirstOrDefault();
        }
    }
}
