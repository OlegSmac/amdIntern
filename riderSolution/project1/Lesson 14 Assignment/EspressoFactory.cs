namespace project1.Lesson_14_Assignment;

public class EspressoFactory : CoffeeFactory
{
    public override Coffee CreateCoffee()
    {
        return new Espresso();
    }
}