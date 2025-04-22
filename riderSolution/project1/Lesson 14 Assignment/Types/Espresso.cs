using System.Text;
using project1.Lesson_14_Assignment.Enums;

namespace project1.Lesson_14_Assignment;

public class Espresso : Coffee
{
    public Espresso() : base(1, 0, 0, 0, 0)
    {
        
    }

    public override string ToString()
    {
        return "Espresso" + base.ToString();
    }
}