using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class tutoButton : MonoBehaviour
{
	public void clicked(){
		SceneManager.LoadScene("MainGame");
	}
}
