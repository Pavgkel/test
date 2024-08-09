using System;
using System.Collections.Generic;

namespace personal_meeting_management_app
{
    public class Meeting
    {
        public string _name;
        public DateTime _begin;      
        public DateTime _end;
        public int _notification;
        public Meeting(string Name, DateTime Begin, DateTime End,int Notification)
        {
            if (!string.IsNullOrEmpty(Name)) _name = Name;
            _begin = Begin;
            _end = End;
            _notification = Notification;
        }
    }

    

    class Program
    {
        //печать меню
        static int PrintMenu()
        {
            int Interface;
            bool ok;
            Console.WriteLine(@"Какую задачу выполнить?
1-добавить встречу
2-изменить встречу
3-удалить встречу
4-посмотреть встречи
5-выход");

            ok = int.TryParse(Console.ReadLine(), out Interface);

            if (Interface <= 0 || Interface > 5 || !ok)
            {
                Console.WriteLine("Ошибка");
                ok = false;
            }
            return Interface;
        }
        /*
        static Meeting adding()
        {
            DateTime date1 = new DateTime(2016, 7, 20, 18, 30, 25);
            DateTime date2 = new DateTime(2016, 7, 20, 19, 30, 25);

            List<Meeting> Meeting = new List<Meeting>
            {
                new Meeting("Встреча", date1, date2),
            };
            return Meeting;
        }
        */
        static void Main(string[] args)
        {
            DateTime date1 = new DateTime(2016, 7, 20,18,30,25);
            DateTime date2 = new DateTime(2016, 7, 20,19,30,25);

            List<Meeting> Meeting = new List<Meeting>
            {
                
            };            

            int Interface;
            double[] mas = null;
            string name2;
            do
            {
                Interface = PrintMenu();
                switch (Interface)
                {
                    case 1:
                        Console.WriteLine("Введите название встречи");
                        string name = Console.ReadLine();

                        Console.WriteLine("Введите дату и время начала встречи");
                        DateTime dn = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Введите дату и время конца встречи");
                        DateTime dk = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("За сколько часов напомнить до встречи");
                        int not = int.Parse(Console.ReadLine());
                        Meeting.Add(new Meeting(name, dn, dk,not));
                        break;
                    case 2:
                        Console.WriteLine("Введите название встречи");
                        name = Console.ReadLine();
                        int interface2;
                        do
                        {
                        Console.WriteLine(@"Что хотите изменить?
                        1-изменить название
                        2-изменить начало встречи
                        3-изменить конец встречи
                        4-имзенить время до уведомления
                        5-выход из меню изменения");
                        interface2 = int.Parse(Console.ReadLine());

                            switch (interface2)
                            {
                                case 1:
                                    Console.WriteLine("Переименуйте встречу");
                                    //name2 = 
                                    Meeting.Find(p => p._name == name)._name= Console.ReadLine(); ;
                                    break;
                                case 2:
                                    Console.WriteLine("Введите дату и время начала встречи");
                                    dn = DateTime.Parse(Console.ReadLine());
                                    Meeting.Find(p => p._name == name)._begin = dn;
                                    break;
                                case 3:
                                    Console.WriteLine("Введите дату и время конца встречи");
                                    dk = DateTime.Parse(Console.ReadLine());
                                    Meeting.Find(p => p._name == name)._end = dk;
                                    break;
                                case 4:
                                    Console.WriteLine("За сколько часов напомнить до встречи");
                                    not = int.Parse(Console.ReadLine());
                                    Meeting.Find(p => p._name == name)._notification = not;
                                    break;
                            }
                            
                        }
                        while (interface2 != 5);
                        break;
                    case 3:
                        Console.WriteLine("Введите название встречи");
                        int search = Meeting.FindIndex(p => p._name == Console.ReadLine());
                        Meeting.RemoveAt(search);

                        break;
                    case 4:
                        foreach (var meet in Meeting)
                        {
                            Console.WriteLine($"Название:{ meet._name} Начало {meet._begin} Конец {meet._end}");
                        }
                        break;
                }
            }
            while (Interface != 5);
        }
    }
}
