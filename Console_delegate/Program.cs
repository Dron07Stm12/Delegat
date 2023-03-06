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

        public void MyTask2() {
            Console.WriteLine(" задача MyTask запущен, екземпляра класса Program");
            int? i = default;
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(1000);
                i = Task.CurrentId;
                Console.WriteLine("В методе MyTask() екземпляра класса Program, подсчет равен " + count + " задача: " + i);
                //Console.WriteLine(i);
               
            }
            Console.WriteLine("задача MyTask завершен, екземпляра класса Program");

        }

        static void MyTask()
        {
            Console.WriteLine(" задача MyTask запущен");
            int? i = default;
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(1000);
                i = Task.CurrentId;
                Console.WriteLine("В методе MyTask(), подсчет равен " + count + " задача: " + i);
            }
            Console.WriteLine("задача MyTask завершен");
        }

          //Main основной поток
        static void Main(string[] args)
        {
            // методы MyTask(), MyTask2() и Main() выполняются параллельно. 
            Console.WriteLine("Основной поток запущен через точку входа Main");

            Task task = Task.Factory.StartNew(delegate () {
                Console.WriteLine("Задача номер: " + Task.CurrentId + " запущена");
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine( " сосчитала число: " + i);
                }
                Console.WriteLine("Задача номер: " + Task.CurrentId + " завершена");
            });

            task.Wait();    
            task.Dispose();

            Task task2 = Task.Factory.StartNew(() => {

                Console.WriteLine("Задача номер: " + Task.CurrentId + " запущена");
                for (int i = 0; i < 7; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine(" сосчитала число: " + i);
                }
                Console.WriteLine("Задача номер: " + Task.CurrentId + " завершена");

            });

            task2.Wait();
            task2.Dispose();    
          
            Console.WriteLine("Основной поток завершен");
            
        }
    }
}
