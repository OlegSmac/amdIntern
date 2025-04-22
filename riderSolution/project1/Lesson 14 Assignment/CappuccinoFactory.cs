namespace project1.Lesson_14_Assignment;

public class CappuccinoFactory : CoffeeFactory
{
    public override Coffee CreateCoffee()
    {
        return new Cappuccino();
    }
}