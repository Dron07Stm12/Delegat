using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Console_delegate
{

   
    internal class Program
    {

        static bool MyTask() { return true; }

        static int SumIt(object v) {

            int x = (int)v;
            int sum = 0;
            for (; x > 0; x--)
            {
                sum += x;
            }
           return sum;  
        
        }
          //Main основной поток
        static void Main(string[] args)
        {

            Console.WriteLine("Основной поток запущен");

            //public Task(Func<TResult> function);
            // Сконструировать объект первой задачи. 
            Task<bool> tsk = Task<bool>.Factory.StartNew(MyTask);
            Console.WriteLine(tsk.Result);

            //public Task(Func<object?, TResult> function, object? state);
            // Сконструировать объект второй  задачи.
            Task<int> tsk2 = Task<int>.Factory.StartNew(SumIt,3);
            Console.WriteLine(tsk2.Result);

            Func<object, int> func = delegate (object o) {
              
                int x = (int)o;
                int sum = default;    
                for (; x > 0; --x )
                {
                   sum+= x; 
                }
                return sum; 
            };

            //public Task(Func<object?, TResult> function, object? state);
            // Сконструировать объект третьей  задачи, который возвращает обьект int(TResult),
            //а в самом делегате Func - принемает обьект(object?). Сдесь вместо метода безымянный 
            // блок кода func
            Task<int> tsk3 = Task<int>.Factory.StartNew(func,4);
            Console.WriteLine(tsk3.Result);


            tsk.Dispose();
            tsk2.Dispose();
            tsk3.Dispose();

            Console.WriteLine("Основной поток завершен");

        }
    }
}
