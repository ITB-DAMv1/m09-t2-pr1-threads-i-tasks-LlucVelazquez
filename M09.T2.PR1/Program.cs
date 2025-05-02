using System.Drawing;

namespace M09.T2.PR1
{
    public class Program
    {
        const int numComensal = 5;
        const int maxTimeHungry = 15000;
        const int SimulationTime = 30000;
        public static readonly ConsoleColor[] COLORS = { ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Green, 
            ConsoleColor.Yellow, ConsoleColor.Cyan };
        public static readonly object[] palets = new object[numComensal];
        public static readonly ComensalData[] comensalsData = new ComensalData[numComensal];
        public static object statsLock = new();
        public static bool endSimulation = false;
        public class ComensalData
        {
            public int Id { get; set; }
            public int TimesEat { get; set; }
            public double MaxTimeHungry { get; set; }
            public DateTime LastEat { get; set; } = DateTime.Now;
        }
        public static void Main(string[] args)
        {
            for (int i = 0; i < numComensal; i++)
            {
                palets[i] = new object();
                comensalsData[i] = new ComensalData { Id = i };
            }
            List<Thread> threads = new();
            for (int i = 0; i < numComensal; i++)
            {
                int id = i;
                Thread t = new Thread(() => Comensal(id));
                threads.Add(t);
                t.Start();
                
            }
            Thread monitor = new Thread(Monitor);
            monitor.Start();

            Thread.Sleep(SimulationTime);
            endSimulation = true;
            
            foreach (var t in threads) t.Join();
            monitor.Join();
        }
        public static void Comensal(int id)
        {
            Random random = new();
            ConsoleColor color = COLORS[id];

            while (!endSimulation)
            {
                Log("esta pensant", color, id);
                Thread.Sleep(random.Next(500,2000));

                int palet1 = id;
                int palet2 = (id + 1) % numComensal;
                if (palet2 < palet1) (palet1, palet2) = (palet2, palet1);
                lock (statsLock)
                {
                    comensalsData[id].LastEat = DateTime.Now;
                }
                lock (palets[palet1])
                {
                    Log($"agafat el palet {palet1}", color, id);
                    lock (palets[palet2])
                    {
                        Log($"agafat el palet {palet2}", color, id);
                        Log("esta menjant", color, id);
                        Thread.Sleep(random.Next(500, 1000));
                        Log("ha acabat de menjar", color, id);
                    }
                }
            }
        }
        public static void Monitor()
        {
            while (!endSimulation)
            {
                lock (statsLock)
                {
                    foreach(var c in comensalsData)
                    {
                        double timeHungry = (DateTime.Now - c.LastEat).TotalMilliseconds;
                        if (timeHungry > maxTimeHungry)
                        {
                            Console.WriteLine($"Comensal {c.Id} ha estat massa temps sense menjar ({timeHungry}ms)");
                            endSimulation = true;
                            return;
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }
        public static void Log(string action, ConsoleColor color, int id)
        {
            Console.ResetColor();
            Console.Write($"[{DateTime.Now:HH:mm:ss}] ");

            Console.BackgroundColor = action switch
            {
                "esta pensant" => ConsoleColor.White,
                "esta menjant" => ConsoleColor.Black,
                "agafat el palet" => ConsoleColor.Gray,
                "ha acabat de menjar" => ConsoleColor.Magenta,
                _ => ConsoleColor.White
            };
            Console.ForegroundColor = color;
            Console.Write($"Comensal {id} {action}");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
