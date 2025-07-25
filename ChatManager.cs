﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Scripting;

public class patch_ChatManager : ChatManager
{
    public bool show_nametags = true;
    private static patch_ChatManager patch_instance
    {
        get
        {
            return (patch_ChatManager)instance;
        }
    }
    public extern static bool orig_WriteInChat(string newText);
    public new static bool WriteInChat(string newText)
    {
        if (newText.Equals("/credits"))
        {
            AddNewComandText("Patch developers:");
            AddNewComandText("-    Bettehem");
            AddNewComandText("-    Lilly");
            AddNewComandText("-    Toshh");
            AddNewComandText("-    Dark");
            return true;
        }
        if (newText.StartsWith("/"))
        {
            string[] args = newText.Substring(1).ToLower().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (args.Length == 0 || args[0] == "help")
            {
                orig_WriteInChat(newText);
                AddNewComandText("     /play - Hover Jukebox v0.1");
                AddNewComandText("     /real shade (on|off) - more realism in shading");
                AddNewComandText("     /region | eu (default) | us | jp | au |");
                AddNewComandText("     /tutorial -moves you to tutorial");
                AddNewComandText("     /color {RED},{GREEN},{BLUE} -change your own color");
                AddNewComandText("     /find {Player} -get pointed towards a player");
                AddNewComandText("     /nametags on|off -change visibility of player nametags");
                AddNewComandText("     /hoverlight on|off <own|other|all> -change hoverlight visibility outside of missions for own/other/all players");
                AddNewComandText("     /skybox -teleports to skybox");
                AddNewComandText("     /home -teleports to localbox");
                AddNewComandText("     /maps - opens custom maps list");
                AddNewComandText("     /unload - unload currently loaded map");
                AddNewComandText("     /credits - the Modding team!");
                return false;
            }
            if (args[0] == "real" && args[1] == "shade")
            {
                if (args[2] == "on" || args[2] == "1")
                {
                    GameProceduralSky.instance.SkyValue = 0.5f;
                    GameProceduralSky.instance.EquatorValue = 0f;
                    GameProceduralSky.instance.GroundSaturation = 2f;
                    PostFX_Manager.instance.ssr2.iterations = 3;
                    PostFX_Manager.instance.ssr2.pixelStride = 10;
                    PostFX_Manager.instance.ssr2.ssrDownsample = 0;
                    PostFX_Manager.instance.ssr2.blurDownsample = 1;
                    PostFX_Manager.instance.ssr2.pixelStrideZCutoff = 10000f;
                    PostFX_Manager.instance.ssr2.maxRayDistance = 45f;
                    PostFX_Manager.instance.ssr2.binarySearchIterations = 1;
                    PostFX_Manager.instance.ssr2.ReflectionStrength = 0.15f;
                    PostFX_Manager.instance.ssr2.screenEdgeFadeStart = 0.5f;
                    PostFX_Manager.instance.ssr2.enabled = true;
                    PostFX_Manager.instance.HBAO.enabled = true;
                    if (Application.loadedLevelName == "prison 1")
                    {
                        PostFX_Manager.instance.ssr2.pixelStride = -10;
                    }
                    return true;
                } else if (args[2] == "off" || args[2] == "0")
                {
                    GameProceduralSky.instance.SkyValue = 1f;
                    GameProceduralSky.instance.EquatorValue = 0.6f;
                    GameProceduralSky.instance.GroundSaturation = 0.6f;
                    PostFX_Manager.instance.ssr2.enabled = false;
                    PostFX_Manager.instance.HBAO.enabled = false;
                    return true;
                }
                else
                {
                    AddNewComandText("usage:");
                    AddNewComandText("/real shade [on|1]  - turns on realistic shading");
                    AddNewComandText("/real shade [off|0] - turns off realistic shading");
                    return false;
                }
            } else if (args[0] == "break" && args[1] == "dance") {
                if (Player_Local_Controler.instance != null)
                {
                    ((patch_Player_Local_Controler)Player_Local_Controler.instance).PlayBreakdance();
                }
                AddNewComandText("Nice Moves!");
                return true;
            } else if (args[0] == "skybox") {
                Game.current_player_manager.Teleport(new Vector3(0f, 5001f, 0f));
                return true;
            } else if (args[0] == "home") {
                SpawnPoint.target_spawnpoint_code = SaveManager.GetSpawnPoint();
                Game.current_player_manager.Teleport(SpawnPoint.instance);
                return true;
            } else if (args[0] == "maps") {
                patch_GuiEtenduManager.ChooseMap();
                return true;
            } else if (args[0] == "unload") {
                FindObjectOfType<AssetBundleLoader>().UnloadBundle();
                FindObjectOfType<AssetBundleLoader>().UnloadFileBundle();
                Resources.UnloadUnusedAssets();
                AddNewComandText("unloaded");
                return false;
            } else if (args[0] == "tutorial") {
                Game.instance.LoadLevel("Tuto", "tuto");
                return true;
            } else if (args[0] == "region") {
                CloudRegionCode region;
                AddNewComandText("Your current region is " + PhotonNetwork.networkingPeer.CloudRegion);
                if (args.Length == 1) return true;

                switch (args[1])
                {
                    case "us": region = CloudRegionCode.us; break;
                    case "jp": region = CloudRegionCode.jp; break;
                    case "au": region = CloudRegionCode.au; break;
                    case "eu": region = CloudRegionCode.eu; break;
                    default:
                        AddNewComandText("'" + args[1] + "' is not a region. Use us, jp, au, or eu.");
                        return false;
                }
                PhotonNetwork.PhotonServerSettings.PreferredRegion = region;
                AddNewComandText("Region changed. Go offline and back online to join the server.");
                return true;
            } else if (args[0] == "play") {
                AddNewComandText("DarkGamer's Edition exclusive");
                AddNewComandText("Hover Jukebox v0.1");
                AddNewComandText("Music Change:");
                AddNewComandText(" /play [song number]");
                AddNewComandText("       1 .Never 4 Ever");
                AddNewComandText("       2 .Heaven UP");
                AddNewComandText("       3 .Inner Reflexion");
                AddNewComandText("       4 .Underground");
                AddNewComandText("       5 .Wata's Lounge");
                AddNewComandText("       6 .Defy the Great Administrator");
                AddNewComandText("       7 .Enter The City");
                AddNewComandText("       8 .Furtive Ascencion");
                AddNewComandText("       9 .Get Ready");
                AddNewComandText("       0 .Greendy's Trance");
                AddNewComandText("       ! .Grind-e's Secret Goal");
                AddNewComandText("       @ .Hi Jump");
                AddNewComandText("       # .Never Give Up");
                AddNewComandText("       $ .Supprassing Oneself");
                AddNewComandText("       % .Try Hard");
                AddNewComandText("       ^ .Drumbie");
                switch (args.Length == 1 ? "" : args[1])
                {
                    case "2":
                        Music_Manager.SetCustomMusic("HEAVEN★UP", "HEAVEN★UP INTRO", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("HEAVEN★UP selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "1":
                        Music_Manager.SetCustomMusic("NEVER 4EVER", "", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Never 4 Ever selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "4":
                        Music_Manager.SetCustomMusic("UNDERGROUND", "", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Underground selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "3":
                        Music_Manager.SetCustomMusic("INNER REFLEXION", "INNER REFLEXION INTRO", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Inner Reflexion selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "5":
                        Music_Manager.SetCustomMusic("WATA'S LOUNGE", "WATA'S LOUNGE INTRO", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Wata's Lounge selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "6":
                        Music_Manager.SetCustomMusic("DEFY THE GREAT ADMIN", "DEFY THE GREAT ADMIN Intro", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Defy Great Administrator selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "7":
                        Music_Manager.SetCustomMusic("ENTER THE CITY", "ENTER THE CITY MENU", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Enter The City selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "8":
                        Music_Manager.SetCustomMusic("FURTIVE ASCENCION", "", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Furtive Ascencion selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "9":
                        Music_Manager.SetCustomMusic("GET READY", "", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Get Ready selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "0":
                        Music_Manager.SetCustomMusic("GREENDY'S TRANCE", "", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Greendy's Trance selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "!":
                        Music_Manager.SetCustomMusic("GRIND-E's SECRET GOAL", "", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Grind-e's Secret Goal selected");
                        AddNewComandText("Song by Cedric Menendez");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "@":
                        Music_Manager.SetCustomMusic("HI-JUMP", "HI-JUMP INTRO", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Hi Jump selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "#":
                        Music_Manager.SetCustomMusic("NEVER GIVE UP", "NEVER GIVE UP INTRO", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Never Give Up selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "$":
                        Music_Manager.SetCustomMusic("SURPASSING ONESELF", "SURPASSING ONESELF INTRO", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Supprassing Oneself selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "%":
                        Music_Manager.SetCustomMusic("TRY HARD", "", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Try Hard selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "^":
                        Music_Manager.SetCustomMusic("DRUMBIE", "DRUMBIE INTRO", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("Drumbie selected");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                    case "easteregg":
                        Music_Manager.SetCustomMusic("last boss 2nd part", "last boss 2nd part_Intro", 2f);
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        AddNewComandText("How did u knew there was one!? ;D");
                        AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                        return true;
                }
                AddNewComandText("═══════ ♫ ♪ ♫  ♪♫ ♪ ═══════");
                return false;
            } else if (args[0] == "color" || args[0] == "colour") {
                if (args.Length == 4f
                && float.TryParse(args[1], out float red)
                && float.TryParse(args[2], out float green)
                && float.TryParse(args[3], out float blue)) {
                    instance.mainPlayerColor = new Color(red, green, blue, 1f);
                    AddNewComandText("Color successfully changed to " + instance.mainPlayerColor.ToString());
                    return true;
                } else {
                    AddNewComandText("Correct usage (values are 0-1):\n/color {RED},{GREEN},{BLUE}");
                    return false;
                }
            } else if (args[0] == "find") {
                if (args.Length == 1) {
                    patch_GuiEtenduManager.ChoosePlayer();
                    return true;
                } else if (args.Length == 2) {
                    string playerName = args[1].ToLower();
                    var player = Game.player_list.Values.FirstOrDefault(inroom => inroom.displayName.ToLower().Contains(playerName));
                    if (player == null)
                    {
                        AddNewComandText("Player not found: " + playerName);
                        return false;
                    }
                    patch_Player_Local_Controler.ForceLookAtPlayer(player);
                    return true;
                } else {
                    AddNewComandText("Usage: /find [player name]");
                    AddNewComandText("Finds a player by name and looks at them.");
                    return false;
                }
            } else if (args[0] == "nametags") {
                if (args.Length == 1)
                {
                    patch_instance.show_nametags = !patch_instance.show_nametags;
                }
                else if (args.Length == 2 && args[1] == "on")
                {
                    patch_instance.show_nametags = true;
                }
                else if (args.Length == 2 && args[1] == "off")
                {
                    patch_instance.show_nametags = false;
                }
                else
                {
                    AddNewComandText("Usage: /nametags [on|off]");
                    return false;
                }
                AddNewComandText("Adjusting Player Nametags!");
                foreach (KeyValuePair<int, Player_Manager> item in Game.player_list)
                {
                    if (item.Value.id != Game.current_player_manager.id && item.Value.displayNameReady)
                    {
                        item.Value.gui_name.SetActive(patch_instance.show_nametags);
                    }
                }
                return true;
            } else if (args[0] == "hoverlight") {
                List<Player_Manager> list = new List<Player_Manager>();
                bool hoverlightVisibility = false;
                int targets = 3;
                if (args.Length < 2) goto bad_hoverlight_inputs;
                switch (args[1])
                {
                    case "on": hoverlightVisibility = true; break;
                    case "off": hoverlightVisibility = false; break;
                    default: goto bad_hoverlight_inputs;
                }
                if (args.Length < 3) goto got_hoverlight_inputs;
                switch (args[2])
                {
                    case "all": targets = 3; break;
                    case "other": targets = 1; break;
                    case "own": targets = 2; break;
                    default: goto bad_hoverlight_inputs;
                }
                goto got_hoverlight_inputs;
            bad_hoverlight_inputs:
                AddNewComandText("Usage: /hoverlight <on|off> [own|other|all]");
                return false;
            got_hoverlight_inputs:
                if (hoverlightVisibility) {
                    AddNewComandText("Enabling hoverlight!");
                } else {
                    AddNewComandText("Disabling hoverlight!");
                }

                foreach (Player_Manager value in Game.player_list.Values)
                {
                    if (InBitSet((value == Game.current_player_manager) ? 1 : 0, targets))
                    {
                        ((patch_Player_animator_manager_2)value.animator).SetHoverlightVisibility(hoverlightVisibility);
                    }
                }
                return true;
            }
        }
        
        return orig_WriteInChat(newText);
    }
    static bool InBitSet(int index, int bitset)
    {
        return ((1 << index) & bitset) != 0;
    }
}
