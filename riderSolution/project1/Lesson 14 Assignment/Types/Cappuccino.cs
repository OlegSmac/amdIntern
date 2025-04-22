namespace project1.Lesson_14_Assignment;

public class Cappuccino : Coffee
{
    public Cappuccino() : base(1, 0, 1, 0, 0)
    {
        
    }

    public override string ToString()
    {
        return "Cappuccino" + base.ToString();
    }
}