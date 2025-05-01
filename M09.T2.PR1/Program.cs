using System.Drawing;

namespace M09.T2.PR1
{
    public class Program
    {
        static readonly ConsoleColor[] COLORS = { ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Green, 
            ConsoleColor.Yellow, ConsoleColor.Cyan };
        public static void Main(string[] args)
        {
            List<Thread> threads = new();
            for (int i = 0; i < 5; i++)
            {
                int id = i;
                Thread t = new Thread(() => Comensal(id));
                threads.Add(t);
                t.Start();
            }
        }
        public static void Comensal(int id)
        {
            ConsoleColor color = COLORS[id];
            Console.ForegroundColor = color;
            Console.WriteLine($"Thread {id} started.");
            Thread.Sleep(1000); // Simulate work
            Console.WriteLine($"Thread {id} finished.");
            Console.ResetColor();
        }
    }
}
