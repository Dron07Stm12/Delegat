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
            Console.WriteLine(" задача MyTask запущена");
           
            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(1000);
                
                Console.WriteLine("В методе MyTask(), подсчет равен " + count + " задача: " + Task.CurrentId );
            }
            Console.WriteLine("задача MyTask завершена");
        }

        static void ContMyTask(Task task) 
        {
            Console.WriteLine("Продолжение задачи MyTask в задаче  ContMyTask");
            for (int count = 5; count < 10; count++)
            {
                Thread.Sleep(1000);

                Console.WriteLine("В методе ContMyTask(), подсчет равен " + count + " задача: " + Task.CurrentId);
            }
            Console.WriteLine("задача ContMyTask завершена");

        }

          //Main основной поток
        static void Main(string[] args)
        {
            // методы MyTask(), MyTask2() и Main() выполняются параллельно. 
            Console.WriteLine("Основной поток запущен через точку входа Main");

            //Сконструируем обьект первой задачи
            Task task = new Task(MyTask);

            //Создать продолжение задачи
            Task taskCont = task.ContinueWith(ContMyTask);
            Task task1 = taskCont.ContinueWith((first) => {

                Console.WriteLine("Продолжение задачи ContMyTask в задаче  first");
                for (int count = 10; count < 15; count++)
                {
                    Thread.Sleep(1000);

                    Console.WriteLine("В методе first, подсчет равен " + count + " задача: " + Task.CurrentId);
                }
                Console.WriteLine("задача first завершена");

            });

            Task task2 = task1.ContinueWith(delegate {

                Console.WriteLine("Продолжение задачи  first в задаче  анонимной функции");
                for (int count = 15; count < 20; count++)
                {
                    Thread.Sleep(1000);

                    Console.WriteLine("В анонимной функции, подсчет равен " + count + " задача: " + Task.CurrentId);
                }
                Console.WriteLine("задача анонимной функции завершена");


            });

            // Начать последовательность задач, 
            task.Start();

            // Ожидать завершения продолжения. 
          
            task2.Wait();

            task.Dispose();          
            taskCont.Dispose();
            task1.Dispose();
            task2.Dispose();    
          
            Console.WriteLine("Основной поток завершен");
            
        }
    }
}
