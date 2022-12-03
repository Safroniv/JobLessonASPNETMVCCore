
Task1();
Task2();
Console.WriteLine("\nВывод сделанный после выполнения:" +
    "\n Разделение выполнения операций по потокам ускоряет выполнение кода программы" +
    "\n пропорционально количеству создаваемых потоков!");

///<summary>
///Метод использует один поток
///</summary>
static void Task1()
{
    DateTime start = DateTime.Now;
    Console.WriteLine($"Метод Task1() выполняется в контексте потока {Thread.CurrentThread.ManagedThreadId}");
    float[] arr = new float[100_000_000];
    for (int i = 0; i < arr.Length; i++)
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

///<summary>
///Метод использует два потока
///</summary>
static void Task2()
{
    DateTime start = DateTime.Now;
    Console.WriteLine($"Метод Task2() выполняется в контексте потока {Thread.CurrentThread.ManagedThreadId}");
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
            firstHalf2[i] = (float)(arr[i] * Math.Sin(0.2f + i / 5) * Math.Cos(0.2f + i / 5) * Math.Cos(0.4f + i / 2));
        }
    });
    thread4.Start();

    float[] secondHalf2 = arr.Skip(mid2).ToArray();
    Console.WriteLine(secondHalf2.Length);
    //создание второго потока
    Thread thread5 = new Thread(() =>
    {
        Console.WriteLine($"Метод Task2()  thread5 выполняется в контексте потока {Thread.CurrentThread.ManagedThreadId}");
        for (int j = 0; j < secondHalf2.Length; j++)
        {
            secondHalf2[j] = (float)(arr[j] * Math.Sin(0.2f + j / 5) * Math.Cos(0.2f + j / 5) * Math.Cos(0.4f + j / 2));
        }
    });
    thread5.Start();
    //соединение подмассивов в новый массив
    float[] newArr2 = firstHalf2.Concat(secondHalf2).ToArray();
    Console.WriteLine($"размер сшитого массива = {newArr2.Length}");


    DateTime finish = DateTime.Now;

    Console.WriteLine($"Инициализация массива заняла у нас: {finish - start} сек.");
}