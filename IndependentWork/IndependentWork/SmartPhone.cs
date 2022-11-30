using System;
//Класс смартфонов, наследованный от класса телефонов.

namespace IndependentWork
{
    public class SmartPhone : Phone
    {
        //Конструкторы класса.
        public SmartPhone()
        {
 
        }
        public SmartPhone(string Name, uint Price) : base(Name, Price)
        {
 
        }
        //Проверка стоимости на корректность.
        public override uint Price
        {
            get
            {
                return price;
            } 
            set
            {
                if (value >= 10000)
                {
                    price = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Неверная стоимость смартфона.");
                }
            }
        }
        //Переопределенный метод, возвращающий строку с описанием смартфона.
        public override string ToString()
        {
            return $"Smart{base.ToString()}";
        }
    }
}
