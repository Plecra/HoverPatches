setlocal enabledelayedexpansion

set "$Path=%1"
set $path=!$path:%cd%=!
set "$Path=!$Path:/=\!"
set gamedir=!$path!

echo "%cd%\%2\%3.dll"
xcopy "%gamedir%" "%cd%\bin\Debug\patched_game\" /E /H /Y /D
copy "%gamedir%\Hover_Data\Managed\Assembly-CSharp.dll" "%cd%\bin\Debug\patched_game\Hover_Data\Managed\Assembly-CSharp.dll" /Y
copy "%cd%\%2%3.dll" "%cd%\bin\Debug\patched_game\Hover_Data\Managed\Assembly-CSharp.Patches.mm.dll" /Y
copy "%cd%\%2%3.pdb" "%cd%\bin\Debug\patched_game\Hover_Data\Managed\Assembly-CSharp.Patches.mm.pdb" /Y

cd bin\Debug\patched_game\Hover_Data\Managed
.\MonoMod.Patcher.exe Assembly-CSharp.dll
move MONOMODDED_Assembly-CSharp.dll Assembly-CSharp.dll
move MONOMODDED_Assembly-CSharp.pdb Assembly-CSharp.pdb