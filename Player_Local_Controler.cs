class patch_Player_Local_Controler : Player_Local_Controler
{
    public static void ForceLookAtPlayer(Player_Manager player)
    {
        GuiFrame gui_name = player.gui_name;
        gui_name.priority = 5;
        ForceLookAt(player.this_rigidbody.position);
        gui_name.Invoke("Reset", 5f);
        gui_name.Invoke("ClearPriority", 5f);
    }
    public void PlayBreakdance()
    {
        if (player_manager == null) return;
        player_manager.PlayEmote("flair", -1f);
        AchievementDanceTrigger.OnPlayerDance();
    }
}