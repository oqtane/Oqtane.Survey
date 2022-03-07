using Oqtane.Models;
using Oqtane.Modules;

namespace Oqtane.Survey
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Survey",
            Description = "Survey",
            Version = "2.0.0",
            ServerManagerType = "Oqtane.Survey.Manager.SurveyManager, Oqtane.Survey.Server.Oqtane",
            ReleaseVersions = "1.0.0,1.0.1,1.0.2,2.0.0",
            Dependencies = "Oqtane.Survey.Shared.Oqtane,Radzen.Blazor,System.Linq.Dynamic.Core",
            PackageName = "Oqtane.Survey"
        };
    }
}
