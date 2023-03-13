using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour{
    public Image img;
    public Image img2;

    public AudioSource hover;
    public AudioSource clicked;

    bool highlighted = false;
    bool btnClicked = false;

    private void Start() {
        img2.enabled = false;
    }

    private void Update() {
        if (btnClicked){
            img.enabled = false;
            if(this.gameObject.name == "PlayButton"){
                GameObject.Find("QuitButton").GetComponent<ButtonAnimation>().img.enabled = false;
            }
            return;
        }
        if(this.gameObject.name == "QuitButton" && GameObject.Find("PlayButton").GetComponent<ButtonAnimation>().img2.enabled){
                GameObject.Find("QuitButton").GetComponent<ButtonAnimation>().img.enabled = false;
                return;
        }
        if(this.gameObject.name == "PlayButton" && GameObject.Find("QuitButton").GetComponent<ButtonAnimation>().img2.enabled){
                GameObject.Find("PlayButton").GetComponent<ButtonAnimation>().img.enabled = false;
                return;
        }
        if(highlighted){
            img.enabled = true;
        } else{
            img.enabled = false;
        }
    }

    public void SetHighlighted(){
        if(this.gameObject.name == "QuitButton" && GameObject.Find("PlayButton").GetComponent<ButtonAnimation>().img2.enabled){
                GameObject.Find("QuitButton").GetComponent<ButtonAnimation>().img.enabled = false;
                return;
        }
        if(this.gameObject.name == "PlayButton" && GameObject.Find("QuitButton").GetComponent<ButtonAnimation>().img2.enabled){
                GameObject.Find("PlayButton").GetComponent<ButtonAnimation>().img.enabled = false;
                return;
        }
        if (btnClicked) return;
        hover.Play();
        highlighted = true;
    }

    public void RemoveHighlighted(){
        highlighted = false;
    }

    public void ButtonClicked(){
        if(this.gameObject.name == "QuitButton" && GameObject.Find("PlayButton").GetComponent<ButtonAnimation>().img2.enabled){
                GameObject.Find("QuitButton").GetComponent<ButtonAnimation>().img.enabled = false;
                return;
        }
        if(this.gameObject.name == "PlayButton" && GameObject.Find("QuitButton").GetComponent<ButtonAnimation>().img2.enabled){
                GameObject.Find("PlayButton").GetComponent<ButtonAnimation>().img.enabled = false;
                return;
        }
        if (btnClicked) return;
        btnClicked = true;
        img.enabled = false;
        img2.enabled = true;
        clicked.Play();
        if(gameObject.name == "PlayButton"){
            MainMenu.sharedInstance.PlayGame();
        } else if (gameObject.name == "QuitButton"){
            MainMenu.sharedInstance.Quit();
        }   
    }
}
