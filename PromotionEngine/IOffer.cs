using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public interface IOffer
    {

        bool ApplyOffer(List<LineItem> lineItems);
        List<LineItem> GetPromotionAppliedItems();
        bool GetTotalPriceAfterOffer();

    }
}
