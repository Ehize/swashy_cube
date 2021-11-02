using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DangerZone : MonoBehaviour
{
   public TextMeshProUGUI gameOverText;
   public Button restartButton;

//    public TouchSlider touchSlider;
   
//    void Start() 
//    {
// 	 touchSlider = GameObject.Find("Touch Slider").GetComponent<TouchSlider>();
//    }

   private void OnTriggerStay (Collider other) {
	   Cube cube = other.GetComponent<Cube>();
	   if(cube != null) {
		   if(!cube.IsMainCube && cube.CubeRigidbody.velocity.magnitude < .1f){
			   gameOverText.gameObject.SetActive(true);
			   restartButton.gameObject.SetActive(true);
			   Time.timeScale = 0;
			// Debug.Log("Game Over");
		   }
	   }
   }

   public void RestartGame() 
   {
	   Time.timeScale = 1;
	   SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
