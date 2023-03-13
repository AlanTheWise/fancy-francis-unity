using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public bool isHpDrop;
    public int healAmount;

    private void Start() {
        Destroy(transform.parent.gameObject, 4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "PlayerFeet"){
            if(isHpDrop){
                SoundManager.sharedInstance.sounds[2].Play();
                PlayerHealthController.sharedInstance.currentHealth += healAmount;
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
