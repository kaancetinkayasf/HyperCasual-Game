using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private int diamondValue;
    private int healthValue;

    private int currentlyCollectedDiamonds;
    private int currentHealth;
    private int totalDiamonds;

    private bool isLoadingFinished;

    #region SerializedFields
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Transform menuUI;

    [SerializeField]
    private Transform inGameUI;

    [SerializeField]
    private Transform levelEndUI;

    [SerializeField]
    private Transform loadingScreen;

    [SerializeField]
    private TextMeshProUGUI iguDiamondText;

    [SerializeField]
    private TextMeshProUGUI iguHealthText;

    [SerializeField]
    private TextMeshProUGUI endTotalDiamondsText;

    [SerializeField]
    private TextMeshProUGUI endCurrentDiamondsText;


    [SerializeField]
    private TextMeshProUGUI diamondsLostText;

    [SerializeField]
    private Image loadingBar;
    #endregion

    public event Action OnPlayerDied,OnLoadingDone;

    private void Awake()
    {
        playerController.OnCollectDiamond += UpdateCurrentlyCollectedDiamonds;
        playerController.OnTookDamage += UpdateHealthText;
        this.OnPlayerDied += Die;
        playerController.OnLevelEnded += HandleLevelEnded;
        
    }

    private void Die()
    {
        diamondsLostText.text = "Diamonds lost: " + currentlyCollectedDiamonds;
    }

    private void Start()
    {
        currentlyCollectedDiamonds = 0;
        totalDiamonds = PlayerPrefs.GetInt("totalDiamonds");   
        currentHealth = PlayerPrefs.GetInt("healthValue");
        isLoadingFinished = false;

        iguHealthText.text = "Health: " + currentHealth.ToString();
        

        diamondValue = PlayerPrefs.GetInt("diamondEarningValue");


        StartCoroutine(WaitForXSecondsAndDeActivateLoadingUI(2.5f));
    }

    private void Update()
    {
        if (!isLoadingFinished)
        {
            loadingBar.fillAmount += Time.deltaTime * 0.5f;
        }
    }

    private void HandleLevelEnded()
    {
        inGameUI.gameObject.SetActive(false);

        StartCoroutine(WaitForXSecondsAndActivateEndLevelUI(3f));
        
    }

    private void UpdateEndUITexts()
    {

        var totalDiamonds = PlayerPrefs.GetInt("totalDiamonds");

        endCurrentDiamondsText.text = "Collected Diamonds: " + currentlyCollectedDiamonds.ToString();
        endTotalDiamondsText.text = "Total Diamonds: " + totalDiamonds.ToString();
    }

    private void UpdateHealthText(GameObject obj)
    {
        currentHealth -= 1;
        if(currentHealth <= 0)
        {
            OnPlayerDied?.Invoke();
        }
        iguHealthText.text = "Health: " + currentHealth.ToString();
        obj.SetActive(false);
    }

    private void UpdateCurrentlyCollectedDiamonds(GameObject obj)
    {
        currentlyCollectedDiamonds += diamondValue;
        iguDiamondText.text = "Diamonds: " + currentlyCollectedDiamonds.ToString();
    }

    public void ToggleStartGameText(bool isStarted)
    {
        menuUI.gameObject.SetActive(!isStarted);
        inGameUI.gameObject.SetActive(isStarted);
    }

    IEnumerator WaitForXSecondsAndActivateEndLevelUI(float delay)
    {
        yield return new WaitForSeconds(delay);

        levelEndUI.gameObject.SetActive(true);
        totalDiamonds = PlayerPrefs.GetInt("totalDiamonds");
        totalDiamonds += currentlyCollectedDiamonds;
        PlayerPrefs.SetInt("totalDiamonds", totalDiamonds);
        UpdateEndUITexts();
    }

    IEnumerator WaitForXSecondsAndDeActivateLoadingUI(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        loadingScreen.gameObject.SetActive(false);
        isLoadingFinished = true;
        OnLoadingDone?.Invoke();
        
       
    }

    public void ResetTotalDiamonds()
    {
        PlayerPrefs.SetInt("totalDiamonds", 0);
    }

    public void RestartTheScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
