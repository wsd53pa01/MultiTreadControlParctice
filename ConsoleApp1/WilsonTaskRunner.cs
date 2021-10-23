using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class WilsonTaskRunner : TaskRunnerBase
    {

        public override void Run(IEnumerable<MyTask> tasks)
        {
            var totalStepAmount = PracticeSettings.TASK_TOTAL_STEPS;
            var totalThread = 3;
            var lockObjects = GetLockObject(totalStepAmount);
            tasks.AsParallel()
                .WithDegreeOfParallelism(totalThread)
                .ForAll((t) =>
                {
                    for (int i = 1; i <= totalStepAmount; i++)
                    {
                        Action action = () => t.DoStepN(i);
                        var lockObject = lockObjects[i - 1];
                        ExecuteWork(action, lockObject);
                    }
                });
        }

        public object[] GetLockObject(int amount)
        {
            object[] array = new object[3];
            for (int i = 0; i < amount; i++)
            {
                array[i] = new object();
            }
            return array;
        }

        private void ExecuteWork(Action action , object lockObject)
        {
            lock (lockObject)
            {
                action.Invoke();
            }
        }
    }
}
