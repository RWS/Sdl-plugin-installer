using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;

namespace Sdl.Community.SdlPluginInstaller.Model
{
    public enum DeployDestination
    {
        Local,
        Roaming,
        ProgramData
    }

    public enum SelectedVersion
    {
        Studio2,
        Studio3,
        Studio4,
        Studio5,
        Studio6
    }

    public class DeployOptions
    {
        [Option('f', "folderpath", HelpText = "Tell the command to deploy all plugins found in a certain folder.")]
        public string FolderPath { get; set; }

        [Option('r', "read", HelpText = "Tell the command to deploy the specified plugin. This must be a full path to the source plugin.")]
        public string SourcePlugin { get; set; }

        [Option('d', "destination", HelpText = "Tell the command to deploy the plugin(s) in one of the deploy locations.",
            Required = true)]
        public DeployDestination DeployLocation { get; set; }

        [Option('c', "closeConsole", HelpText = "Tell the command to close the console when the deploy is finished.", DefaultValue = false)]
        public bool CloseConsole { get; set; }

        [Option('v', "studioVersion",
           HelpText = "Tell the command Studio version. Versions are in the following format: Studio2,Studio3,Studio4,Studio5,Studio6",
           Required = true)]
        public SelectedVersion Version { get; set; }
    }
}
