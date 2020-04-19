using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Boss : MonoBehaviour
{	
	AudioSource audioSource;
	public events eventhandller;
	public static int life;
	public int count;
	public float speed;
	public GameObject player;
	public SpriteRenderer m_spr;
	public int rage = 1;
	public bool isRage;
	private Vector3 ragedest;
	bool stunned;
    // Start is called before the first frame update
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		stunned = false;
		life = 10000;
		isRage = false;
		speed = 3f;
		StartCoroutine("patern");
		StartCoroutine("rageBuilder");
	}
	IEnumerator rageBuilder(){
		while(true){
			yield return new WaitForSeconds(1);
			if (Time.timeScale == 1){
				count += 1;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.tag == "ArenaWall"){
			Vector3 ndir = new Vector3(other.transform.position.x, other.transform.position.y, 0);
			moveTo(new Vector3(0,0,0), speed*2, false);
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Wall" && isRage){
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Wall" && !isRage){
			Vector3 ndir = new Vector3(other.transform.position.x, other.transform.position.y, 0);
			moveTo(knokbackDir(transform.position, ndir), speed*2, isRage);
		}
		if(other.gameObject.tag == "ArenaWall"){
			Vector3 ndir = new Vector3(other.transform.position.x, other.transform.position.y, 0);
			moveTo(new Vector3(0,0,0), speed*2, false);
			stunned = true;
		}
		if(other.gameObject.tag == "Bullet"){
			audioSource.Play();
			life -= 50;
			if(m_spr.color == Color.red){
				m_spr.color = Color.white;
				m_spr.color = Color.red;	
			}
			else{
				m_spr.color = Color.red;
				m_spr.color = Color.white;
			}
			if (life % 2500 == 0){
				rage = rage * 2;
			}
			if (life <= 0){
				eventhandller.win();
				Destroy(this.gameObject);
			}
		}
	}
	IEnumerator patern(){
		while(true){
			if (stunned){
				yield return new WaitForSeconds(4);
				stunned = false;
			}
			else{
				if (!isRage){
					moveTo(player.transform.position, speed, isRage);
				}
				else{
					ragedest = new Vector3(player.transform.position.x * 2,player.transform.position.y * 2, 0 );
					moveTo(ragedest , speed, isRage);	
				}
				if (count > Math.Floor(1000f / rage)){
					if (isRage){
						isRage = false;
						speed = speed /2;
						m_spr.color = Color.white;
					}
					else{
						//ragedest = new Vector3(player.transform.position.x * 2,player.transform.position.y * 2, 0 );
						isRage = true;
						speed = speed*2;
						m_spr.color = Color.red;
						yield return new WaitForSeconds(5);
					}
					count = 0;
				}
				yield return 0;
			}
		}
	}


    // Update is called once per frame
    void Update()
    {
    	count += 1;
        Vector2 direction = new Vector2(player.transform.position.x - transform.position.x,
    	player.transform.position.y - transform.position.y);

    	transform.up = direction;   
    }

    void moveTo(Vector3 enemy, float speed, bool rush){
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
		if (!rush){
			dir = dir.normalized;
		}
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
