using System;

namespace Events
{

    class Program
    {
        static void Main(string[] args)
        {
            Adder a = new Adder();
            var lover = new MultiplesOfFiveLover();



            //a.OnMultipleOfFiveReached += new Adder.DgEventRaiser(lover.MultipleOfFiveReached);
            a.OnMultipleOfFiveReached += lover.MultipleOfFiveReached;
            a.OnMultipleOfFiveReached += Console.WriteLine;

            var answer = a.Add(4, 3);
            Console.WriteLine("Answer = {0}", answer);

            answer = a.Add(4, 6);
            Console.WriteLine("Answer = {0}", answer);

            a.OnMultipleOfFiveReached -= lover.MultipleOfFiveReached;

            Console.WriteLine("Answer = {0}", a.Add(3,2));

            Console.ReadKey();
        }
    }

    public class Adder
    {
        public delegate void DgEventRaiser(int a = 11);
        //public event DgEventRaiser OnMultipleOfFiveReached;
        public event Action<int> OnMultipleOfFiveReached; //does it work without 'event' keyword? why do we use it?

        public int Add(int x, int y)
        {
            var sum = x + y;
            if (sum % 5 == 0)
            {
                OnMultipleOfFiveReached?.Invoke(sum);
            }
            /*
            obj?.method();

            is same as:

            if (obj != null)
            {
                obj.method();
            }
            */
            return sum;
        }
    }

    public class MultiplesOfFiveLover
    {
        public void MultipleOfFiveReached(int a)
        {
            Console.WriteLine($"Multiple of five reached! ({a})");
        }
    }
}
