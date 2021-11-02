using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	[SerializeField] private float pushForce;
	[SerializeField] private float cubeMaxPosX;
	[Space]
	[SerializeField] private TouchSlider touchSlider;
	
	private Cube mainCube;
	
	private bool isPointerDown;
	private Vector3 cubePos;

	public AudioSource playerAudio;
	
    // Start is called before the first frame update
    private void Start()
    {
        //Get audioSource component
		playerAudio=GetComponent<AudioSource>();

		//Spwan new cube
		SpawnCube();
		
		//listen to slider events:
        touchSlider.OnPointerDownEvent += OnPointerDown;
		touchSlider.OnPointerDragEvent += OnPointerDrag;
		touchSlider.OnPointerUpEvent += OnPointerUp;
    }
	
	private void Update() {
		if(isPointerDown)
		  mainCube.transform.position = Vector3.Lerp (
	        mainCube.transform.position,
			cubePos,
			moveSpeed * Time.deltaTime
	  );
	  
	 
	}

    private void OnPointerDown () {
		isPointerDown = true;
	}
	
	private void OnPointerDrag (float xMovement) {
		if(isPointerDown){
			cubePos = mainCube.transform.position;
			cubePos.x = xMovement * cubeMaxPosX;
		}
	}
	
	private void OnPointerUp () {
		if(isPointerDown){
			isPointerDown = false;
			
			//Push the cube forward
			mainCube.CubeRigidbody.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
			
			Invoke("SpawnNewCube", 0.3f);
		}
	}
	
	private void SpawnNewCube() {
		mainCube.IsMainCube = false;
		SpawnCube();
	}
	
	private void SpawnCube() {
		mainCube = SpawnManager.Instance.SpawnRandom();
		mainCube.IsMainCube = true;
		//reset cubePos variable
		cubePos = mainCube.transform.position;
	}
	
	private void OnDestroy () {
		//remove listeners:
		touchSlider.OnPointerDownEvent -= OnPointerDown;
		touchSlider.OnPointerDragEvent -= OnPointerDrag;
		touchSlider.OnPointerUpEvent -= OnPointerUp;
	}
}
