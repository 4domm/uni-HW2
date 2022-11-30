using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
namespace IndependentWork
{
    internal class Program
    { 
        private static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в консольное приложение для фильтрации правильных телефонов.");
            do
            {
                try
                {
                    // Чтение данных.
                    string[] data = null;
                    Console.WriteLine("Введите название текстового файла: ");
                    string fileName = CheckDataExist();
                    // Проверка и чтение данных на корректность с помощью определенных методов.
                    data = File.ReadLines(fileName, Encoding.GetEncoding(1251)).Where(line => line.Length > 0)
                        .ToArray();
                    List<string> checkedData = CheckData(data);
                    ;
                    List<Phone> objectData = new List<Phone>();
                    // Обработка данных из файла для создание объектов класса.
                    for (int i = 0; i < checkedData.Count; i++)
                    {
                        switch (checkedData[i].Split()[0])
                        {
                            case "Phone":
                                objectData.Add(new Phone(checkedData[i].Split()[1],
                                    uint.Parse(checkedData[i].Split()[2])));
                                break;
                            case "MobilePhone":
                                objectData.Add(new MobilePhone(checkedData[i].Split()[1],
                                    uint.Parse(checkedData[i].Split()[2])));
                                break;
                            case "SmartPhone":
                                objectData.Add(new SmartPhone(checkedData[i].Split()[1],
                                    uint.Parse(checkedData[i].Split()[2])));
                                break;

                        }
                    }

                    string firstPath;
                    string secondPath;
                    do
                    {
                        // Пользователь выбирает названия для файлов, в которые будут записаны данные.
                        Console.WriteLine(
                            "Введите название файла,где будут храниться мобильные телефоны с четной стоимостью.");
                        firstPath = Console.ReadLine();
                        Console.WriteLine("Введите название файла,где будут храниться смартфоны с четной стоимостью.");
                        secondPath = Console.ReadLine();
                        if (firstPath == secondPath)
                        {
                            Console.WriteLine("Названия совпадают,попробуйте еще раз.");
                            Console.WriteLine();
                        }
                    } while (firstPath == secondPath);

                    // Обработка данных файла с помощью определенных методов и запись их в файлы.
                    string[] firstData = PhoneToString(Sort(MobileEvenPrice(objectData)));
                    string[] secondData = PhoneToString(Sort(SmartEvenPrice(objectData)));
                    Encoding en = Encoding.GetEncoding(1251);
                    using (StreamWriter sw = new StreamWriter(firstPath, false, en))
                    {
                        for (int i = 0; i < firstData.Length; i++)
                        {
                            sw.WriteLine(firstData[i]);
                        }
                    }
                    using (StreamWriter sw = new StreamWriter(secondPath, false, en))
                    {
                        for (int i = 0; i < secondData.Length; i++)
                        {
                            sw.WriteLine(secondData[i]);
                        }
                    }
                    Console.WriteLine("Файлы успешно созданы.");
                }
                // Обработка ошибок и исключений.
                catch (ArgumentOutOfRangeException mes)
                {
                    Console.WriteLine(mes.Message);
                }
                catch (IOException)
                {
                    Console.WriteLine("Ошибка чтения или записи.");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Файл пуст.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Неверное имя файла или его не существует.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Ошибка.");
                }
                Console.WriteLine();
                Console.WriteLine("Для завершения работы программы введите 0(ноль) или любой другой символ для продолжения.");
 
            } 
            //Выход из программы.
            while (Console.ReadLine()!="0");
        }
        //Метод для перевода списка телефонов в массив строк.
        private static string [] PhoneToString(List<Phone> data)
        {
            List<string> temp = new List<string>();
            for (int i = 0; i < data.Count; i++)
            {
                temp.Add(data[i].ToString());
            }
            return temp.ToArray();
        }
        //Метод для проверки данных на корректность.
        private static List<string> CheckData(string[] data)
        {
            List<string> checkedData = new List<string>();
            if (data.Length == 0)
            {
                throw new NullReferenceException();
            }
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Split().Length == 3)
                {
                    if (data[i].Split()[0] == "Phone" || data[i].Split()[0] == "SmartPhone" ||
                        data[i].Split()[0] == "MobilePhone")
                    {
                        if (uint.TryParse(data[i].Split()[2],out _))
                        {
                            checkedData.Add(data[i]);
                        } 
                    }
                }
            }

            if (checkedData.Count == 0)
            {
                throw new Exception();
            }
            return checkedData;
        }
 
        //Метод для поиска мобильных телефонов с четной стоимостью.
        private static List<Phone> MobileEvenPrice(List <Phone> objectData)
        {
            List<Phone> temp=new List<Phone>();
            for (int i = 0; i < objectData.Count; i++)
            {
                if (objectData[i].Price % 2 == 0 && (objectData[i].GetType().ToString().Split('.')[1])=="MobilePhone")
                {
                    temp.Add(objectData[i]);
                }
            }
            return temp;
        }
        //Метод для поиска смартфонов с четной стоимостью.
        private static List <Phone> SmartEvenPrice(List <Phone> objectData)
        {
            List<Phone> temp=new List<Phone>();
            for (int i = 0; i < objectData.Count; i++)
            {
                if (objectData[i].Price % 2 == 0 && (objectData[i].GetType().ToString().Split('.')[1])=="SmartPhone")
                {
                    temp.Add(objectData[i]);
                }
            }
            return temp;
        }
        //Проверка файла на существование.
        private static string CheckDataExist()
        {
            string fileName = Console.ReadLine();
            if (!File.Exists(fileName))
            {
                throw new ArgumentException();
            }
            return fileName;
        }
        //Сортировка телефонов по убыванию стоимости.
        private static List<Phone> Sort(List<Phone> arr)
        {
            Phone temp;
            for (int write = 0; write < arr.Count; write++) {
                for (int sort = 0; sort < arr.Count - 1; sort++) {
                    if (arr[sort].Price < arr[sort+1].Price) {
                        temp = arr[sort + 1];
                        arr[sort + 1] = arr[sort];
                        arr[sort] = temp;
                    }
                }
            }
            return arr;
        }
    }
}