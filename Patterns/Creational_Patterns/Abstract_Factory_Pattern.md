# Abstract Factory
https://codeburst.io/design-patterns-learning-abstract-factory-method-through-real-life-examples-9d0cc99ef0e8
## The problem
You have to design a generic vending machine which can provide you different variants of Coffee, Hot Water, Hot Milk, Coke, Lemonade & what not. How will you design that vending machine software?

This example is not about designing the complete vending machine, rather it discusses only one portion of the machine — how to support different variants of beverages, customers will browse all product & choose any one as per their choice. Remember, going forward you may be asked to add support for say fruit juice, tea or some variety of milk shake, chocolate shake etc. So the design should be extensible.

## What you can do
Probably you would start with defining some coffee type enums & creating coffee objects in a if-else or switch statements or wrap this whole object creation logic in a factory, if you don’t know what a factory pattern is, please look at this post & understand that first.

Code:
```
public enum CoffeeType {
  ESPRESSO,
  BLACK_COFFEE,
  LATTE,
  CAPPUCCINO
}

public Coffee getCoffee(CoffeeType type) {
  switch(type) {
    case ESPRESSO:
       return new Espresso();
    case LATTE:
      return new Latte();
    case BLACK_COFFEE:
      return new BlackCoffee();
    case CAPPUCCINO:
      return new Cappuccino();
    default:
      throw new SomeException("Coffee type not supported");
  }
}
```

The above approach works. Cool! But notice one thing — even if you are supporting Coffee only, different variants of coffee may have different ingredients. Example: Black Coffee might not require sugar, it only requires coffee powder, water or milk. Similarly other variants of coffee might differ in terms of percentage of ingredients like say the same quantity of Cappuccino may need more coffee powder than the same quantity of Latte. How will you support those scenarios in your code?

## The real problem(s)
In the above code snippet, the caller of the above getCoffee() method may set different quantity of milk, sugar, water etc after getting an instance of the coffee. But is it right to do so? Should the caller care about instantiation logic of the coffee objects? Instead, inside the switch-case block itself, you can set those parameters while creating an instance. Ok! it’s a little better approach. But the code quickly becomes cumbersome if I ask you to add support for more beverages. Example — add support for coke, coke can be mixed with water or soda, but not with milk or coffee powder. So coke has completely different set of ingredients. And your switch case block has to be modified again. What if I say that a customer can customize the beverage — s/he might choose whether s/he needs sugar or not — how much quantity of sugar, whether s/he needs mix up of water & milk in the coffee or not, or whether s/he needs soda in coke or not. How will you support that?

At this point in time, you are probably considering to create an object of type Customization which contains different fields like — milk, water, soda, sugar etc and pass it to your factory or switch-case block. So now your coffee creation code looks something like below:

See the above code block. It does multiple things — it knows how to create objects & how to set their respective parameters. Basically you have put all the object creation logic related code in this factory & it does more than one thing. Moreover, if you need to add support for say tea, the factory has to know how to create tea & how to set its ingredients as well.

This is just a plain violation of Single Responsibility Principle. And the code is not maintainable either. More beverages you add, more code you add or modify in the above code segment.

When you have a hundred beverages, you will have a switch statement with 100 options and each option has its unique configuration set inside the switch statement itself...

## Theory of the solution
How do you write your code in such a way that your changes are always minimum. Change is constant & inevitable, you have to always change something in your code, but how to minimize that change?

In our scenario, we are supporting actually different products but all are broadly defined as beverage products at least for the customer. It should be easy to change formula of any of the beverages. So we are dealing with runtime polymorphism for sure but at the same time the instantiation logic or parameter setting logic are completely different from each other. We are broadly dealing with beverage products, but their nature & ingredients are fundamentally different. Under beverage umbrella, they belong to different kind of categories like Black Coffee belongs to Coffee category, CocaCola belongs to Coke category, Orange / Mango juice belongs to juice category etc. Such different category objects creation under the same umbrella is supported by Abstract Factory Method.

Under the umbrella "Beverage".

__Typically, when the underlying implementations or mechanisms of object creation vary a lot but those implementations can be placed under the same top level umbrella & you need same public API to access them, there is a good chance that you need Abstract Factory Method design pattern.__


Let’s remodel the vending machine as shown in the image below. Let’s see what this modelling offers to us:
<<TODO:: add image>>

We have a Product interface which is the super type of the end product that we are expecting from the system.

Coffee products Cappuccino, BlackCoffee, juice product like Lemonade, Coke product like CocaCola, other products like HotMilk — all implements the Product interface because as per the assumption of our system, we are creating different products with the same interface for the end user.

All of the mentioned products implement make() method which will be invoked by the caller to finally prepare the product.

Each of the objects additionally stores what preparation & customization parameters were passed while creating those objects in prep & cust instance members respectively. Storing these parameters might not be required at all, depends on your use case.

Notice how the setter methods of all the product vary: Cappuccino & BlackCoffee have setMilk() method while Lemonade & CocaCola don’t have them. Similarly CocaCola has setCoke() while others don’t have that. These methods are very product specific method which are needed for setting the right amount of ingredients.

__Each of the products are instantiated inside its own factory. Example: Lemonade is instantiated by LemonadeFactory. Only the factory knows how to instantiate the product, what quantity of ingredients to set, this abstracts out the product creation & parameter setting logic from the caller — caller does not need to care about how to create products, caller delegates the task of object creation to the factory & the factory does all the hard work. What ingredients to use, how to fetch the ingredients formula, how to calculate their right quantity — all the things are taken care of by the factory. The advantage of the approach is:__

- As mentioned caller does not care how the products are created.
- The instantiation logic is concentrated in the factory, so if you need to remove some existing implementation, remove the product & the factory.
- If you need to add a new product first check if any of the existing product & factory can be used to support that new product without much change, otherwise add new product & factory — no change is required in much of the existing code, only probably you need to add a switch case in the ProductFactory in order to retrieve the new factory. If you don’t want to make this change as well, you can dynamically register classes in a factory (other topic).
- Since all the products implement Product interface, even if new product is added, the caller does not need to change anything at its side to support it.

Note: It’s not mandatory to create a factory for each product, if you have multiple products which can be created with same setter methods (same preparation materials), which only vary in terms of quantity of ingredients, they can be created through a single factory.

ProductFactory is an abstract class that defines the contract all factories which are responsible for product creation should implement. Also there is a static method getProductFactory() which facilitates the discovery of factories to the client.

Customization & Preparation are sample generic objects showing what can be the possible customization parameters & overall ingredients in the system across products.




## The solution
