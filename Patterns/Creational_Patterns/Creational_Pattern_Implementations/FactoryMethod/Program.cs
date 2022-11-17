// https://www.javatpoint.com/factory-method-design-pattern#:~:text=prev%20next%20%E2%86%92-,Factory%20Method%20Pattern,the%20instance%20of%20the%20class.

using FactoryMethod;

PlanFactory planFactory = new PlanFactory();

Console.WriteLine("Enter the name of plan for which the bill will be generated: ");
string userInputPlanName = Console.ReadLine();

Console.WriteLine("Enter the number of units for bill will be calculated: ");
string userInputNUnits = Console.ReadLine();
int n_units = int.Parse(userInputNUnits);

Plan p = planFactory.getPlan(userInputPlanName);

p.GetRate();
var bill = p.CalculateBill(n_units);

Console.WriteLine($"Bill amount for {userInputPlanName} of {n_units} units is: {bill}");