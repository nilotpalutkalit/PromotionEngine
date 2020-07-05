using System;
namespace PromotionEngine
{
    public enum CompaignType
    {
        FlatPrice,
        Percent
    }

    public class CompaignDetails
    {
        public CompaignDetails()
        {
        }

        //value is the discount percentage or the offer price of the items
        public float Value { get; set; }

        //offer can be discount offer or a flat price offer
        CompaignType Type { get; set; }

    }

}
