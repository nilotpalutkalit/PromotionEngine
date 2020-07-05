using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    
    public interface IOffer
    {


        void DefineOffer(List<OfferRule> offerRules, float value);

        //pass line items in the order
        bool ApplyOffer(List<LineItem> lineItems);

        //returns line items where offer is applicable after applying offer
        List<LineItem> GetPromotionAppliedItems();

        //returns the price after applying offer
        float GetTotalPriceAfterOffer();

        //To fetch item price details etc.
        void RegisterProvider(IItemDetailsProvider provider);

    }
}
