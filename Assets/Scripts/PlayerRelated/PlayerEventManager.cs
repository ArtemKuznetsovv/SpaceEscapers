using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEventManager
{
    public static RectTransform EndGameCanvas;

    public enum e_ActiveItems
    {
        RegularGlassDoor,
        OneSidedGlassDoor,

        ElevatorDoor,
        CaptinRoomDoor,// gold
        ControlRoomDoor, // silver + green

        Key,
        WinningConsole
    }

    public static bool isClicked()
    {
        return Input.anyKey;// && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftControl);
    }

    public static bool isKeyDown()
    {
        return Input.anyKeyDown;
    }

    #region Ending Game

    public static void LoseGame()
    {
        (GameObject.FindGameObjectsWithTag("Finish")[0]).GetComponent<EndGameLogic>().LoseGame();
    }

    public static void WinGame()
    {
        (GameObject.FindGameObjectsWithTag("Finish")[0]).GetComponent<EndGameLogic>().WinGame();
    }

    #endregion
}
