using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooting : MonoBehaviour
{
	AudioSource audioSource;
	public AudioClip audio1;
	public AudioClip audio2;
	public GameObject wallCheck;
	public Slider silder;
	public int ammo;
	public Transform firePoint;
	public GameObject bulletPrefab;
	public bool InCreation;
	public int Wallnb;
	public GameObject WallPrefab;
	public float bulletForce = 20f;
	public Text wall_text;
	public Slider ammo_slide;
	public bool canRefil;

	void Start(){
		audioSource = GetComponent<AudioSource>();
		canRefil = true;
		ammo = 20;
		Wallnb = 5;
		StartCoroutine("refilWall");

	}

    void Update()
    {
    	wall_text.text = ": "+Wallnb+"/5";
    	ammo_slide.value = ammo;
    	if(!InCreation){
   	    	if(Input.GetButtonDown("Fire1")){
   	    		if (ammo > 0){
   	    			int r = Random.Range(0,2);
   	    			if (r == 1){
   	    				audioSource.clip = audio2;
   	    			}
   	    			else{
   	    				audioSource.clip = audio1;
   	    			}
   	    			audioSource.Play();
   	    			Shoot();
   	    			ammo -= 1;
   	    		}
        	}
    	    if(Input.GetButtonDown("Fire2")){
    	    	wallCheck.SetActive(true);
    	    	Wall_create();
    	    	InCreation = true;	
        	}
        	if(Input.GetKeyDown(KeyCode.R)){
        		if(canRefil){
					ammo = 20;
					canRefil = false;
					StartCoroutine("refilCoolDown");
				}
        	}
    	}
    }

    void Shoot(){
    	GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    	Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    	rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    void Wall_create(){
    	Vector3 mousePosition = Input.mousePosition;
    	mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    	mousePosition.z = -1;
    	GameObject Wall = Instantiate(WallPrefab, mousePosition, Quaternion.identity);
    }

    IEnumerator refilCoolDown(){
    	silder.value = 0.80f;
    	for(int i = 0; i < 80; i++){
    	    silder.value -= 0.01f;
    	   	yield return new WaitForSeconds(0.1f);
    	}
    	canRefil = true;
    }
    IEnumerator refilWall(){
    	while(true){
	    	if (Wallnb < 5) {
	    		Wallnb += 1;
	    	}
			yield return new WaitForSeconds(2);
    	    }
    }
}

