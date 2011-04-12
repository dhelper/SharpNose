using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SharpNose.SDK
{
    public enum Operation
    {
        Invalid,
        Config,
        RunTests
    }

    public class ArgumentParser
    {
        private static readonly ReadOnlyCollection<string> SwitchFlags =
            new ReadOnlyCollection<string>(new List<string> {@"-", @"/"});

        private static readonly ReadOnlyCollection<string> RecursiveSwitch =
            new ReadOnlyCollection<string>(new List<string> {"r", "recursive"});

        private static readonly ReadOnlyCollection<string> AssemblyMaskSwitch =
            new ReadOnlyCollection<string>(new List<string> {"m:", "mask:"});

        private Regex _assemblyMask;

        public ArgumentParser(IEnumerable<string> arguments)
        {
            this.Arguments = arguments.ToList();
            InitializeDefaults();
            SetPathAndOperation();
        }

        public List<string> Arguments { get; private set; }

        private List<string> PathArguments
        {
            get { return this.Arguments.FindAll(arg => SwitchFlags.All(flag => !arg.StartsWith(flag))); }
        }

        private List<string> SwitchArguments
        {
            get { return this.Arguments.FindAll(arg => SwitchFlags.Any(arg.StartsWith)); }
        }

        public Operation SelectedOperation { get; private set; }

        public string BasePath { get; private set; }

        public bool RecursiveSearch
        {
            get { return CommandSwitchEnabled(RecursiveSwitch); }
        }

        public bool MaskAssembly
        {
            get { return CommandSwitchEnabled(AssemblyMaskSwitch); }
        }

        public Regex AssemblyMask
        {
            get
            {
                if (this._assemblyMask == null && MaskAssembly)
                {
                    string firstAssemblyMaskArguments = GetSwitches(AssemblyMaskSwitch).First();
                    string assemblyMask = RemoveSwitchFromArgument(
                        firstAssemblyMaskArguments, AssemblyMaskSwitch.ToList());
                    this._assemblyMask = new Regex(assemblyMask, RegexOptions.Compiled);
                }

                return this._assemblyMask;
            }
        }

        private bool CommandSwitchEnabled(IEnumerable<string> commandSwitch)
        {
            return GetSwitches(commandSwitch).Count() > 0;
        }

        private static string RemoveSwitchFromArgument(string argument, List<string> commandSwitches)
        {
            string startWithSwitch =
                commandSwitches.Find(
                    cs =>
                    SwitchFlags.Any(
                        flag =>
                        argument.ToLower().StartsWith((flag + cs).ToLower())));
            argument = argument.Remove(0, startWithSwitch.Count());

            return argument.StartsWith(":") ? argument.Remove(0,1) : argument;
        }

        private IEnumerable<string> GetSwitches(IEnumerable<string> commandSwitch)
        {
            return
                this.SwitchArguments.FindAll(
                    arg =>
                    SwitchFlags.Any(
                        flag => commandSwitch.Any(cs => arg.ToLower().StartsWith((flag + cs).ToLower()))));
        }

        private void SetPathAndOperation()
        {
            if (PathArguments.Count != 1) return;

            string fullPath = GetFullPath(PathArguments.First());

            if (!DirectoryExists(fullPath)) return;

            this.BasePath = fullPath;
            this.SelectedOperation = Operation.RunTests;
        }

        internal virtual bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        internal virtual string GetFullPath(string path)
        {
            return Path.GetFullPath(System.Environment.ExpandEnvironmentVariables(path));
        }

        private void InitializeDefaults()
        {
            this.SelectedOperation = Operation.Invalid;
            this.BasePath = string.Empty;
        }
    }
}
