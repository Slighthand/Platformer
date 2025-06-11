using UnityEngine;
using UnityEngine.InputSystem;


public class RebindSaveLoad : MonoBehaviour
{
    // totally seperate from our main savegame system. this is the for the entire game.

    public InputActionAsset actions;

    public void OnEnable()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);
    }

    public void OnDisable()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }

}
