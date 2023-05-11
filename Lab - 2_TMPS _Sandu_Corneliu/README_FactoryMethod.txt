Sablonul dat delegheaza crearea obiectelor ce mostenesc datele unor clase.


_________________________________________________________________________________________________________

using System;

namespace ConsoleApp4
{
    //interfata data va realiza producerea de
    //telefoane si casti
    
    interface Product
    {
        void Release();
    }


    
    class Telefon : Product
    {
        public void Release() => Console.WriteLine("S-a fabricat un nou telefon");
    }
    class Casti : Product
    {
        public void Release() => Console.WriteLine("S-a fabricat o pereche noua de casti");
    }


    //Interfata data raspunde de crearea fabricii
    //abstracte de confectionare a productiei
    interface workshop
    {
        Product Create();
    }

    class Telworkshop : workshop
    {
        public Product Create() => new Telefon();
    }

    class Castiworkshop : workshop
    {
        public Product Create() => new Casti();
    }

    class Program
    {
        static void Main(string[] args)
        {
            workshop creator = new Telworkshop();
            Product Telefon = creator.Create();
            creator = new Castiworkshop();
            Product Casti = creator.Create();
            Telefon.Release();
            Casti.Release();

        }
    }
}
