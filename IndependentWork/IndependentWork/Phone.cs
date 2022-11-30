using System;
//Базовый класс телефонов.
namespace IndependentWork
{
    public class Phone
    {
        protected string Name;
        protected uint price;
        //Проверка стоимости на корректность.
        public virtual uint Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value>=100)
                {
                    price = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Неверная стоимость телефона.");
                }
            }
        }
        //Переопределенный метод, возвращающий строку с описанием телефона.
        public override string ToString()
        {
            return $"Phone {Name} {Price}";
        }
        //Конструкторы класса.
        public Phone()
        {
     
        }
        public Phone(string Name,uint Price)
        {
            this.Name = Name;
            this.Price = Price;
        }
        //Переопределенный метод для сравнения телефонов по всем параметрам.
        public override bool Equals(object obj)
        {
            string equal = $"{GetType()} {Name} {Price}";
            string objString = "";
            string[] check = obj.ToString().Split();
            string equCheck = equal.Split()[1] + " " + equal.Split()[2];
     
            for (int i = 1; i < check.Length; i++)
            {
                if (i != check.Length - 1)
                {
                    objString += check[i] + " ";
                }
                else
                {
                    objString += check[i];
                }
            }
     
            if (equCheck == objString && equal.Split()[0]==obj.GetType().ToString())
            {
                return true;
            }
            return false;
        }
     
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

