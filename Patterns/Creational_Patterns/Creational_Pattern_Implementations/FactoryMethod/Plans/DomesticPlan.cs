namespace FactoryMethod.Plans
{
    public class DomesticPlan : Plan
    {
        public override void GetRate()
        {
            Rate = 3.50;
        }
    }
}
