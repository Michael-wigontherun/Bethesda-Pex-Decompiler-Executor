# Bethesda-Pex-Decompiler-Executor
Main file always released on github: https://github.com/Michael-wigontherun/Bethesda-Pex-Decompiler-Executor

﻿Overview
﻿One issue with eslifying mods is the method "GetFormFromFile()". The method uses the formID and the plugin name, now if that formID is from the mod your eslifing and its not a formID with the first 5 digits are the number 0, Example: 00000800, then it has a compiled formID that needs to be changed to the new ID and recompiled. This utility does not change it and recompile, because then you would need to run it every single time there is an update and I don't trust code that writes other code because robot apocalypse. It does however allows you to decompile files at mass and scan them for the "GetFormFromFile()".

﻿While this is not a advanced tool it is important to know what to eslify and what not to eslify. Obscene911 made a great guide for eslifing here.
A shortened version of it is do not eslify plugins with .seq files, scripts that call hard coded formIDs from that plugin you want to eslify (See bottom for extra information), and facegen files. This helps with finding hard coded FormIDs.

﻿Installation
Install the Creation Kit from the Bethesda net launcher, it is free.
Install all of the requirements of this.
Then just unpack my archive then drop the folder into the "Skyrim Special Edition\Papyrus Compiler\".

﻿Usage
Run my program once to generate folders.
Then, locate all scripts related to the mod you want to eslify. Copy them over to the Scripts folder that was generated inside the Papyrus Compiler.
Finally run this program again, and choose number 2 to run its .pas scanner

﻿Extra Notes

My other utilities can solve the FaceGen Eslify and Voice Eslify problems respectively.

Source Code: https://github.com/Michael-wigontherun/Bethesda-Pex-Decompiler-Executor

Discord link for for my utilities: https://discord.gg/uNbvqdJRx5

﻿My other utilities

Skyrim New Game Plus esk Batch Builder: https://www.nexusmods.com/skyrimspecialedition/mods/44325

FaceGen Eslify: https://www.nexusmods.com/skyrimspecialedition/mods/46208

Voice Eslify: https://www.nexusmods.com/skyrimspecialedition/mods/49638/
