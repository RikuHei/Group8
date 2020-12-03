using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    public void ShowMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
