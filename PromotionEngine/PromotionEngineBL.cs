using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class PromotionEngineBL : ICompaignProvider
    {
        public Dictionary<int, CompaignDetails> compaignDetails;
        public Dictionary<int, IOffer> availableOffers;
        
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
    }
}
