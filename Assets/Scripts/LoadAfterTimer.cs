using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterTimer : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 4f;

    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         timeElapsed += Time.deltaTime;

        if (timeElapsed > delayBeforeLoading)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}
