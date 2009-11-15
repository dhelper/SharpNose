namespace SharpNose.Core
{
    public class NUnitCommandLineMaker : CommandLineMaker
    {
        private readonly string m_path;
        public NUnitCommandLineMaker(string nunitPath) 
        {
            m_path = nunitPath;	
        }
		
        override public string TestRunner
        {
            get
            {
                return m_path +"\\nunit-console.exe";
            }
			
        }
    }
}