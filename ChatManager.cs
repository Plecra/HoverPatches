using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class patch_ChatManager : ChatManager
{
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
            }
            else
                if (args[0] == "skybox")
            {
                Game.current_player_manager.Teleport(new Vector3(0f, 5001f, 0f));
                return true;
            } else if (args[0] == "home")
            {
                SpawnPoint.target_spawnpoint_code = SaveManager.GetSpawnPoint();
                Game.current_player_manager.Teleport(SpawnPoint.instance);
                return true;
            }
            else
                if (args[0] == "maps")
            {
                patch_GuiEtenduManager.ChooseMap();
                return true;
            }
            else if (args[0] == "unload")
            {
                FindObjectOfType<AssetBundleLoader>().UnloadBundle();
                FindObjectOfType<AssetBundleLoader>().UnloadFileBundle();
                Resources.UnloadUnusedAssets();
                AddNewComandText("unloaded");
                return false;
            }
        }
        
        if (newText.Equals("/"))
        {
            orig_WriteInChat(newText);
            AddNewComandText("     /real shade (on|off) - more realism in shading");
            AddNewComandText("     /skybox -teleports to skybox");
            AddNewComandText("     /home -teleports to localbox");
            AddNewComandText("     /maps - opens custom maps list");
            AddNewComandText("     /unload - unload currently loaded map");
            AddNewComandText("     /credits - the Modding team!");
            return false;
        }
        return orig_WriteInChat(newText);
    }
}
