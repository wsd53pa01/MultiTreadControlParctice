using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskRunnerBase run = new WilsonTaskRunner();
            run.ExecuteTasks(10);
        }
    }
}
