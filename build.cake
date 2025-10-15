string target = Argument("target", "Build");

Task("Clean")
    .ContinueOnError()
    .Does(() =>
{
    var settings = new DeleteDirectorySettings
    {
        Recursive = true,
        Force = true
    };

    EnsureDirectoryDoesNotExist("./src/**/bin", settings);
    EnsureDirectoryDoesNotExist("./src/**/obj", settings);
    EnsureDirectoryDoesNotExist("./src/Sdk/Buf", settings);
    EnsureDirectoryDoesNotExist("./src/Sdk/Cerbos/Api", settings);
    EnsureDirectoryDoesNotExist("./src/Sdk/Google", settings);
    EnsureDirectoryDoesNotExist("./src/Sdk/Grpc", settings);
});

Task("Generate")
    .Does(() =>
{
    StartProcess("buf", new ProcessSettings { Arguments = "generate" });
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
    .IsDependentOn("Generate")
    .Does(() =>
{
    var path = "./src/Sdk";
    DotNetRestore(path);
    DotNetPack(path, new DotNetPackSettings() { Configuration = "Release" });
});

RunTarget(target);