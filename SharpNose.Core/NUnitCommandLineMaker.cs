namespace SharpNose.Core
{
    public class NUnitCommandLineMaker : CommandLineMaker
    {
        private string path;
        public NUnitCommandLineMaker(string nunitPath) :base()
        {
            path = nunitPath;	
        }
		
        override public string TestRunner
        {
            get
            {
                return path +"\\nunit-console.exe";
            }
			
        }
    }
}