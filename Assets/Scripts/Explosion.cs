using UnityEngine;

public class Explosion : MonoBehaviour
{
  
   
   [SerializeField] private ParticleSystem cubeExplosionFX ;

   ParticleSystem.MainModule cubeExplosionFXMainModule ;
	//singletonclass
	public static Explosion Instance;
	
	private void Awake() {
		Instance = this;
	}
	
	private void Start() {
		cubeExplosionFXMainModule = cubeExplosionFX.main;
	}
	
	public void PlayCubeExplosionFX(Vector3 position, Color color) {
		cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(color);
		cubeExplosionFX.transform.position = position;
		cubeExplosionFX.Play();
	}
}
