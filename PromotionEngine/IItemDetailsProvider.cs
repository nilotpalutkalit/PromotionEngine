using System;
namespace PromotionEngine
{
    public interface IItemDetailsProvider
    {
        //returns the compaign id after registering the compaign
        public int RegisterCompaign(CompaignDetails compaign);
        public float GetItemPrice(char ID);
    }
}
