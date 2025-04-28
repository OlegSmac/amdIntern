using project1.Lesson_14_Assignment.Enums;

namespace project1.Lesson_14_Assignment;

public class CoffeeFactory
{
    public Coffee CreateCoffee(CoffeeType coffeeType)
    {
        if (coffeeType == CoffeeType.Cappuccino) return new Cappuccino();
        if (coffeeType == CoffeeType.Espresso) return new Espresso();
        if (coffeeType == CoffeeType.FlatWhite) return new FlatWhite();
        
        return null;
    }
}