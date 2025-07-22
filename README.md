# Patches for Hover: Revolt of Gamers

Hover has seen a strong modding project patching up missing features and adding cheat codes/
rebalancing progression for long term players. For now, this project is pulling out the patches
that I want to play with and organising it all into a patch-oriented workflow to make it
easier to collaborate on.

## Building

Visual studio/build tools can be used to build this.

*Dependencies*
- Support for .NET framework 3.5 must be installed.
- MonoMod patcher. `postbuild.bat` currently expects to find `MonoMod.Patcher.exe` in
  `bin\Debug\patched_game\Hover_Data\Managed`

The build scripts will automate the process when you're building from Visual Studio. Here
are the steps they go through.

- Build `Hover.Patches.mm.dll` from the csproj. `msbuild` will be able to do this with most versions of "Build Tools for Visual Studio"
    - The Hover.Patches libraries references the assemblies from the base game, `Assembly-CSharp.dll` in particular.
      The build scripts use the `HoverDirectory` environment variable to locate them.
- Now that we have the compiled patches, they must be applied to the game using monomod.
  The monomod tool is given the name of the assembly to patch: For us, `Assembly-CSharp.dll`.
  It searches for other dlls in the directory with the same prefix to use as patches, and generates
  `MONOMODDED_Assembly-CSharp.dll`. To give it our patch then, we put the patch dll in `Hover\Hover_Data\Managed`
  and rename it to `Hover\Hover_Data\Managed\Assembly-CSharp.Patches.mm.dll`.

Done!

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

## Licensing

My contributions to this project are both released to the public domain and licensed as Apache 2.0 at your option.
However, large parts of the implementation were designed by the original mod authors and has no
licensing - use it only as you see fit.

   Copyright 2025

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.