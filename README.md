# Patches for Hover: Revolt of Gamers

Hover has seen a strong modding project patching up missing features and adding cheat codes/
rebalancing progression for long term players. For now, this project is pulling out the patches
that I want to play with and organising it all into a patch-oriented workflow to make it
easier to collaborate on.

## Building

Visual studio can be used to build this. Support for .NET framework 3.5 must be installed.

It relies on being able to reference the game files for compilation. By default it will search
in steam's default installation path, but you can replace the Hover directory by creating a
`Directory.Build.props` from the `Directory.Build.props.user` template.

`msbuild HoverPatches.sln /p:Configuration=Release` can then be used to build the compiled patches.

### Applying the patches

These patches are built on MonoMod. Once the patches have been generated, they can be applied
to a binary by running the MonoMod Patcher. [Documentation is here](https://monomod.dev/docs/README.Patcher.html).

To use it, put the patcher in the directory with the target c# assembly. For hover, that's
`Hover\Hover_Data\Managed`

```
cd Hover\Hover_Data\Managed
mv PATCH_SOURCE_DIR\bin\Release\net35\Hover.Patches.mm.dll .\Assembly-CSharp.Patches.mm.dll
.\MonoMod.exe Assembly-CSharp.dll
```

This will generate a new `MONOMODDED_Assembly-CSharp.dll` - use it to replace the game's `Assembly-CSharp.dll`,
and the patches will be installed!


> This is of course not a user friendly process by any means, and should be streamlined with a few scripts.

## Debugging

dnSpy's debugger as described in https://github.com/dnSpy/dnSpy/wiki/Debugging-Unity-Games works on hover.
Replace the mono.dll with the debug-enabled unity build, and use `Unity (Connect)`-mode in the Start Debugger
dialog. For some reason it wouldnt work when dnSpy tried to directly launch Hover.