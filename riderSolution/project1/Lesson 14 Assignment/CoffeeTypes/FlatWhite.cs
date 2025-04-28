namespace project1.Lesson_14_Assignment;

public class FlatWhite : Coffee
{
    public FlatWhite() : base(2, 0, 1, 0, 0)
    {
        
    }

    public override string ToString()
    {
        return "FlatWhite" + base.ToString();
    }
}