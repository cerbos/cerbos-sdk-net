string target = Argument("target", "Build");

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
    var path = "./src/Sdk";
    DotNetRestore("./src/Sdk");
    DotNetBuild(path, new DotNetBuildSettings { Configuration = "Release" });
});

Task("UnitTests")
    .Does(() => 
{
    var path = "./src/Sdk.UnitTests"; 
    DotNetRestore(path);
    DotNetBuild(path, new DotNetBuildSettings { Configuration = "Release" });
    DotNetTest(path + "/Sdk.UnitTests.csproj", new DotNetTestSettings { Configuration = "Release" });
});

Task("Pack")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var path = "./src/Sdk";
    DotNetRestore(path);
    DotNetPack(path, new DotNetPackSettings() { Configuration = "Release" });
});

RunTarget(target);