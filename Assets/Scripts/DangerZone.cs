using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DangerZone : MonoBehaviour
{
   public GameObject gameOverPanel;
   public TextMeshProUGUI scoreText;
   public TextMeshProUGUI highScoreText;
//    public Button mainMenuButton;
//    public Button retryButton;

   public AudioClip gameOverSound;

   public Player player;

   void Start() 
   {
	   player = GameObject.Find("Player").GetComponent<Player>();
       highScoreText.text = "Best Score\n" + PlayerPrefs.GetInt("HighScore", 0);
   }


   void Update() 
   {
       scoreText.text = player.score.ToString();
   }

//    public TouchSlider touchSlider;
   
//    void Start() 
//    {
// 	 touchSlider = GameObject.Find("Touch Slider").GetComponent<TouchSlider>();
//    }

   private void OnTriggerStay (Collider other) {
	   Cube cube = other.GetComponent<Cube>();
	   if(cube != null) {
		   if(!cube.IsMainCube && cube.CubeRigidbody.velocity.magnitude < .1f) {
			   player.playerAudio.PlayOneShot(gameOverSound, 1.0f);
               scoreText.gameObject.SetActive(false);
               highScoreText.gameObject.SetActive(true);
			   gameOverPanel.SetActive(true);
			//    mainMenuButton.gameObject.SetActive(true);
			//    retryButton.gameObject.SetActive(true);
			   Time.timeScale = 0;
			// Debug.Log("Game Over");

            if(player.score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", player.score);
            }
            player.score = 0;
		   }
	   }
   }

   public void RestartGame() 
   {
	   Time.timeScale = 1;
	   SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

   public void QuitGame() {
        Application.Quit();
        Debug.Log("Quit");
   }

   public void MainMenu() 
   {
	   SceneManager.LoadScene(1);
   }
}
