using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	AudioSource audioSource;
	public AudioClip audio1;
	public GameObject Menu;
	public events eventhandller;
	public SpriteRenderer sprt;
	bool canMove;
	bool invincible;
	public float health;
	public Slider slider;
	protected ContactFilter2D contactFilter;
	public float speed;
	Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
    	audioSource = GetComponent<AudioSource>();
    	invincible = false;
    	canMove = true;
    	health = 1f;
    	contactFilter.useTriggers = false;
    	contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    	contactFilter.useLayerMask = true;
    }

    void Face_mouse(){
    	Vector3 mousePosition = Input.mousePosition;
    	mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

    	Vector2 direction = new Vector2(mousePosition.x - transform.position.x,
    	 mousePosition.y - transform.position.y);

    	transform.up = direction;

    }
    IEnumerator OnTriggerEnter2D(Collider2D other){
    	if(other.gameObject.tag == "ArenaWall"){
    		moveTo(new Vector3(0,0,0), speed);
    		canMove = false;
    		yield return new WaitForSeconds(0.1f);
    		canMove = true;
    	}
    	if(other.gameObject.tag == "Enemy"){
    		if (!invincible){
    			audioSource.clip = audio1;
    			audioSource.Play();
    			health -= 0.05f;
    			if (health <= 0){
    				eventhandller.lost();
    				Destroy(this.gameObject);
    			}
    		}
    	}
    }

    void OnTriggerStay2D(Collider2D other){
    	if(other.gameObject.tag == "ArenaWall"){
    		moveTo(new Vector3(0,0,0), speed*1.5f);
    	}
		else if (other.gameObject.tag != "PrefabWall"){
			Vector3 ndir = new Vector3(other.transform.position.x,other.transform.position.y, 0);
			moveTo(knokbackDir(transform.position, ndir), speed);
		}
    }
    // Update is called once per frame
    void Update()
    {   
    	slider.value = health;
    	Face_mouse();         
    	if (Input.GetKeyDown(KeyCode.LeftShift))
    	{
    		StartCoroutine("dodge");
    	}
    	if (Input.GetKeyDown(KeyCode.Escape))
    	{
    		if (Menu.activeSelf){
    			Time.timeScale = 1;
    			eventhandller.stopWatch.Start();
    		    Menu.SetActive(false);
    		}
    		else{
    			eventhandller.stopWatch.Stop();
    			Time.timeScale = 0;
    			Menu.SetActive(true);	
    		}
    	}
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        if (canMove){
            dir = new Vector3(x,y,0);
            transform.position += dir * speed * Time.deltaTime;
        }
    }

    IEnumerator dodge(){
    	invincible = true;
    	Color tmp = sprt.color;
    	tmp.a = 0.5f;
    	sprt.color = tmp;
    	yield return new WaitForSeconds(0.5f);
    	tmp.a = 1;
    	sprt.color = tmp;
    	invincible = false;

    }
	void moveTo(Vector3 enemy, float speed){
		float x;
		float y;
		Vector3 dir;
		Vector3 pos = transform.position;
		if ((enemy.x - pos.x) >= 0){
			x = speed;
		}

		else {
			x = -speed;
		}

		if ((enemy.y - pos.y) >= 0){
			y = speed;
		}

		else{
			y = -speed;
		}
  
		dir = new Vector3(enemy.x - pos.x ,enemy.y - pos.y ,0);
		dir = dir.normalized;
		transform.position += dir *speed * Time.deltaTime;
	}

	Vector3 knokbackDir(Vector3 obj, Vector3 obstacle){
		Vector3 newDir;
		float x;
		float y;
		x = obj.x + ( obj.x - obstacle.x)*3;
		y = obj.y + (obj.y - obstacle.y)*3;
		newDir = new Vector3(x, y, 0);
		return newDir;
	}
}