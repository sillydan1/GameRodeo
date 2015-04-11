using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour 
{
    public Material[] iconMats;
    private PowerUpType slot1;
    private PowerUpType slot2;
    private Renderer slot1UI;
    private Renderer slot2UI;

    public PowerUpType Slot1
    {
        get
        {
            return slot1;
        }
    }
    public PowerUpType Slot2
    {
        get
        {
            return slot2;
        }
    }

    void Start()
    {
        slot1 = PowerUpType.None;
        slot2 = PowerUpType.None;
        slot1UI = Camera.main.transform.FindChild("Slot1").GetComponent<Renderer>();
        slot2UI = Camera.main.transform.FindChild("Slot2").GetComponent<Renderer>();
    }
    public string GetXYZValue()
    {
        string outp = "";
        #region slot1
        switch (slot1)
        {
            case PowerUpType.Spike:
                outp += "X";
                break;
            case PowerUpType.Poison:
                outp += "Y";
                break;
            case PowerUpType.Shield:
                outp += "Z";
                break;
            case PowerUpType.None:
                outp += "W";
                break;
            default:
                outp += "?";
                break;
        }
        #endregion
        #region slot2
        switch (slot2)
        {
            case PowerUpType.Spike:
                outp += "X";
                break;
            case PowerUpType.Poison:
                outp += "Y";
                break;
            case PowerUpType.Shield:
                outp += "Z";
                break;
            case PowerUpType.None:
                outp += "W";
                break;
            default:
                outp += "?";
                break;
        }
        #endregion
        return outp;
    }
    public void PickUpNewPowerUp(PowerUpType type)
    {
        if (slot1 == PowerUpType.None)
            ChangeSlot1(type); //Slot1 is empty
        else if (slot2 == PowerUpType.None)
            ChangeSlot2(type); //Slot1 is taken, but slot2 is empty
        else
        {
            //Both slots have been taken, Reset the combo
            ChangeSlot1(type);
            ChangeSlot2(PowerUpType.None);
        }
    }
    private void ChangeSlot1(PowerUpType type)
    {
        slot1UI.material = iconMats[(int)type];
        slot1 = type;
    }
    private void ChangeSlot2(PowerUpType type)
    {
        slot2UI.material = iconMats[(int)type];
        slot2 = type;
    }
}
