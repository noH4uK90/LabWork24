namespace LabWork24;

internal static class Program
{
    private static string _commonVar = string.Empty;
    
    public static void Main(string[] args)
    {
        //Task1();
        //Task2();
        //Task3();
        Task4();
    }

    private static void Task1()
    {
        var thread = new Thread(() =>
        {
            while (true)
            {
                Console.WriteLine(1);
                Thread.Sleep(1000);
            }
        });
        thread.Start();

        while (true)
        {
            Console.WriteLine(0);
            Thread.Sleep(3000);
        }
    }

    private static void Task2()
    {
        var thread = new Thread(() => WriteString(1));
        thread.Priority = ThreadPriority.Lowest;
        var thread2 = new Thread(() => WriteString(2));
        thread2.Priority = ThreadPriority.BelowNormal;
        var thread3 = new Thread(() => WriteString(3));
        thread3.Priority = ThreadPriority.AboveNormal;
        var thread4 = new Thread(() => WriteString(4));
        thread4.Priority = ThreadPriority.Highest;

        thread.Start();
        thread2.Start();
        thread3.Start();
        thread4.Start();

        void WriteString(int n)
        {
            Console.WriteLine($"Поток {n} запущен");
            for (var i = 0; i < 1000; i++)
                Console.WriteLine(n);
            Console.WriteLine($"Поток {n} завершен");
        }
    }

    private static void Task3()
    {
        var thread = new Thread(() =>
        {
            while (_commonVar != "x")
            {
                Console.WriteLine(1);
                Thread.Sleep(1000);
            }

            Console.WriteLine("Thread end");
        });
        thread.Start();
        
        Thread.Sleep(5000);
        _commonVar = "x";
        Console.WriteLine("Program end");
    }

    private static void Task4()
    {
        const int count = 10;
        
        Console.Write("Введите n: ");
        if (!int.TryParse(Console.ReadLine(), out var n))
            return;

        var pool = new List<Thread>();

        for (var i = n; i <= n + count; i++)
        {
            var value = i;
            var thread = new Thread(() => 
                SearchDividers(value));
            thread.Start();
            pool.Add(thread);
        }

        foreach (var thread in pool)
            thread.Join();
        
        void SearchDividers(int n)
        {
            for (var i = 1; i <= n; i++)
            {
                if (n % i != 0) 
                    continue;
                
                Console.WriteLine($"{n} делится нацело на {i}.");
            }
        }
    }
}