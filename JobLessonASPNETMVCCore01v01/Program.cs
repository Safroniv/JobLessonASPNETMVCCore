



Task1();

Task2();


///<summary>
///Метод использует один поток
///</summary>
static void Task1()
{

    float[] arr = new float[100_000_000];
    DateTime start = DateTime.Now;
    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] = (float)(arr[i] * Math.Sin(0.2f + i / 5) * Math.Cos(0.2f + i / 5) * Math.Cos(0.4f + i / 2));
    }
    DateTime finish = DateTime.Now;
    Console.WriteLine($"Инициализация массива заняла у нас: {finish - start} сек.");
}

///<summary>
///Метод использует два потока
///</summary>
static void Task2()
{
    float[] arr = new float[100_000_000];

    int mid1 = (arr.Length + 1) / 2;
    float[] firstHalf1 = arr.Take(mid1).ToArray();
    Console.WriteLine(firstHalf1.Length);
    //создание одного потока
    Thread thread2 = new Thread(() =>
    {
        Console.WriteLine($"Метод Task2()  thread2 выполняется в контексте потока {Thread.CurrentThread.ManagedThreadId}");
        for (int i = 0; i < firstHalf1.Length; i++)
        {
            firstHalf1[i] = 1;
        }
    });
    thread2.Start();

    float[] secondHalf1 = arr.Skip(mid1).ToArray();
    Console.WriteLine(secondHalf1.Length);
    Thread thread3 = new Thread(() =>
    {
        Console.WriteLine($"Метод Task2()  thread3 выполняется в контексте потока {Thread.CurrentThread.ManagedThreadId}");
        for (int i = 0; i < secondHalf1.Length; i++)
        {
            secondHalf1[i] = 1;
        }
    });
    thread3.Start();
    float[] newArr1 = firstHalf1.Concat(secondHalf1).ToArray();
    Console.WriteLine($"размер сшитого массива = {newArr1.Length}");

    //разделение массива
    int mid2 = (arr.Length + 1) / 2;
    float[] firstHalf2 = arr.Take(mid2).ToArray();
    Console.WriteLine(firstHalf2.Length);
    //создание одного потока
    Thread thread4 = new Thread(() =>
    {
        Console.WriteLine($"Метод Task2()  thread4 выполняется в контексте потока {Thread.CurrentThread.ManagedThreadId}");
        for (int i = 0; i < firstHalf2.Length; i++)
    {
        arr[i] = 1;
    }
    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] = (float)(arr[i] * Math.Sin(0.2f + i / 5) * Math.Cos(0.2f + i / 5) * Math.Cos(0.4f + i / 2));
    }

    DateTime finish = DateTime.Now;

    Console.WriteLine($"Инициализация массива заняла у нас: {finish - start} сек.");
}