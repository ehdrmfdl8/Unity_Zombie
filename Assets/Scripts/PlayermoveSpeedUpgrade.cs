using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayermoveSpeedUpgrade : MonoBehaviour
{
    public Color upgradableColor = Color.blue;
    public Color notUpgradableColor = Color.red;
    public Image colorImage;
    public CanvasGroup canvasGroup;
    [HideInInspector] public int level;
    [HideInInspector] public int currentCost;
    public int startCurrentCost = 100;
    [HideInInspector] public bool isPurchased = false;
    public float costPow = 3.14f; // 가격 배율
    public float upgradePow = 1f; //업그레이드 배율
    public Text UpgradeDisplayer;
    public string itemName;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void PurchaseItem()
    {
        if (GameManager.instance.gold >= currentCost)
        {
            isPurchased = true;
            GameManager.instance.gold -= currentCost;
            level++;

            UpdateItem();
            UpdateUI();
        }
    }

    public void UpdateItem()
    {
        PlayerMovement.Instance.moveSpeed += 0.5f * Mathf.Pow(upgradePow, level);
        currentCost = startCurrentCost * (int)Mathf.Pow(costPow, level);
    }

    public void UpdateUI()
    {
        UpgradeDisplayer.text = itemName + "\nLevel: " + level + "\n가격 " + currentCost + "\n뛰는속도: "
            + PlayerMovement.Instance.moveSpeed;
        if (isPurchased)
        {
            canvasGroup.alpha = 1.0f;
        }
        else
        {
            canvasGroup.alpha = 0.6f;
        }

        if (currentCost <= GameManager.instance.gold)
        {
            colorImage.color = upgradableColor;
        }
        else
        {
            colorImage.color = notUpgradableColor;
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
}