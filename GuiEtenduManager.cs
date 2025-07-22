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
    
	public static void ChoosePlayer()
	{
		List<string> player_names = new List<string>();
		List<int> players = new List<int>();
		foreach (KeyValuePair<int, Player_Manager> item in Game.player_list) {
			if (item.Value != Game.current_player_manager && item.Value.netIdentity != null && item.Value.gamer_type == GamerType.net) {
				player_names.Add(item.Value.displayName);
				players.Add(item.Key);
			}
		}
		if (players.Count == 0) {
			ChatManager.AddNewComandText("No players to pick");
			return;
		}
		GuiGameInterface.CreateListPopup(0, "GuiGeneral.popupFollowPlayer", player_names.ToArray(), new string[player_names.Count], delegate(GuiListButton button, GuiPopUpManager popUpManager)
		{
			if (button != null) {
				var player = Game.player_list[players[button.indexInList]];
				if (player != null) patch_Player_Local_Controler.ForceLookAtPlayer(player);
			}
		});
	}
}
