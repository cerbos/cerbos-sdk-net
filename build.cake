#tool nuget:?package=NUnit.ConsoleRunner&version=3.15.0

string target = Argument("target", "UnitTests");

Task("Fetch")
    .Does(() =>
{
    StartProcess("./fetch_protos.sh");
});

Task("Clean")
    .ContinueOnError()
    .Does(() =>
{
    CleanDirectories("./src/**/bin");
    CleanDirectories("./src/**/obj");
});

Task("Generate")
    .Does(() =>
{
    StartProcess("buf", new ProcessSettings{Arguments = "generate proto"});
});

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Generate")
    .Does(() =>
{
    var paths = new string[] { "./src/Sdk", "./src/Sdk.UnitTests" }; 

    foreach (var path in paths) {
        DotNetRestore(path);
        DotNetBuild(path, new DotNetBuildSettings { Configuration = "Release" });
    }
});

Task("UnitTests")
    .IsDependentOn("Build")
    .Does(() => 
{
    NUnit3("src/Sdk.UnitTests/bin/Release/net6.0/Cerbos.Sdk.UnitTests.dll", new NUnit3Settings { NoResults = true });
});

Task("Pack")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var path = "./src/Sdk";
    DotNetRestore(path);
    DotNetPack(path);
});

RunTarget(target);