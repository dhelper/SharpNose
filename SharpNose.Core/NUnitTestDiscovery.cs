namespace SharpNose.Core
{
    public class NUnitTestDiscovery : TestDiscovery
    {		
        public override string TestFixtureName
        {
            get
            {
                return "TestFixtureAttribute";
            }
        }
    }
}