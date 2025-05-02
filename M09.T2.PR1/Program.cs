using System.Drawing;

namespace M09.T2.PR1
{
    public class Program
    {
        const int numComensal = 5;
        const int maxTimeHungry = 15000;
        const int SimulationTime = 30000;
        static readonly ConsoleColor[] COLORS = { ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Green, 
            ConsoleColor.Yellow, ConsoleColor.Cyan };
        public static bool endSimulation = false;
        public static void Main(string[] args)
        {
            List<Thread> threads = new();
            for (int i = 0; i < numComensal; i++)
            {
                int id = i;
                Thread t = new Thread(() => Comensal(id));
                threads.Add(t);
                t.Start();
                
            }

            Thread.Sleep(SimulationTime);
            endSimulation = true;
            
            foreach (var t in threads) t.Join();
        }
        public static void Comensal(int id)
        {
            Random random = new();
            //ConsoleColor color = COLORS[id];

            while (!endSimulation)
            {
                //Console.ForegroundColor = color;
                Console.WriteLine($"Comensal {id} esta pensant.");
                Thread.Sleep(random.Next(500,2000));
                Console.WriteLine($"Thread {id} started.");
                Thread.Sleep(1000); // Simulate work
                Console.WriteLine($"Thread {id} finished.");
            }
        }
    }
}
