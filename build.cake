string target = Argument("target", "Default");

Task("Protos")
    .Does(() =>
{
    StartProcess("buf", new ProcessSettings{Arguments = "generate buf.build/cerbos/cerbos-api"});
});

Task("Build")
    .IsDependentOn("Protos")
    .Does(() =>
{
    var projectPath = "./src/Cerbos";

    DotNetCoreRestore(projectPath);

    var buildSettings = new DotNetCorePackSettings();
    var versionNumber = AppVeyor.Environment.Build.Number;
    buildSettings.VersionSuffix = string.Format("beta{0:0000}", versionNumber);
    
    DotNetCorePack(projectPath, buildSettings);
});


RunTarget(target);