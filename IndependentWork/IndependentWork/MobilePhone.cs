using System;
//Класс мобильных телефонов, наследованный от класса телефонов.
namespace IndependentWork
{
    public class MobilePhone : Phone
    {
        //Конструкторы класса.
        public MobilePhone()
        {
 
        }
 
        public MobilePhone(string Name, uint Price) : base(Name, Price)
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
                if (value >= 1000)
                {
                    price = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Неверная стоимость мобильного телефона.");
                }
            }
        }
        //Переопределенный метод, возвращающий строку с описанием мобильного телефона.
        public override string ToString()
        {
            return $"Mobile{base.ToString()}";
        }
    }
}