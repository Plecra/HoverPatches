using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class patch_Player_animator_manager_2 : Player_animator_manager_2
{

    public Color hoverlightColor;

    public extern void orig_SetCustomColor(StateCharacter state);
    public void SetCustomColor(StateCharacter state)
    {
        orig_SetCustomColor(state);
        hoverlightColor = defaut_light_color;
    }
    public void SetHoverlightVisibility(bool is_visible)
    {
        if (is_visible)
        {
            hoverlightColor = defaut_light_color;
        }
        else
        {
            hoverlightColor = new Color(0f, 0f, 0f, 0f);
        }
        SetTeamLightColor(hoverlightColor);
    }

    public void RestoreHoverLight()
    {
        SetHoverlightVisibility(hoverlightColor != new Color(0f, 0f, 0f, 0f));
    }
}
