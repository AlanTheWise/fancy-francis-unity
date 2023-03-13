using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Belloc : MonoBehaviour{
    private enum states {shooting, floorfire, weak};
    private states currentState;

    public GameObject bulletPrefab;
    public Transform bulletShootPos;
    private EnemyController ec;
    public GameObject floorFireArea;
    private Animator anim;
    public GameObject shield;

    private float shootInterval = 2f, shootIntervalCounter;
    private int bulletsShooted, bulletsPerState = 8;
    private float floorFlameStartDuration = 2f, floorFlameStartCounter, floorFlameDuration = 4f, floorFlameCounter;
    private float weakDuration= 6f, weakCounter;

    void Start(){
        currentState = states.shooting;
        ec = GetComponent<EnemyController>();
        anim = GetComponent<Animator>();
        ec.isInmortal = true;
        SoundManager.sharedInstance.sounds[8].Stop();
        SoundManager.sharedInstance.sounds[13].Play();
    }

    void Update(){
        if(ec.currentHealth <= 0){
            Invoke("Credits", 5f);
            shield.SetActive(false);
            anim.SetTrigger("Dead");
            if(SoundManager.sharedInstance.sounds[13].isPlaying){
                SoundManager.sharedInstance.sounds[13].Stop();
                SoundManager.sharedInstance.sounds[10].Play();
            }
            return;
        }

        if (ec.isInmortal){
            shield.SetActive(true);
        } else{
            shield.SetActive(false);
        }

        switch (currentState){
            case states.shooting:
                shootIntervalCounter += Time.deltaTime;
                if(shootIntervalCounter >= shootInterval){
                    if (bulletsShooted >= bulletsPerState){
                        bulletsShooted = 0;
                        currentState = states.floorfire;
                        break;
                    }
                    SoundManager.sharedInstance.sounds[11].Play();
                    anim.SetTrigger("FireBallAttack");
                    ShootBullet(new Vector3(bulletShootPos.position.x - Random.Range(0f,2f), bulletShootPos.position.y + Random.Range(-1.25f,0.25f) ,Mathf.Abs(bulletShootPos.position.z)));
                    ShootBullet(new Vector3(bulletShootPos.position.x - Random.Range(0f,2f), bulletShootPos.position.y + Random.Range(2f,4f), Mathf.Abs(bulletShootPos.position.z)));
                    ShootBullet(new Vector3(bulletShootPos.position.x - Random.Range(0f,2f), bulletShootPos.position.y + Random.Range(5f,6f), Mathf.Abs(bulletShootPos.position.z)));
                    bulletsShooted++;
                    shootIntervalCounter = 0;
                } 
                print("shooting state");
                break;

            case states.floorfire:
                anim.SetTrigger("FloorFlameAttack");
                floorFlameStartCounter += Time.deltaTime;
                if(floorFlameStartCounter >= floorFlameStartDuration){
                    if(!SoundManager.sharedInstance.sounds[12].isPlaying && !PauseMenu.isPaused){
                        SoundManager.sharedInstance.sounds[12].Play();
                    }
                    floorFireArea.SetActive(true);
                }
                if(floorFireArea.activeInHierarchy){
                    floorFlameCounter += Time.deltaTime;
                    if (floorFlameCounter >= floorFlameDuration){
                        floorFlameCounter = 0;
                        floorFlameStartCounter = 0;
                        floorFireArea.SetActive(false);
                        SoundManager.sharedInstance.sounds[12].Stop();
                        currentState = states.weak;
                    }
                }
                print("floorfire state");
                break;

            case states.weak:
                anim.SetTrigger("Weak");
                ec.isInmortal = false;
                weakCounter += Time.deltaTime;
                if(weakCounter >= weakDuration){
                    weakCounter = 0;
                    ec.isInmortal = true;
                    currentState = states.shooting;
                }
                print("weak state");
                break;
        }
    }

    public void ShootBullet(Vector3 shootPos){
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, shootPos, Quaternion.identity);
        bullet.name = "Bullet";
        if(ec.isFacingRight){
            bullet.transform.Rotate(0, 180, 0);
        }
        bullet.GetComponent<EnemyBullet>().Shoot(ec.isFacingRight, Quaternion.Euler(0, 0, 0));
        bullet.GetComponent<EnemyBullet>().setDestroyAfter(4f);
    }

    private void Credits(){
        SceneManager.LoadScene("Credits");
    } 
}
