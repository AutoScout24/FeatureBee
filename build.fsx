// include Fake libs
#r "tools/Fake.Core/tools/FakeLib.dll"

open Fake

RestorePackages()

let authors = ["AutoScout24"]

// project name and description
let projectName = "FeatureBee"
let projectDescription = "Release your new features without a release."
let projectSummary = projectDescription


// Directories
let buildDir        = "./build/"
let testResultsDir  = "./testresults/"
let deployDir       = "./deploy/"
let packagingDir    = "./packaging/"

let releaseNotes =
    ReadFile "ReleaseNotes.md"
    |> ReleaseNotesHelper.parseReleaseNotes

let buildMode = getBuildParamOrDefault "buildMode" "Release"

// Targets
Target "Clean" (fun _ -> 
    CleanDirs [buildDir; testResultsDir; deployDir; packagingDir]
)
open Fake.AssemblyInfoFile

Target "SetVersion" (fun _ ->
    CreateCSharpAssemblyInfo "./SolutionInfo.cs"
      [ Attribute.Product projectName
        Attribute.Version releaseNotes.AssemblyVersion
        Attribute.FileVersion releaseNotes.AssemblyVersion
        Attribute.ComVisible false]
)

Target "BuildApp" (fun _ ->
    // compile all projects below src/app/
     MSBuild buildDir "Build" ["Configuration", buildMode] ["./FeatureBee.sln"]
        |> Log "AppBuild-Output: "
)

Target "NUnitTest" (fun _ ->  
    !! ("./build/FeatureBee*.dll") 
        |> NUnit (fun p -> 
            {p with
                DisableShadowCopy = true; 
                OutputFile = testResultsDir + "TestResults.xml"})
)

Target "MSpecTest" (fun _ ->
    !! ("./build/FeatureBee*.dll") 
        |> MSpec (fun p -> 
            {p with 
                ExcludeTags = ["LongRunning"]
                HtmlOutputDir = testResultsDir})
)

Target "CreatePackage" (fun _ ->
    let net45Dir = packagingDir @@ "lib/net45/"
    CleanDirs [net45Dir]

    CopyFile net45Dir (buildDir @@ "FeatureBee.Client.dll")
    CopyFiles packagingDir ["LICENSE.txt"; "README.md"; "ReleaseNotes.md"]

    NuGet (fun p ->
        {p with
            Authors = authors
            Project = projectName
            Description = projectDescription
            OutputPath = packagingDir
            Summary = projectSummary
            WorkingDir = packagingDir
            Version = releaseNotes.AssemblyVersion
            ReleaseNotes = toLines releaseNotes.Notes
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" }) "featurebee.nuspec"
)

Target "JasmineTest" DoNothing // TODO: Needs to be done...

Target "Default" DoNothing

// Build order
"Clean"
  ==> "SetVersion"
  ==> "BuildApp"
  ==> "NUnitTest"
  ==> "MSpecTest"
  ==> "JasmineTest"
  ==> "CreatePackage"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
