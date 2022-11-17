namespace FactoryMethod.Plans
{
    public class InstitutionalPlan : Plan
    {
        public override void GetRate()
        {
            Rate = 5.50;
        }
    }
}
