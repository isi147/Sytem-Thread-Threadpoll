using System.Diagnostics;

int operations = 500;
var watch = new Stopwatch();




watch.Start();
UseThread(operations);
watch.Stop();

Console.WriteLine("Thread Milliseconds :" + watch.ElapsedMilliseconds);

watch.Reset();


watch.Start();
UseThreadPool(operations);
watch.Stop();


Console.WriteLine("ThreadPool Milliseconds :" + watch.ElapsedMilliseconds);


void UseThread(int operation)
{
    using var countdown = new CountdownEvent(operation);
    Console.WriteLine("Threads !!!");

    for (int i = 0; i < operation; i++)
    {
        var thread = new Thread(() =>
        {
            Console.Write($"{Thread.CurrentThread.ManagedThreadId} ");


            countdown.Signal();
        });

        thread.Start();
    }

    countdown.Wait();
    Console.WriteLine();
}



void UseThreadPool(int operation)
{
    using var countdown = new CountdownEvent(operation);

    Console.WriteLine("ThreadPools !!!");

    for (int i = 0; i < operation; i++)
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            Console.Write($"{Thread.CurrentThread.ManagedThreadId} ");


            countdown.Signal();
        });
    }

    countdown.Wait();
    Console.WriteLine();
}
