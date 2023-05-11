Sablonul dat garanteaza ca in program va exista o clasa unica, dar vom avea acces global la ea.


______________________________
using System;

namespace Single
{

    public class DB
    {
        private string data;
        private static DB dbConnection;

        private DB() => Console.WriteLine("Conectare la DB");
        
        public static DB GetConnection()
        {
            if (dbConnection == null)
                dbConnection = new DB();
            return dbConnection;
         }
        public string SelectData() => data;

        public void InsertData(string str)
        {
            Console.WriteLine("datele noi: " + str + ", sunt adaugate in DB");
            data = str;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DB.GetConnection().InsertData("singleton");
            Console.WriteLine("Datele din DB sunt: "+ DB.GetConnection().SelectData());
        }
    }
}
