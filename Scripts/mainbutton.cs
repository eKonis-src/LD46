using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mainbutton : MonoBehaviour
{
    // Start is called before the first frame update
	public void clicked(){
		SceneManager.LoadScene("TutoGame");
	}
}
