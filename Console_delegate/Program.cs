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


        static void MyTask(object ct)
        {

            //создание признака отмены и явное приведение его к типу object
            CancellationToken token = (CancellationToken)ct;

            // Проверить, отменена ли задача, прежде чем запускать ее. 
            token.ThrowIfCancellationRequested();

            Console.WriteLine("MyTask запущен");

            for (int i = 0; i < 10; i++)
            {
                // В данном примере для отслеживания отмены задачи применяется опрос, 
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Получен запрос на отмену задачи");
                    token.ThrowIfCancellationRequested();
                }
                Thread.Sleep(500);
                Console.WriteLine("В методе MyTask подсчет равен:" + i);

            }

            Console.WriteLine("MyTask завершен");
        }

        //Main основной поток
        static void Main(string[] args)
        {

            Console.WriteLine("Основной поток запущен");

            // Создать объект источника признаков отмены. 
            CancellationTokenSource source = new CancellationTokenSource();
            // Запустить задачу, передав признак отмены ей самой и делегату. 
            Task task = Task.Factory.StartNew(MyTask, source.Token, source.Token);

            Thread.Sleep(1000);

            try
            {
                // Отменить задачу. 
                source.Cancel();

                // Приостановить выполнение метода Main() до тех пор, 
                // пока не завершится задача task.
                task.Wait();


            }
            catch (AggregateException exe)
            {
                if (task.IsCanceled)
                {
                    Console.WriteLine("\nЗадача tsk отменена\n");
                    //Для просмотра исключения снять комментарии со следующей строки кода: 
                    Console.WriteLine(exe);


                }

            }
            finally
            {

                task.Dispose();
                source.Cancel();
            }

            Console.WriteLine("Основной поток завершен");

        }
    }
}
