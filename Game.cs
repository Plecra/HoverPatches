using System;
using System.Diagnostics;
using UnityEngine;

public class patch_Game : Game
{
    extern void orig_LoadLevel(string scene_name, string new_spawn_point_code = "", bool forceCreateMainPlayer = false);
    public new void LoadLevel(string scene_name, string new_spawn_point_code = "", bool forceCreateMainPlayer = false)
    {   
       if (can_load_level)
       {
           PostFX_Manager.instance.ssr2.enabled = false;
           PostFX_Manager.instance.HBAO.enabled = false;
       }
       orig_LoadLevel(scene_name, new_spawn_point_code, forceCreateMainPlayer);
    }
    private extern void orig_Start();

    private void Start()
    {
        orig_Start();
        instance.gameObject.AddComponent<AssetBundleLoader>();
    }
}
