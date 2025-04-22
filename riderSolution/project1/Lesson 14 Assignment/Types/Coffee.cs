using System.Text;
using project1.Lesson_14_Assignment.Enums;

namespace project1.Lesson_14_Assignment;

public abstract class Coffee
{
    public int BlackCoffee { get; set; }
    public int Sugar { get; set; }
    public Dictionary<MilkType, int> Milk { get; set; } = new Dictionary<MilkType, int>();

    public Coffee()
    {
        BlackCoffee = 0;
        Sugar = 0;
        foreach (MilkType milk in Enum.GetValues(typeof(MilkType)))
        {
            Milk[milk] = 0;
        }
    }

    public Coffee(int blackCoffee, int sugar, int regularMilk, int oatMilk, int soyMilk)
    {
        BlackCoffee = blackCoffee;
        Sugar = sugar;
        Milk[MilkType.Regular] = regularMilk;
        Milk[MilkType.Oat] = oatMilk;
        Milk[MilkType.Soy] = soyMilk;
    }

    public void AddSugar(int sugarAmount)
    {
        Sugar += sugarAmount;
    }

    public bool AddMilk(int milkAmount, string milkType = "regular")
    {
        milkType = milkType.ToLower();
        foreach (MilkType milk in Enum.GetValues(typeof(MilkType)))
        {
            if (milk.ToString().ToLower() == milkType)
            {
                Milk[milk] += milkAmount;
                return true;
            }
        }
        
        return false;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder($"\n- black coffee: {BlackCoffee}\n- sugar: {Sugar}\n");
        foreach (MilkType milk in Enum.GetValues(typeof(MilkType)))
        {
            sb.Append($"- {milk}: {Milk[milk]}\n");
        }
        
        return sb.ToString();
    }
}