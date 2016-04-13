# CPURTD
CPU Reverse-Tower Defense

###Recommended Tools:
- Visual Studio 2015
- Unity 5.2(.4)
- [SourceTree (1.7)]

###Using Visual Studio as Diff Tool in Source Tree

####Setup*
1. Open Options menu through `Tools` > `Options`
2. Under the `Diff` tab, set `External Diff Tool` to `Custom`
3. Set the `Diff Command` to `[VS Install Directory]\Common7\IDE\vsDiffMerge.exe`, where `[VS Install Directory]` is probably `C:\Program Files (x86)\Visual Studio 14.0` and `Arguments` to `$LOCAL $REMOTE \\t`
4. Set `External Merge Tool` to `Custom`
5. Set the `Diff Command` to `[VS Install Directory]\Common7\IDE\vsDiffMerge.exe`, where `[VS Install Directory]` is probably `C:\Program Files (x86)\Visual Studio 14.0` and `Arguments` to `$LOCAL $REMOTE $BASE $MERGED \\m`

*[Source](https://github.com/Inmeta/Knowledge/wiki/Setting-Up-DiffMerge#setting-up-diff-and-merge-for-sourcetree)

####Usage

When a merge conflict is encountered in SourceTree:

1. Right click on the conflicted file and select `Resolve Conflicts` > `Launch External Merge Tool` *(note: Visual Studio may take a few seconds to start up)*
2. When Visual Studio Launches, select the correct changesets and make adjustments to resolve conflicts
3. Click `Accept Merge` (small button in the directly above the split view to the left). The spilt view should be closed.
4. When you focus back to SourceTree, the merged file should be automatically Staged and a `[conflicted file].orig` created. The `.orig` file can be safely deleted.

###Project Setup

1. On the repo page in GitHub, change clone url from `SSH` to `HTTPS` and copy the clone URL (above the repo file listing).
2. In SourceTree, click `Clone / New`.
3. In the dialog that appears paste the clone URL into the `Source Path / URL:` field
2. Set the `Destination Path:` to where ever you want the project
3. Click `Clone`

####Visual Studio Tools for Unity (VS 2015)*

Better integration between Visual Studio and Unity, allows VS breakpoints and inspection

**[Download VS Tools for Unity](https://visualstudiogallery.msdn.microsoft.com/8d26236e-4a64-4d64-8486-7df95156aba9)** and complete setup

**For Editor debugging**, run "Attch to Unity" in Visual Studio, then run the game from Unity. *Warning: Pausing from VS crashes Unity.*

**For Build debugging** In Unity, open the Build Settings dialog under `File > Build Settings` and check `Development Build` and `Script Debugging` (haven't actually gotten this to work).

*[Source](https://msdn.microsoft.com/en-us/library/dn940025.aspx)
