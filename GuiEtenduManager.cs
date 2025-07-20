using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class patch_GuiEtenduManager : GuiEtenduManager
{
    public static void ChooseMap()
    {
        string[] maps = AssetBundleLoader.GetMapCandidates();
        if (maps.Length == 0) {
            ChatManager.AddNewComandText("No maps to pick");
            return;
        }
        var mapNames = (from v in maps select Path.GetFileName(v)).ToArray();
        GuiGameInterface.CreateListPopup(0, "GuiGeneral.popupChooseCustomMap", new string[maps.Length], mapNames, delegate (GuiListButton button, GuiPopUpManager popUpManager)
        {
            if (button == null) return;
            string chosen_map = maps[button.indexInList];
            Debug.Log("folder: " + chosen_map);
            FindObjectOfType<AssetBundleLoader>().LoadFileFromBundle(chosen_map.ToString());
        });
    }
}
