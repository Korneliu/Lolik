Adapteaza clasele pentru ca ele sa poata lucra impreuna.
In cazul dat, adapteaza clasa GBscale dupa clasa Euscale.

____________________________________________________________________
using System;

namespace Adapter
{
    interface Scales
    {
        float Getweight();
    }
    class Euscale : Scales
    {
        private float currentweight;
        public Euscale(float cw) => this.currentweight = cw;
        public float Getweight() => currentweight;
    }

    class Gbscale
    {
        private float currentweight;
        public Gbscale(float cw) => this.currentweight = cw;
        public float Getweight() => currentweight;
    }
    class AdapterGB : Scales
    {
        Gbscale british;
        public AdapterGB(Gbscale british) => this.british = british;

        public float Getweight()=> british.Getweight()*0.453f;

    }



    class Program
    {
        static void Main(string[] args)
        {
            float kg = 55.0f;// kg
            float lb = 55.0f;//lb

            Scales euscales = new Euscale(kg);
            Scales gbscales = new AdapterGB(new Gbscale(lb));

            Console.WriteLine(euscales.Getweight());
            Console.WriteLine(gbscales.Getweight());

        }
    }
}
