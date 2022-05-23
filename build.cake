string target = Argument("target", "Build");

Task("Fetch")
    .Does(() =>
{
    StartProcess("./fetch_protos.sh");
});

Task("Generate")
    .Does(() =>
{
    StartProcess("buf", new ProcessSettings{Arguments = "generate proto"});
});

Task("Build")
    .IsDependentOn("Generate")
    .Does(() =>
{
    var projectPath = "./src/Sdk";

    DotNetRestore(projectPath);

    var buildSettings = new DotNetCorePackSettings();
    var versionNumber = AppVeyor.Environment.Build.Number;
    buildSettings.VersionSuffix = string.Format("beta{0:0000}", versionNumber);
    
    DotNetPack(projectPath, buildSettings);
});

RunTarget(target);