using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager sharedInstance;

    [SerializeField] TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI gemDisplay;

    private void Awake(){
        if (sharedInstance == null){
            sharedInstance = this;
        }
    }

    private void Start(){
        StartCoroutine("UpdateUI");
    }

    public void UpdateHealthDisplay(){
        healthDisplay.text = PlayerHealthController.sharedInstance.currentHealth.ToString();
    }

    public void UpdateGemDisplay(){
        gemDisplay.text = PlayerScore.gemsAmount.ToString();
    }

    IEnumerator UpdateUI() {
        while(true){
            UpdateHealthDisplay();
            UpdateGemDisplay();
            yield return new WaitForSeconds(.1f);
        }
    }
    
}
