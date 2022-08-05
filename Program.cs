using System;

namespace Interview_Refactor1
{
    class Program
    {
        static void Main(string[] args)
        {
            // want to maximize the number of apple pies we can make.
            // it takes 3 apples, 2 lbs of sugar and 1 pound of flour to make 1 apple pie
            // this is intended to run on .NET Core

            do
            {
                int cntApples;
                int lbsSugar;
                int lbsFlour;

                Console.WriteLine("How many apples do you have?");
                if(!int.TryParse(Console.ReadLine(), out cntApples))
                {
                    Console.WriteLine("Please enter a number, let's try again from the top!");
                    continue;
                }

                Console.WriteLine("How much sugar do you have?");
                if(!int.TryParse(Console.ReadLine(), out lbsSugar))
                {
                    Console.WriteLine("Please enter a number, let's try again from the top!");
                    continue;
                }

                Console.WriteLine("How many pounds of flour do you have?");
                if(!int.TryParse(Console.ReadLine(), out lbsFlour))
                {
                    Console.WriteLine("Please enter a number, let's try again from the top!");
                    continue;
                }

                Console.WriteLine("You can make:");
                utility.Calc(cntApples, lbsSugar, lbsFlour);

                Console.WriteLine("\n\nEnter to calculate again, or 'q' to quit!");
            } while (!string.Equals(Console.ReadLine(), "Q", StringComparison.CurrentCultureIgnoreCase));

        }
    }

    public static class utility
    {
        public static void Calc(int cntApples, int lbsSugar, int lbsFlour)
        {
            try
            {
                int maxFromApples = (cntApples / 3);
                int maxFromSugar = lbsSugar / 2;
                int maxFromFlour =  lbsFlour;
                var maxPies = Math.Min(Math.Min(maxFromApples, maxFromSugar), maxFromFlour);
               
                Console.WriteLine(maxPies + " apple pies!");

                var leftOverApples = cntApples - (maxPies * 3);
                var leftOverSugar = lbsSugar - (maxPies * 2);
                var leftOverFlour = lbsFlour - maxPies;

                Console.WriteLine($"You will have: {leftOverApples} apple(s) left over, {leftOverSugar} lbs sugar left over, and {leftOverFlour} lbs flour left over.");
            }
            catch (Exception e)
            {
                Console.WriteLine("error");
            }

        }
    }
}
