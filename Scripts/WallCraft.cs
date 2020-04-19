using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCraft : MonoBehaviour
{
    public GameObject wallCheck;
	public GameObject player;
	public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
    	player = GameObject.FindGameObjectsWithTag("Player")[0];
        wallCheck = GameObject.FindGameObjectsWithTag("Wcheck")[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
    	mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);  
    	mousePosition.z = -1;
    	this.transform.position = mousePosition;

    	Vector2 direction = new Vector2(player.transform.position.x - transform.position.x,
    	player.transform.position.y - transform.position.y);

    	transform.up = direction;
    	if(Input.GetButtonDown("Fire1")){
    		if (player.GetComponent<shooting>().Wallnb > 0){
   		   		Instantiate(wall , this.transform.position, this.transform.rotation);
    	    	player.GetComponent<shooting>().Wallnb -= 1;
    	    }
   		   	player.GetComponent<shooting>().InCreation = false;
            wallCheck.SetActive(false);
   	    	Destroy(this.gameObject);
    	}
       	if(Input.GetButtonDown("Fire2")){
    		player.GetComponent<shooting>().InCreation = false;
            wallCheck.SetActive(false);
    		Destroy(this.gameObject);    		
    	}
    }
}
