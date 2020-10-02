using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEventManager
{
    public enum e_ActiveItems
    {
        RegularGlassDoor,
        OneSidedGlassDoor,

        ElevatorDoor,
        CaptinRoomDoor,// gold
        ControlRoomDoor, // silver + green

        Key
    }

    public static bool isClicked()
    {
        return Input.anyKey;// && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftControl);
    }

    public static bool isKeyDown()
    {
        return Input.anyKeyDown;
    }

}
