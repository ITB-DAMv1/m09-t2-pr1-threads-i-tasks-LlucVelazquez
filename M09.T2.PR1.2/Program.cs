using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class AsteroidsGame
{
    static readonly object lockObj = new object();
    static int shipPos;
    static List<(int x, int y)> asteroids = new List<(int, int)>();
    static bool gameOver = false;
    static int score = 0;

    static async Task Main()
    {
        Console.SetWindowSize(50, 20);
        Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

        Console.CursorVisible = false;
        shipPos = Console.WindowWidth / 2;
        ForçaMidaConsola();
        var inputTask = Task.Run(() => ReadInput());
        var updateTask = Task.Run(() => UpdatePositions());
        var renderTask = Task.Run(() => RenderLoop());
        

        await Task.WhenAny(updateTask);

        // Mostrar dades finals
        Console.Clear();
        Console.WriteLine("Joc acabat!");
        Console.WriteLine($"Puntuació: {score}");
        Console.WriteLine("Prem una tecla per sortir...");
        Console.ReadKey();
    }

    static void ReadInput()
    {
        while (!gameOver)
        {
            var key = Console.ReadKey(true).Key;
            lock (lockObj)
            {
                int width = Console.WindowWidth;
                if (key == ConsoleKey.A && shipPos > 0) shipPos--;
                else if (key == ConsoleKey.D && shipPos < width - 1) shipPos++;
            }
        }
    }
     
    static async Task UpdatePositions()
    {
        var rand = new Random();
        while (!gameOver)
        {
            lock (lockObj)
            {
                int width = Console.WindowWidth;
                int height = Console.WindowHeight;

                if (rand.NextDouble() < 0.1)
                    asteroids.Add((rand.Next(width), 0));
                for (int i = 0; i < asteroids.Count; i++)
                    asteroids[i] = (asteroids[i].x, asteroids[i].y + 1);
                asteroids.RemoveAll(a => a.y >= height);

                foreach (var a in asteroids)
                {
                    if (a.y == height - 1 && a.x == shipPos)
                    {
                        gameOver = true;
                        break;
                    }
                }

                if (!gameOver)
                    score++;
            }
            await Task.Delay(20);
        }
    }
    const int amplada = 50;
    const int alçada = 20;

    static void ForçaMidaConsola()
    {
        if (Console.WindowWidth != amplada || Console.WindowHeight != alçada)
            Console.SetWindowSize(amplada, alçada);
        if (Console.BufferWidth != amplada || Console.BufferHeight != alçada)
            Console.SetBufferSize(amplada, alçada);
    }

    static async Task RenderLoop()
    {
        while (!gameOver)
        {
            lock (lockObj)
            {
                int width = Console.WindowWidth;
                int height = Console.WindowHeight;

                Console.Clear();

                if (height - 1 >= 0 && shipPos >= 0 && shipPos < width)
                {
                    Console.SetCursorPosition(shipPos, height - 1);
                    Console.Write('^');
                }

                // Dibuixar asteroides
                foreach (var a in asteroids)
                {
                    if (a.y >= 0 && a.y < height && a.x >= 0 && a.x < width)
                    {
                        Console.SetCursorPosition(a.x, a.y);
                        Console.Write('*');
                    }
                }
                Console.SetCursorPosition(0, 0);
            }
            await Task.Delay(50);
        }
    }
}
