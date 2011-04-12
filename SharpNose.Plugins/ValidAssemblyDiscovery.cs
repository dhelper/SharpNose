using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpNose.SDK
{
    public class ValidAssemblyDiscovery
    {
        private readonly ArgumentParser _args;

        public ValidAssemblyDiscovery(ArgumentParser args)
        {
            _args = args;
        }

        public virtual string BasePath
        {
            get { return _args.BasePath; }
        }

        public virtual ReadOnlyCollection<string> Assemblies
        {
            get { return FindValidAssemblies(this._args).AsReadOnly(); }
        }

        public virtual List<string> FindValidAssemblies(ArgumentParser args)
        {
            List<string> validAssemblies =
                args.RecursiveSearch
                    ? FindValidAssembliesRecursively(args.BasePath)
                    : FindValidAssembliesInPath(args.BasePath);

            if (args.MaskAssembly)
            {
                validAssemblies.RemoveAll(assembly => !args.AssemblyMask.IsMatch(assembly));
            }

            return validAssemblies;
        }

        private List<string> FindValidAssembliesRecursively(string path)
        {
            List<string> testAssemblies = FindValidAssembliesInPath(path).ToList();

            string[] subDirectories = this.GetDirectoriesInPath(path);

            foreach (string subDirectory in subDirectories)
            {
                testAssemblies.AddRange(FindValidAssembliesRecursively(subDirectory));
            }

            return testAssemblies;
        }

        private List<string> FindValidAssembliesInPath(string path)
        {
            ProgressingOrSpinningCursor.ProgressOrSpinCursor();

            return
                (
                    from filename in GetFilesInPath(path)
                    where IsValidAssembly(filename)
                    select filename
                ).ToList();
        }

        internal virtual string[] GetFilesInPath(string path)
        {
            return Directory.GetFiles(path);
        }

        internal virtual string[] GetDirectoriesInPath(string path)
        {
            return System.IO.Directory.GetDirectories(path);
        }

        internal virtual bool IsValidAssembly(string filename)
        {
            try
            {
                AssemblyName.GetAssemblyName(filename);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
