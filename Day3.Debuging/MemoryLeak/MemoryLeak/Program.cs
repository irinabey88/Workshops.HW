using MemoryLeakSinglton;


namespace MemoryLeak
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 70000; i++)
            {
                SingletonMemoryLeak.CreateBigObject();
            }
        }
    }
}
