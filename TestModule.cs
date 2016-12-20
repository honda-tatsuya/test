using System.Linq;
using Nancy;
using Nancy.Responses.Negotiation;

namespace ConsoleApplication
{
    public class TestModule : NancyModule
    {
        public TestModule() : base("")
        {
            Get("/", _ => showDbTest());
        }

        private Negotiator showDbTest()
        {
            using(var db = new MainContext()) {
                return View["base.html", db.Jobs.ToList()];
            }
        }
    }
}