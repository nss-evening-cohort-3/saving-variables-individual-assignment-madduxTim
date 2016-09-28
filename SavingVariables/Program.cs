using System;

namespace SavingVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            InputHandler inputHandler = new InputHandler();
            Console.WriteLine("Let's get to savin' some variables!");
            while (inputHandler.@switch)
            {
                Console.Write(">> ");
                string input = Console.ReadLine().ToLower();
                inputHandler.InputSwitch(input);
            }
        }
    }
}
