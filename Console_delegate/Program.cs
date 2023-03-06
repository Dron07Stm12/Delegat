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


            Action<Task, object> action = delegate (Task task1,object u)
            {
                Console.WriteLine("Продолжение задачи MyTask в задаче action");
                for (int count = 5; count < 10; count++)
                {
                    Thread.Sleep(1000);

                    Console.WriteLine("В методе ContMyTask(), подсчет равен " + (count + (int)u) + " задача: " + Task.CurrentId);
                }
                Console.WriteLine("задача action завершена");
              
            };


            //Сконструируем обьект первой задачи
            Task task = new Task(MyTask);
            object p = 6;
            //Создадим продолжение задачи task
            // вторая задача(taskCont) не начинается до тех 
            //пор, пока не завершится первая. 
            Task taskCont = task.ContinueWith(action, p);
            //Создадим продолжение задачи taskCont
            // третья задача(taskCont2) не начинается до тех 
            //пор, пока не завершится вторая. 
            Task taskCont2 = taskCont.ContinueWith(ContMyTask);
            // Начать последовательность задач, 
            task.Start();

            // Ожидать завершения продолжения. 
            //taskCont.Wait();
            taskCont2.Wait();

            task.Dispose(); 
            taskCont.Dispose(); 
            taskCont2.Dispose();
          
            Console.WriteLine("Основной поток завершен");
            
        }
    }
}
