// Using Cake (C# Make) http://cakebuild.net

#addin "Cake.FileHelpers"
#tool "nuget:?package=GitVersion.CommandLine"

var project = "./src/xunit2.ioc.autofac/project.json";

var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");
var artifacts = Argument("Artifacts", "./artifacts/");

GitVersion versionInfo = null;

// Tasks

Task("Clean")
    .Does(() => {
        if (DirectoryExists(artifacts)) {
            DeleteDirectory(artifacts, recursive:true);
        }
    });

Task("Restore")
    .IsDependentOn("Version")
    .Does(() => {
        DotNetCoreRestore("./src/");
    });

Task("GitVersion")
    .Does(() => {
        Information("Calculating version using GitVersion https://github.com/GitTools/GitVersion");

        GitVersion(new GitVersionSettings{
            OutputType = GitVersionOutput.BuildServer
        });

        versionInfo = GitVersion(new GitVersionSettings{ OutputType = GitVersionOutput.Json });

        // Update project.json version string
        foreach (var root in new[] { "src", "test" }) {
            ReplaceRegexInFiles($"{root}/**/project.json",
                @"""version"": ""([0-9]+\.[0-9]+\.[0-9]+[-.]?[0-9a-zA-Z*])""",
                $"\"version\": \"{versionInfo.NuGetVersion}\"");
        }
    });

Task("Version")
    .IsDependentOn("GitVersion")
    .Does(() => {

        Information("GitVersion https://github.com/GitTools/GitVersion");
        Information($"SemVer: {versionInfo.SemVer}");
        Information($"Sha: {versionInfo.Sha}");

        if (string.IsNullOrEmpty(versionInfo.PreReleaseTag)) {
            Information($"Tag: {versionInfo.MajorMinorPatch} (Release)");
        } else {
            Information($"Tag: {versionInfo.PreReleaseTag} (Pre-release)");
        }

        if (AppVeyor.IsRunningOnAppVeyor)
        {
            AppVeyor.UpdateBuildVersion(versionInfo.SemVer);
        }

    });

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() => {
        var settings = new DotNetCoreBuildSettings
        {
            Configuration = configuration
        };

        DotNetCoreBuild(project, settings);
    });

Task("Package")
    .IsDependentOn("Build")
    .Does(() => {
        var settings = new DotNetCorePackSettings
        {
            Configuration = configuration,
            OutputDirectory = artifacts,
            NoBuild = true
        };

        DotNetCorePack(project, settings);

        if (AppVeyor.IsRunningOnAppVeyor)
        {
            foreach (var file in GetFiles(artifacts + "**/*"))
                AppVeyor.UploadArtifact(file.FullPath);
        }
    });

Task("Default")
    .IsDependentOn("Package");

RunTarget(target);
