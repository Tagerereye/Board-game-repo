using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject panel;
    Animator animator;
    public Transform classesMenuParent;
    public Transform[] classesMenu;
    int nrOfPlayers;
    // Start is called before the first frame update
    void Start()
    {
        classesMenu = new Transform[6];
        animator = panel.GetComponent<Animator>();
        for (int i=0;i<classesMenuParent.childCount;i++)
        {
            //Debug.Log("getting child " + i + " which is " + classesMenuParent.GetChild(i));
            classesMenu[i] = classesMenuParent.GetChild(i);
        }
       
    }

    // Update is called once per frame
    public void UpdateUI()
    {
        for(int i=1;i<=6;i++)
        {
            if (i <= SingletonScript.nrOfPlayers) classesMenu[i - 1].gameObject.SetActive(true);
            else classesMenu[i - 1].gameObject.SetActive(false);
        }
    }
    public void StartGame()
    {
        panel.SetActive(true);
        for(int i=0;i<SingletonScript.nrOfPlayers;i++)
        {
            Debug.Log("Player " + (i + 1) + " is " + classesMenu[i].GetComponentInChildren<TMP_Dropdown>().value);
            SingletonScript.playerClasses[i] = classesMenu[i].GetComponentInChildren<TMP_Dropdown>().value;
        }
        animator.Play("Base Layer.MenuTurnBlack");
    }
}
