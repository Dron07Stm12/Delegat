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

            //Создаем экземпляр класса Program
            Program program = new Program();

            //Создаем задачу экземпляра класса Program
            Task task2 = new Task(program.MyTask2);

            //Создаем задачу(MyTask) через конструктор класса Task,
            //т.к. конструктор принемает делегат Action соответствующий сигнатуре метода MyTask
            Task task = new Task(Program.MyTask);

            Console.WriteLine("Идентификатор задачи task: " + task.Id);
            Console.WriteLine("Идентификатор задачи task2: " + task2.Id);


            //запускаем задачи
            task.Start();
            task2.Start();

            //ожидание выполнение задачи task, чтобы она успела выполниться
            //перед закрытием после выполнения программы в методе Main
            //task.Wait();
            //Task[] tasks = {task,task2 };
            //Task [] task1 = new Task[2] {task,task2 };
            //или список задач
            //Task.WaitAll(task2,task);
            // или массив тасков 
            //Task.WaitAll(task1);

            //выход после выполнения одной из задачи
            //Task.WaitAny(task2,task);    

            //выполнение каких-либо действий в методе Main(основной поток)
            for (int i = 0; i < 10; i++)
            {
                //метод Thread. Sleep() использован для сохранения активным основного потока 
                //до тех пор, пока не завершится выполнение метода MyTask() и метода MyTask2
                Thread.Sleep(1000);
                Console.WriteLine($"Код в основном потоке: {i}");
            }
            Console.WriteLine("Основной поток завершен");
            
        }
    }
}
