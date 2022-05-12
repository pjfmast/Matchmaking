// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

//CallerWithAsync();
//MultipleAsyncMethods();

//MultipleAsyncMethodsWithCombinatorWhenAll();
MultipleAsyncMethodsWithCombinatorWhenAny();
Console.ReadLine();



//Zie ook: http://stackoverflow.com/questions/14399232/how-does-task-currentid-work
static Task<string> BakeAsync(string name, int duration)
{
    return Task.Run<string>(async () =>
    {
        Console.WriteLine("running BakeAsync in thread {0} and task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
        await Task.Delay(duration);
        return string.Format("Baked: {0}", name);
    });
}

async static void CallerWithAsync()
{
    Console.WriteLine("started CallerWithAsync in thread {0} and task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
    string result = await BakeAsync("Pizza Salami", 2000);
    Console.WriteLine(result);
    Console.WriteLine("finished BakeAsync");
}


async static void MultipleAsyncMethods()
{
    Stopwatch stopwatch = Stopwatch.StartNew();

    Console.WriteLine("starting two invokes of BakeAsync in thread {0} and task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
    string s1 = await BakeAsync("Pizza Salami", 3000);
    Console.WriteLine("Finished  method 1.\n Result 1: {0}", s1);

    string s2 = await BakeAsync("Pizza Romana", 4000);
    Console.WriteLine("Finished both methods.\n Result 1: {0}\n Result 2: {1}", s1, s2);
    stopwatch.Stop();
    Console.WriteLine($"Finished in: {stopwatch.ElapsedMilliseconds} milliseconds.");
}

async static void MultipleAsyncMethodsWithCombinatorWhenAll()
{
    Console.WriteLine("WhenAll started...");
    Stopwatch stopwatch = Stopwatch.StartNew();

    Task<string> t1 = BakeAsync("Pizza Salami", 3000);
    Task<string> t2 = BakeAsync("Pizza Romana", 4000);
    await Task.WhenAll(t1, t2);

    stopwatch.Stop();
    Console.WriteLine("All task finished: task1:{0} and task2:{1}", t1.IsCompleted, t2.IsCompleted);
    Console.WriteLine("Finished both methods.\n Result 1: {0}\n Result 2: {1}", t1.Result, t2.Result);
    Console.WriteLine($"Finished in: {stopwatch.ElapsedMilliseconds} milliseconds.");
}

async static void MultipleAsyncMethodsWithCombinatorWhenAny()
{
    Console.WriteLine("WhenAny started...");
    Stopwatch stopwatch = Stopwatch.StartNew();

    Task<string> t1 = BakeAsync("Pizza Salami", 6000);
    Task<string> t2 = BakeAsync("Pizza Margarita", 1000);
    Task<string> firstFinished = await Task.WhenAny(t1, t2);
    string firstBaked = await firstFinished;

    Console.WriteLine($"WhenAny finished with {firstBaked} in: {stopwatch.ElapsedMilliseconds} milliseconds.");
    Console.WriteLine("some task finished: task1:{0} or task2:{1}", t1.IsCompleted, t2.IsCompleted);
    Console.WriteLine("Finished reading results from both methods.\n Result 1: {0}\n Result 2: {1}", t1.Result, t2.Result);
    Console.WriteLine($"Printing both results finished in: {stopwatch.ElapsedMilliseconds} milliseconds.");
    stopwatch.Stop();

}