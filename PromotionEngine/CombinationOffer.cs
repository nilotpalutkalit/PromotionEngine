using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class CombinationOffer : IOffer
    {
        List<OfferRule> offerDefinition { get; set; } = new List<OfferRule>();
        List<LineItem> ItemsOnOffer { get; set; } = new List<LineItem>();
        Dictionary<char, LineItem> order { get; set; } = new Dictionary<char, LineItem>();
        IItemDetailsProvider ItemDetailsProvider { get; set; }
        CompaignDetails CompaignDetails { get; set; } = new CompaignDetails();
        int MultiplicationFactor { get; set;} = 0;

        public CombinationOffer()
        {
        }

        public bool ApplyOffer(List<LineItem> lineItems)
        {


            foreach(var eachOrder in lineItems)
            {
                order.Add(eachOrder.ItemID, eachOrder);
            }

            var offerMultiplicationFactor = 0;
            foreach(var itemsRequired in offerDefinition)
            {
                if(!order.ContainsKey(itemsRequired.ItemIDToBuy))
                {
                    ItemsOnOffer.Clear();
                    return false;
                }

                if(order[itemsRequired.ItemIDToBuy].OfferApplied)
                {

                    ItemsOnOffer.Clear();
                    return false;
                }

                int mf = order[itemsRequired.ItemIDToBuy].Quantity / itemsRequired.QuantityToBuy;
                if(mf < 1)
                {
                    ItemsOnOffer.Clear();
                    return false;
                }

                if(offerMultiplicationFactor == 0)
                {
                    offerMultiplicationFactor = mf;
                }

                else if(mf < offerMultiplicationFactor)
                {
                    offerMultiplicationFactor = mf;
                }
            }

            foreach(var itemsRequired in offerDefinition)
            {
                LineItem offerItem = order[itemsRequired.ItemIDToBuy];
                offerItem.Quantity = offerMultiplicationFactor * itemsRequired.QuantityToBuy;
                ItemsOnOffer.Add(offerItem);
            }

            MultiplicationFactor = offerMultiplicationFactor;

            return ItemsOnOffer.Count > 0;
            
        }

        public void DefineOffer(List<OfferRule> offerRules, float offerValue)
        {
            offerDefinition = offerRules;
            CompaignDetails.Value = offerValue;
            CompaignDetails.Type = CompaignType.FlatPrice;
        }

        public List<LineItem> GetPromotionAppliedItems()
        {
            foreach(var eachItem in ItemsOnOffer)
            {

                eachItem.OfferApplied = true;
            }

            return ItemsOnOffer;
        }

        public float GetTotalPriceAfterOffer()
        {





            float price = MultiplicationFactor * CompaignDetails.Value;

            return price;

        }



        public void RegisterProvider(IItemDetailsProvider provider)
        {
            ItemDetailsProvider = provider;
        }
    }
}
