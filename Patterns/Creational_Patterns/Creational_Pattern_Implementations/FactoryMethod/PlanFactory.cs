using FactoryMethod.Plans;

namespace FactoryMethod
{
    public class PlanFactory
    {
        public Plan? getPlan(string planType)
        {
            if (planType.ToUpper().Equals("DOMESTIC"))
            {
                return new DomesticPlan();
            }
            else if (planType.ToUpper().Equals("COMMERCIAL"))
            {
                return new CommercialPlan();
            }
            else if (planType.ToUpper().Equals("INSTITUTIONAL"))
            {
                return new InstitutionalPlan();
            }

            return null;
        }
    }
}
