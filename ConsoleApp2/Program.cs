using System;
using System.Data;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Days days = new Days();
            int c;
            DataTable data;
            DataView view;
            data = days.GetData();
            Console.WriteLine("id   Имя           Дата рождения      Заметки");
            foreach (DataRow item in data.Rows)
            {
                Console.WriteLine(item[0].ToString() + "  " + item[1].ToString() + ((DateTime)item[2]).ToShortDateString() + "       " + item[3].ToString());
            }
            while (true)
            {
                Console.Write("1.Вывод списка дней рождения\n" +
                    "2.Вывод 3 ближайших дней рождения\n" +
                    "3.Добавление в список\n" +
                    "4.Удаление из списка\n" +
                    "5.Изменение записи\n");
                c = Console.Read() - '0';
                Console.ReadLine();
                if (c == 1)
                {
                    data = days.GetData();
                    Console.WriteLine("id   Имя           Дата рождения      Заметки");
                    foreach (DataRow item in data.Rows)
                    {
                        Console.WriteLine(item[0].ToString() + "  " + item[1].ToString() + ((DateTime)item[2]).ToShortDateString() + "       " + item[3].ToString());
                    }
                }
                if (c == 2)
                {
                    data = days.GetDataLast();
                    view = data.DefaultView;
                    Console.WriteLine("Ближайшие дни рождения в этом году: ");
                    Console.WriteLine("id   Имя           День рождения      Заметки");
                    if(view.Count >0)
                    for (int i = 0; i < 3; i++)
                    {
                        if (view[i] == null)
                            break;
                        Console.WriteLine(view[i][0].ToString() + "  " + view[i][1].ToString() + ((DateTime)view[i][2]).ToShortDateString() + "       " + view[i][3].ToString());
                    }
                }
                if (c == 3)
                {
                    Console.WriteLine("Введите имя");
                    string name = Console.ReadLine();
                    Console.WriteLine("Введите дату рождения");
                    DateTime date;
                    while (true)
                    {
                        try
                        {
                            date = Convert.ToDateTime(Console.ReadLine());
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Неверный ввод, повторите");
                        }
                    }
                    Console.WriteLine("Введите доп. информацию");
                    string note = Console.ReadLine();
                    if (!(days.add(name, date, note)))
                        Console.WriteLine("Ошибка");
                }
                if (c == 4)
                {
                    data = days.GetData();
                    Console.WriteLine("id   Имя           Дата рождения      Заметки");
                    foreach (DataRow item in data.Rows)
                    {
                        Console.WriteLine(item[0].ToString() + "  " + item[1].ToString() + ((DateTime)item[2]).ToShortDateString() + "       " + item[3].ToString());
                    }
                    Console.WriteLine("Введите id для удаления");
                    int id;
                    while (true)
                    {
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Неверный ввод, повторите");
                        }
                    }
                    if (!(days.delete(id)))
                        Console.WriteLine("Ошибка");
                }
                if (c == 5)
                {
                    data = days.GetData();
                    Console.WriteLine("id   Имя           Дата рождения      Заметки");
                    foreach (DataRow item in data.Rows)
                    {
                        Console.WriteLine(item[0].ToString() + "  " + item[1].ToString() + ((DateTime)item[2]).ToShortDateString() + "       " + item[3].ToString());
                    }
                    Console.WriteLine("Введите id для изменения(Поля без введенных данных останутся без изменений)");
                    int id;
                    DataRow row = null;
                    while (true)
                    {
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Неверный ввод, повторите");
                        }
                    }
                    foreach (DataRow item in data.Rows)
                    {
                        if ((int)item[0] == id)
                        {
                            row = item;
                            break;
                        }
                    }
                    if (row != null)
                    {
                        Console.WriteLine(row[0].ToString() + "  " + row[1].ToString() + ((DateTime)row[2]).ToShortDateString() + "       " + row[3].ToString());
                        Console.WriteLine("Введите имя");
                        string name = Console.ReadLine();
                        if (name == "")
                        {
                            name = row[1].ToString();
                        }
                        Console.WriteLine("Введите дату рождения");
                        DateTime date;
                        while (true)
                        {
                            try
                            {
                                string dates = Console.ReadLine();
                                if (dates == "")
                                {
                                    date = Convert.ToDateTime(row[2]);
                                    break;
                                }
                                date = Convert.ToDateTime(dates);
                                break;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Неверный ввод, повторите");
                                throw;
                            }
                        }
                        Console.WriteLine("Введите доп. информацию");
                        string note = Console.ReadLine();
                        if (note == "")
                        {
                            note = row[3].ToString();
                        }
                        if (!(days.change(id, name, date, note)))
                            Console.WriteLine("Ошибка");
                    }
                    else
                    {
                        Console.WriteLine("Не найдено");
                    }
                }
            }
        }
    }
}
