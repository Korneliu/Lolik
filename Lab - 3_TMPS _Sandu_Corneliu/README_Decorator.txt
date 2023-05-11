Acest pattern se foloseste pentru a da diferite comenzi dinamice obiectului.
In cazul dat, vom transmite un mesaj, iar programa o va codifica, apoi o va cripta.


____________________________________________________________
using System;

namespace Decorator
{
    interface Processor {
        void Process();


    }
    class Transmit : Processor
    {
        private string data;
        public Transmit(string data) => this.data = data;
        public void Process() => Console.WriteLine("Datele " + data + " au fost transmise");
    }
    abstract class Protejare : Processor
    {
        protected Processor processor;
        public Protejare(Processor pr) => processor = pr;
        public virtual void Process() => processor.Process();
    }
    class Codificare : Protejare
    {
        public Codificare(Processor pr) : base(pr) { }
        public override void Process()
        {
            Console.WriteLine("S-a codificat mesajul-> ");
            processor.Process();
        }
    }
    class Criptare : Protejare
    {
        public Criptare(Processor pr) : base(pr) { }
        public override void Process()
        {
            Console.WriteLine("S-a criptat mesajul-> ");
            processor.Process();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Processor transmit = new Transmit("Decorator");
            transmit.Process();
            Console.WriteLine();

            Codificare cod = new Codificare(transmit);
            cod.Process();
            Console.WriteLine();

            Criptare cript = new Criptare(cod);
            cript.Process();
        }
    }
}
