using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingletonScript : MonoBehaviour
{
    public static int nrOfPlayers = 6;
    public static int[] playerClasses;
    public TMP_Dropdown nrPlayersDropdown;

    private void Start()
    {
        playerClasses = new int[6];
    }
    public void UpdatePlayers()
    {
        nrOfPlayers = nrPlayersDropdown.value + 1;
        Debug.Log(nrOfPlayers);
    }

}
