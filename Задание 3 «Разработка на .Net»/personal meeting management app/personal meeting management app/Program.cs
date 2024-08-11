using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
5-сохранить встречи
6-выход");

            ok = int.TryParse(Console.ReadLine(), out Interface);

            if (Interface <= 0 || Interface > 6 || !ok)
            {
                Console.WriteLine("Ошибка");
                ok = false;
            }
            return Interface;
        }
        
        static void Main(string[] args)
        {
            List<Meeting> Meeting = new List<Meeting>
            {
                
            };            

            int Interface;
            
            do
            {
                Interface = PrintMenu();
                switch (Interface)
                {
                    case 1:
                        Console.WriteLine("Введите название встречи");
                        string name = Console.ReadLine();
                        DateTime dn;
                        DateTime dk;
                        do
                        {
                            Console.WriteLine("Введите дату и время начала встречи");
                            dn = DateTime.Parse(Console.ReadLine());
                            if (Meeting.FindAll(p => p._begin <= dn && p._end>= dn).Count != 0) Console.WriteLine("На это время уже назначена встреча");
                            if(dn < DateTime.Now) Console.WriteLine("Встреча должна быть назначена на будущее!");
                        }
                        while (dn < DateTime.Now || Meeting.FindAll(p => p._begin <= dn && p._end >= dn).Count != 0);

                        do
                        {
                            Console.WriteLine("Введите дату и время конца встречи");
                            dk= DateTime.Parse(Console.ReadLine());
                            if (Meeting.FindAll(p => p._begin <= dk && p._end >= dk).Count != 0) Console.WriteLine("На это время уже назначена встреча");
                            if (dk < DateTime.Now || dk<dn) Console.WriteLine("Встреча должна быть назначена на будущее!");
                        }
                        while (dk < DateTime.Now ||dk<dn || Meeting.FindAll(p=>p._begin <= dk && p._end >= dk).Count != 0);
                       

                        Console.WriteLine("За сколько часов напомнить до встречи");
                        int not = int.Parse(Console.ReadLine());
                        Meeting.Add(new Meeting(name, dn, dk, not));
                        
                        
                        break;
                    case 2:
                        Console.WriteLine("Введите название встречи");
                        name = Console.ReadLine();
                        int interface2;
                        var ok = Meeting.Find(p => p._name == name);
                        if (ok != null)
                        {
                            do
                            {
                                Console.WriteLine(@"Что хотите изменить?
                        1-изменить название
                        2-изменить начало встречи
                        3-изменить конец встречи
                        4-имзенить время до уведомления
                        5-выход из меню изменения");
                                interface2 = int.Parse(Console.ReadLine());
                                bool ok2;
                                switch (interface2)
                                {
                                    case 1:
                                        Console.WriteLine("Переименуйте встречу");
                                        //name2 = 
                                        ok._name = Console.ReadLine(); ;
                                        break;
                                    case 2:
                                        do
                                        {
                                            Console.WriteLine("Введите дату и время начала встречи");
                                            dn = DateTime.Parse(Console.ReadLine());
                                            ok2= Meeting.FindAll(p => p._begin <= dn && p._end >= dn).Count != 0 && !Meeting.Contains(ok);
                                            if (ok2) Console.WriteLine("На это время уже назначена встреча");
                                            if (dn < DateTime.Now) Console.WriteLine("Встреча должна быть назначена на будущее!");
                                        }
                                        while (dn< DateTime.Now || ok2);
                                        ok._begin = dn;
                                        break;
                                    case 3:
                                        do
                                        {
                                            Console.WriteLine("Введите дату и время конца встречи");
                                            dk = DateTime.Parse(Console.ReadLine());
                                            ok2 = Meeting.FindAll(p => p._begin <= dk && p._end >= dk).Count != 0 && !Meeting.Contains(ok);
                                            if (ok2) Console.WriteLine("На это время уже назначена встреча");
                                            if (dk < DateTime.Now || dk < ok._end) Console.WriteLine("Встреча должна быть назначена на будущее!");
                                        }
                                        while (dk< DateTime.Now || dk < ok._end || ok2);
                                        ok._end = dk;
                                        break;
                                    case 4:
                                        Console.WriteLine("За сколько часов напомнить до встречи");
                                        not = int.Parse(Console.ReadLine());
                                        ok._notification = not;
                                        break;
                                }

                            }
                            while (interface2 != 5);
                        }
                        else Console.WriteLine("Такой встречи не существует!");
                        
                        break;
                    case 3:
                        Console.WriteLine("Введите название встречи");
                        int search = Meeting.FindIndex(p => p._name == Console.ReadLine());
                        if (search != -1)
                        {
                            Meeting.RemoveAt(search);
                            Console.WriteLine("Встреча удалена!");
                        }
                        else Console.WriteLine("Такой встречи не существует или список пуст");

                        break;
                    case 4:
                        Console.WriteLine("Введите дату для просмотра встреч");
                        dn = DateTime.Parse(Console.ReadLine());
                        var meet2=Meeting.FindAll(p => p._begin.Date == dn.Date);
                        foreach (var meet in meet2)
                        {
                            Console.WriteLine($"Название:{ meet._name} Начало {meet._begin} Конец {meet._end}");
                        }
                        if (meet2.Count == 0) Console.WriteLine("У вас сегодня нет встреч!");
                        break;
                    case 5:
                        Console.WriteLine("Введите дату для сохранения встреч");
                        dn = DateTime.Parse(Console.ReadLine());
                        meet2 = Meeting.FindAll(p => p._begin.Date == dn.Date);
                        if (meet2.Count != 0)
                        {
                            foreach (var meet in meet2)
                            {
                                File.AppendAllText(@"gfg.txt",$"Название:{ meet._name} Начало {meet._begin} Конец {meet._end}\n");
                            }
                            Console.WriteLine("Встречи записаны в файл!");
                        }
                        else   Console.WriteLine("У вас сегодня нет встреч!");                      
                      
                        break;
                       
                }
            }
            while (Interface != 6);
        }
    }
}
