using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void EnableObject()
    {
        gameObject.SetActive(false);
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}