namespace FactoryMethod
{
    public abstract class Plan
    {
        protected double Rate;
        public abstract void GetRate();

        public double CalculateBill(int units)
        {
            return (units * Rate);
        }
    }
}
