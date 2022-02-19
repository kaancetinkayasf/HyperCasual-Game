using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    private int diamonds;
    private int healthPrice;
    private int diamondEarningPrice;

    private int diamondEarningValue;
    private int healthValue;

    [SerializeField]
    private TextMeshProUGUI diamondsText;

    [SerializeField]
    private TextMeshProUGUI moneyPerGrabbedDiamondTitle;

    [SerializeField]
    private TextMeshProUGUI healthTitle;

    [SerializeField]
    private TextMeshProUGUI priceForIncreasingGoldEarningsText;

    [SerializeField]
    private TextMeshProUGUI priceForHealthText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("healthValue") && PlayerPrefs.HasKey("diamondEarningValue") && PlayerPrefs.HasKey("healthPrice") && PlayerPrefs.HasKey("diamondEarningsPrice"))
        {
            return;
        }

        PlayerPrefs.SetInt("healthValue", 3);
        PlayerPrefs.SetInt("diamondEarningValue", 1);
        PlayerPrefs.SetInt("healthPrice", 10);
        PlayerPrefs.SetInt("diamondEarningsPrice", 10);
    }

    private void Start()
    {
        diamonds = PlayerPrefs.GetInt("totalDiamonds");
        diamondsText.text = "Diamonds: "+diamonds.ToString();

        diamondEarningValue = PlayerPrefs.GetInt("diamondEarningValue");
        diamondEarningPrice = PlayerPrefs.GetInt("diamondEarningsPrice");

        healthValue = PlayerPrefs.GetInt("healthValue");
        healthPrice = PlayerPrefs.GetInt("healthPrice");

        moneyPerGrabbedDiamondTitle.text = "Diamond Per Grabbed Diamond: " + diamondEarningValue;

        priceForHealthText.text = "Price: " + healthPrice + " Diamonds for +1 Health";
        healthTitle.text = "Health: " + healthValue;
        priceForIncreasingGoldEarningsText.text = "Price: " + diamondEarningPrice + " Diamonds for +1 Diamond per grab";
 
    }


    public void IncreaseDiamondEarnings()
    {
        if(diamonds < diamondEarningPrice)
        {
            Debug.Log("Insufficent Funds");
            return;
        }

        diamondEarningPrice = PlayerPrefs.GetInt("diamondEarningsPrice");
        diamonds -= diamondEarningPrice;
       

        PlayerPrefs.SetInt("diamondEarningValue", diamondEarningValue + 1);
        PlayerPrefs.SetInt("diamondEarningsPrice", diamondEarningPrice + 20);

        UpdateLocalVariables();
        UpdateDiamondsTextAfterBuying();


    }

    public void IncreaseHealth()
    {
        if (diamonds < healthPrice)
        {
            Debug.Log("Insufficent Funds");
            return;
        }

        healthPrice = PlayerPrefs.GetInt("healthPrice");
        diamonds -= healthPrice;
        UpdateTotalDiamonds();

        PlayerPrefs.SetInt("healthValue", healthValue + 1);
        PlayerPrefs.SetInt("healthPrice", healthPrice + 20);

        UpdateLocalVariables();
        UpdateHealthTextAfterBuying();
    }

   

    private void UpdateLocalVariables()
    {
        healthValue = PlayerPrefs.GetInt("healthValue");
        healthPrice = PlayerPrefs.GetInt("healthPrice");
        diamondEarningPrice = PlayerPrefs.GetInt("diamondEarningsPrice");
        diamondEarningValue = PlayerPrefs.GetInt("diamondEarningValue");
    }

    private void UpdateHealthTextAfterBuying()
    {
        
        healthTitle.text = "Health: " + healthValue;
        priceForHealthText.text = "Price: " + healthPrice + " Diamonds for +1 Health";
    }

  
    private void UpdateDiamondsTextAfterBuying()
    {

        UpdateTotalDiamonds();
        moneyPerGrabbedDiamondTitle.text = "Diamond Per Grabbed Diamond: " + diamondEarningValue;
        priceForIncreasingGoldEarningsText.text = "Price: " + diamondEarningPrice + " Diamonds for +1 Diamond per grab";
    }

    private void UpdateTotalDiamonds()
    {
        PlayerPrefs.SetInt("totalDiamonds", diamonds);
        diamondsText.text = "Diamonds: " + diamonds.ToString();
    }

    public void ResetAllAndAddDiamonds()
    {
        PlayerPrefs.SetInt("totalDiamonds", 100);
        PlayerPrefs.SetInt("healthValue", 3);
        PlayerPrefs.SetInt("diamondEarningValue", 1);
        PlayerPrefs.SetInt("healthPrice", 10);
        PlayerPrefs.SetInt("diamondEarningsPrice", 10);
    }


}
