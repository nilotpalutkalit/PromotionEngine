using System;
namespace PromotionEngine
{
    public interface ICompaignProvider
    {
        //returns the compaign id after registering the compaign
        public int RegisterCompaign(CompaignDetails compaign);
    }
}
