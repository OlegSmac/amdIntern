﻿using System.Reflection.Emit;
using project1.Lesson_10_Assignment;
using project1.Lesson_5_Assignment;

namespace project1;

public class Program
{
    static async Task Main()
    {
        try
        {
            await VehicleMain.MainFunction();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
        //Lesson5Main.MainFunction();
        //Lesson10Main.MainFunction();
    }
}
