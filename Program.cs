using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoBogus;
using Raven.Client.Documents;
using Raven.Client.Documents.BulkInsert;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace Sample.RavenDb.MemoryConsumption
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("First of all, we're looking for ravendb at http://localhost:8080, the database name is Sample.");
            Console.WriteLine("Let's begin inserting 1000 registries.");
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            using(var store = CreateStoreObject())
            using (var session = store.OpenAsyncSession())
            {
                await InsertRegistry(store);

                Console.WriteLine("Ok, now, let's retrieve all documents 50 times");

                for (var i = 0; i < 50; i++)
                {
                    var data = await QueryReturningBigObjects(session);
                    Console.WriteLine($"Query {i+1} done");
                }

                Console.WriteLine("It's the end of using context, this should be gc'ed soon");
            }

            Console.WriteLine("Now, we're outside of the usings.");
            Console.WriteLine("To finish the app, press any key...");
            Console.ReadKey();
        }

        static async Task<IEnumerable<Model01>> QueryReturningBigObjects(IAsyncDocumentSession session) =>
            await session.Query<Model01>()
                .Customize(x => x.NoCaching().NoTracking().WaitForNonStaleResults())
                .Where(x => x.Prop11 != null)
                .ToListAsync();

        static async Task InsertRegistry(IDocumentStore store)
        {
            Console.WriteLine("Inserting 1000's registries");
            using var bulkInsert = store.BulkInsert();

            for (int i = 0; i < 1000; i++)
                await bulkInsert.StoreAsync(AutoFaker.Generate<Model01>());

            Console.WriteLine("Inserts done");
        }

        static IDocumentStore CreateStoreObject() =>
            new DocumentStore
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "Sample",
                Certificate = null,
                Conventions =
                {
                    MaxNumberOfRequestsPerSession = 2000,
                }
            }.Initialize();
    }

    public class Model01
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
        public string Prop03 { get; set; }
        public int Prop04 { get; set; } = 1;
        public string Prop05 { get; set; }
        public int Prop06 { get; set; } = 1;
        public int Prop07 { get; set; } = 1;
        public string Prop08 { get; set; }
        public string Prop09 { get; set; }
        public string Prop10 { get; set; }
        public Model04 Prop11 { get; set; }
        public Model07 Prop12 { get; set; }
        public Model13 Prop13 { get; set; } = new Model13();
        public Model15 Prop14 { get; set; } = new Model15();
        public List<Model06> Prop15 { get; set; } = new List<Model06>();
        public bool Prop16 { get; set; } = false;
        public bool Prop17 { get; set; } = false;
        public bool Prop18 { get; set; } = false;
        public string Prop19 { get; set; }
    }

    public class Model02
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
        public string Prop03 { get; set; }
        public string Prop04 { get; set; }
        public string Prop05 { get; set; }
        public string Prop06 { get; set; }
        public string Prop07 { get; set; }
        public string Prop08 { get; set; }
        public string Prop09 { get; set; }

        public Model02() { }
    }

    public class Model03
    {
        public string Prop01 { get; }
        public string Prop02 { get; set; }
        public bool Prop03 { get; }

        public Model03(string prop01, string prop02, bool prop03 = true)
        {
            Prop01 = prop01;
            Prop02 = prop02;
            Prop03 = prop03;
        }
    }

    public class Model04
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
        public DateTime Prop03 { get; set; }
        public List<Model22> Prop04 { get; set; }
        public bool Prop05 { get; set; }
        public bool Prop06 => Prop04.Any();

        public Model04(string prop01, string prop02, DateTime prop03, bool prop5 = true,
            List<Model22> prop04 = null)
        {
            Prop01 = prop01;
            Prop02 = prop02;
            Prop03 = prop03;
            Prop05 = prop5;
            Prop04 = prop04 ?? new List<Model22>();
        }
    }

    public class Model05
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
        public string Prop03 { get; set; }
        public string Prop04 { get; set; }
        public string Prop05 { get; set; }
        public string Prop06 { get; set; }
        public string Prop07 { get; set; }
        public bool Prop08 { get; set; }
    }

    public class Model06
    {
        public int Prop01 { get; set; }
        public string Prop02 { get; set; }
        public bool Prop03 { get; set; }
        public DateTime Prop04 { get; set; }
        public DateTime? Prop05 { get; set; }
    }

    public class Model07
    {
        public string Prop01 { get; set; }
        public bool Prop02 { get; set; }
        public string Prop03 { get; set; }
        public DateTime Prop04 { get; set; }
        public string Prop05 { get; set; }
        public int Prop06 { get; set; }
        public bool? Prop07 { get; set; }
        public int Prop08 { get; set; }
        public int? Prop09 { get; set; }
        public bool Prop10 { get; set; }
        public Model08 Prop11 { get; set; } = new Model08();
        public bool Prop12 { get; set; }
        public Model09 Prop13 { get; set; } = new Model09();
        public Model10 Prop14 { get; set; } = new Model10();
        public Model11 Prop15 { get; set; } = new Model11();
        public bool? Prop16 { get; set; }
        public List<Model23> Prop17 { get; set; } = new List<Model23>();
        public Model24 Prop18 { get; set; } = new Model24();
        public List<Model26> Prop19 { get; set; } = new List<Model26>();
        public Model27 Prop20 { get; set; } = new Model27();
        public List<Model18> Prop21 { get; set; } = new List<Model18>();
        public Model31 Prop22 { get; set; } = new Model31();
        public Model32 Prop23 { get; set; } = new Model32();
    }

    public class Model08
    {
        public Model09 Prop01 { get; set; } = new Model09();
        public Model25 Prop02 { get; set; } = new Model25();
    }

    public class Model09
    {
        public int Prop01 { get; set; }
        public int Prop02 { get; set; }
        public string Prop03 { get; set; }
        public string Prop04 { get; set; }
        public string Prop05 { get; set; }
        public string Prop06 { get; set; }
        public int Prop07 { get; set; }
        public int Prop08 { get; set; }
        public bool Prop09 { get; set; }
    }

    public class Model10
    {
        public decimal Prop01 { get; set; }
        public decimal Prop02 { get; set; }
    }

    public class Model11
    {
        public decimal Prop01 { get; set; }
        public bool Prop02 { get; set; }
        public bool Prop03 { get; set; }
        public bool? Prop04 { get; set; }
        public bool? Prop05 { get; set; }
        public decimal? Prop06 { get; set; }
        public bool Prop07 { get; set; }
        public string Prop08 { get; set; }
        public string Prop09 { get; set; }
        public bool Prop10 { get; set; }
        public bool Prop11 { get; set; }
        public List<Model12> Prop12 { get; set; } = new List<Model12>();
    }

    public class Model12
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
        public int Prop03 { get; set; }
        public int Prop04 { get; set; }
        public int Prop05 { get; set; }
        public string Prop06 { get; set; }
        public string Prop07 { get; set; }
    }

    public class Model13
    {
        public List<Model05> Prop01 { get; set; } = new List<Model05>();
        public List<Model14> Prop02 { get; set; } = new List<Model14>();
    }

    public class Model14
    {
        public string Prop01 { get; set; }
        public int Prop02 { get; set; }
        public List<string> Prop03 { get; set; }
        public string Prop04 { get; set; }
    }

    public class Model15
    {
        public List<Model17> Prop01 { get; set; } = new List<Model17>();
        public List<Model16> Prop02 { get; set; } = new List<Model16>();
    }


    public enum Enum01
    {
        TypeA,
        TypeB,
        TypeC,
        TypeD,
    }

    public class Model16
    {
        public Enum01 Prop01 { get; set; }
        public int Prop02 { get; set; }
        public string Prop03 { get; set; }
        public long Prop04 { get; set; }
        public DateTime Prop05 { get; set; }
    }

    public class Model17 : Model16
    {
        public string Prop06 { get; set; }
    }

    public class Model18
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
        public int Prop03 { get; set; }
        public string Prop04 { get; set; }
        public bool Prop05 { get; set; }
        public bool Prop06 { get; set; }
        public bool Prop07 { get; set; }
        public bool Prop08 { get; set; }
        public Model19 Prop09 { get; set; } = new Model19();
        public Model20 Prop10 { get; set; } = new Model20();
        public Model21 Prop11 { get; set; } = new Model21();
    }

    public class Model19
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
    }

    public class Model20
    {
        public bool Prop01 { get; set; }
        public int? Prop02 { get; set; }
        public string Prop03 { get; set; }
    }

    public class Model21
    {
        public bool Prop01 { get; set; }
        public bool? Prop02 { get; set; }
        public string Prop03 { get; set; }
        public int? Prop04 { get; set; }
    }

    public class Model22
    {
        public string Prop01 { get; set; }
        public int? Prop02 { get; set; }
        public string Prop03 { get; set; }
        public int Prop04 { get; set; }
    }

    public class Model23
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
    }

    public class Model24
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
        public Model25 Prop03 { get; set; } = new Model25();
    }

    public class Model25
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
        public string Prop03 { get; set; }
        public string Prop04 { get; set; }
    }

    public class Model26
    {
        public Enum02 Prop01 { get; set; }
        public string Prop02 { get; set; }
        public string Prop03 { get; set; }
        public int Prop04 { get; set; }
        public int Prop05 { get; set; }
        public decimal Prop06 { get; set; }
        public decimal Prop07 { get; set; }
        public decimal? Prop08 { get; set; }
        public Model20 Prop09 { get; set; } = new Model20();
        public Model21 Prop10 { get; set; } = new Model21();
        public List<Model26> Prop11 { get; set; } = new List<Model26>();
    }

    public enum Enum02
    {
        TypeA = 1,
        TypeB = 2
    }

    public class Model27
    {
        public Model28 Prop01 { get; set; } = new Model28();

        public Model29 Prop02 { get; set; } = new Model29();

        public Model30 Prop03 { get; set; } = new Model30();

    }

    public class Model28
    {
        public bool Prop01 { get; set; }
        public bool? Prop02 { get; set; }
        public int? Prop03 { get; set; }
        public string Prop04 { get; set; }
        public string Prop05 { get; set; }
    }

    public class Model29
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
        public string Prop03 { get; set; }
        public int? Prop04 { get; set; }
        public int? Prop05 { get; set; }
    }

    public class Model30
    {
        public int? Prop01 { get; set; }
        public int? Prop02 { get; set; }
        public int? Prop03 { get; set; }
        public int? Prop04 { get; set; }
    }

    public class Model31
    {
        public string Prop01 { get; set; }
        public string Prop02 { get; set; }
        public string Prop03 { get; set; }
        public Model19 Prop04 { get; set; } = new Model19();
        public string Prop05 { get; set; }
    }

    public class Model32
    {
        public bool Prop1 { get; set; }
        public bool Prop02 { get; set; }
        public bool Prop03 { get; set; }
        public bool Prop04 { get; set; }
    }
}
