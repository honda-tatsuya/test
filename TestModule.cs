using Nancy;

namespace ConsoleApplication
{
    public class TestModule : NancyModule
    {
        public TestModule() : base("")
        {
            Get("/", _ => "Hello World!!");
        }
    }
}