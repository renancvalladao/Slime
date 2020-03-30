using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeafTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI leafDisplay;

    public void UpdateLeafCount()
    {
        leafDisplay.text = "" + playerInventory.leaves;
    }
}
