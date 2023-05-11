Patternul dat se foloseste pentru a delega comanda intre obiecte, astfel incat ea sa fie executata.

_______________________________________
using System;

namespace Chain
{
    interface Worker
    {
        Worker GoNext(Worker worker);
        string Execute(string comman);
    }
    abstract class AbstractWorker : Worker
    {
        private Worker nextworker;
        public AbstractWorker() => nextworker = null;
        public Worker GoNext(Worker worker)
        {
            nextworker = worker;
            return worker;
        }

        public virtual string Execute(string command)
        {
            if (nextworker != null)
                return nextworker.Execute(command);
            return string.Empty;

            
        }
    }
    class Designer : AbstractWorker
    {
        public override string Execute(string command)
        {
            if (command == "Proiecteaza")
                return "Designerul a efectuat comanda: " + command;
            else
                return base.Execute(command);
        }
    }

    class Constructor : AbstractWorker
    {
        public override string Execute(string command)
        {
            if (command == "construieste")
                return "Constructorul a efectuat comanda: " + command;
            else
                return base.Execute(command);
        }
    }
    class Zugrav : AbstractWorker
    {
        public override string Execute(string command)
        {
            if (command == "zugraveste")
                return "Zugravul a efectuat comanda: " + command;
            else
                return base.Execute(command);
        }
    }
    class Program
    {
        public static void GiveCommand (Worker worker, string Command)
        {
            string str = worker.Execute(Command);
            if (str == "")
                Console.WriteLine(Command + " Comanda neinteleasa");
            else
                Console.WriteLine(str);
        }
        static void Main(string[] args)
        {
            Designer designer = new Designer();
            Constructor constructor = new Constructor();
            Zugrav zugrav = new Zugrav();

            designer.GoNext(constructor).GoNext(zugrav);

            GiveCommand(designer, "Proiecteaza");
            GiveCommand(designer, "construieste");
            GiveCommand(designer, "zugraveste");

            GiveCommand(designer, "vinde locuinta");

        }
    }
}
