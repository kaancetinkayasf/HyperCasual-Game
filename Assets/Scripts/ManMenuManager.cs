using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManMenuManager : MonoBehaviour
{
    [SerializeField]
    private Transform mainMenu;

    [SerializeField]
    private Transform shopMenu;

    public void ActivateShopUI()
    {
        mainMenu.gameObject.SetActive(false);
        shopMenu.gameObject.SetActive(true);
    }

    public void StartTheGame()
    {
        SceneManager.LoadSceneAsync("Level1");
    }

    public void BackToMainMenu()
    {

        mainMenu.gameObject.SetActive(true);
        shopMenu.gameObject.SetActive(false);
    }
}
