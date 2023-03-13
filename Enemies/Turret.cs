using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour{

    private GameObject bulletPrefab;
    private Transform bulletShootPos;

    public float shootInterval, waitTime;
    public float bulletDestroyAfter;
    public bool isFacingRight = false;

    public HealthBar healthBar;

    private void Start(){
        bulletPrefab = (GameObject)Resources.Load("prefabs/turretbulletprefab", typeof(GameObject));
        bulletShootPos = transform.Find("BulletShootPos");
        
        StartCoroutine("Shoot");
    }

    void Update()
    {
        healthBar.SetHealth(GetComponent<EnemyController>().currentHealth, GetComponent<EnemyController>().maxHealth);
    }

    IEnumerator Shoot(){
        GameObject bullet;
        while(true){
            yield return new WaitForSeconds(waitTime);
            bullet = Instantiate(bulletPrefab, bulletShootPos.position, Quaternion.identity);
            bullet.name = "Bullet";
            bullet.GetComponent<EnemyBullet>().Shoot(isFacingRight, Quaternion.Euler(0, 0, -25));
            bullet.GetComponent<EnemyBullet>().setDestroyAfter(bulletDestroyAfter);


            if(Mathf.Abs(this.transform.position.x - PlayerController.sharedInstance.transform.position.x) <= 7){
                SoundManager.sharedInstance.sounds[3].Play();
            }

            yield return new WaitForSeconds(shootInterval);

            bullet = Instantiate(bulletPrefab, bulletShootPos.position, Quaternion.identity);
            bullet.name = "Bullet";
            bullet.GetComponent<EnemyBullet>().Shoot(isFacingRight, Quaternion.Euler(0, 0, -5));
            bullet.GetComponent<EnemyBullet>().setDestroyAfter(bulletDestroyAfter);

            if(Mathf.Abs(this.transform.position.x - PlayerController.sharedInstance.transform.position.x) <= 7){
                SoundManager.sharedInstance.sounds[3].Play();
            }

            yield return new WaitForSeconds(shootInterval);

            bullet = Instantiate(bulletPrefab, bulletShootPos.position, Quaternion.identity);
            bullet.name = "Bullet";
            bullet.GetComponent<EnemyBullet>().Shoot(isFacingRight, Quaternion.Euler(0, 0, 5));
            bullet.GetComponent<EnemyBullet>().setDestroyAfter(bulletDestroyAfter);

            if(Mathf.Abs(this.transform.position.x - PlayerController.sharedInstance.transform.position.x) <= 7){
                SoundManager.sharedInstance.sounds[3].Play();
            }

            yield return new WaitForSeconds(shootInterval);

            bullet = Instantiate(bulletPrefab, bulletShootPos.position, Quaternion.identity);
            bullet.name = "Bullet";
            bullet.GetComponent<EnemyBullet>().Shoot(isFacingRight, Quaternion.Euler(0, 0, 25));
            bullet.GetComponent<EnemyBullet>().setDestroyAfter(bulletDestroyAfter);

            if(Mathf.Abs(this.transform.position.x - PlayerController.sharedInstance.transform.position.x) <= 7){
                SoundManager.sharedInstance.sounds[3].Play();
            }
        }
    }
}
