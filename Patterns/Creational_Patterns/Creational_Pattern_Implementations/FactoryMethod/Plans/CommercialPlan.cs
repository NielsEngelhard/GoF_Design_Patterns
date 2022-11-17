using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Plans
{
    public class CommercialPlan : Plan
    {
        public override void GetRate()
        {
            Rate = 7.50;
        }
    }
}
