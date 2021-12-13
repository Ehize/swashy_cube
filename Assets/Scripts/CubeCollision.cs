using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeCollision : MonoBehaviour
{	
	Cube cube;
	public Player player;
	public AudioClip splashSound;

	

	void Start()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	private void Awake() {
		cube = GetComponent<Cube>();
	}
	   
    private void OnCollisionEnter(Collision collision) {
		Cube otherCube = collision.gameObject.GetComponent<Cube>();
    
	//checks if in contact with other cubes
	if (otherCube != null && cube.CubeID > otherCube.CubeID) {
		
		//checks if both cubes have the same number
		if(cube.CubeNumber == otherCube.CubeNumber) {
		  Debug.Log("HIT : " + cube.CubeNumber);

        Vector3 contactPoint = collision.contacts[0].point;
	
		//checks if cube number is less than maximum in spawnmanager:
		if(otherCube.CubeNumber < SpawnManager.Instance.maxCubeNumber){
			//spawn a new cube
			Cube newCube = SpawnManager.Instance.Spawn(cube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f);
			//push the new cube up and forward
			float pushForce = 2.5f;
			newCube.CubeRigidbody.AddForce(new Vector3(0, .3f, 1f) * pushForce, ForceMode.Impulse);
		
		//torque
		float randomValue = Random.Range(-20f, 20f);
		Vector3 randomDirection = Vector3.one * randomValue;
		newCube.CubeRigidbody.AddTorque(randomDirection);
	   }
	   
	   //Let the expolosion affect surrounding cubes too
	   Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
	   float explosionForce = 400f;
	   float explosionRadius = 1.5f;
	   
	   
	   foreach(Collider coll in surroundedCubes) {
		   if(coll.attachedRigidbody != null)
			  coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
	   }
	   //explosion FX
	   Explosion.Instance.PlayCubeExplosionFX (contactPoint, cube.CubeColor);
	   player.playerAudio.PlayOneShot(splashSound, 1.0f);
	  
	   
	   //Destroy the two cubes
	   SpawnManager.Instance.DestroyCube(cube);
	   SpawnManager.Instance.DestroyCube(otherCube);
	   player.UpdateScore(10);
	 }
     
	 //Reduce score if any cube collide with non-identical match
	 if(cube.CubeNumber != otherCube.CubeNumber) {
       player.UpdateScore(-10);
	 } else {
	   player.UpdateScore(100);
	 }
   }

   
  }

}
