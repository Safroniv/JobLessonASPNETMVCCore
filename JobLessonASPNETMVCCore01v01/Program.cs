



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