using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour
{

    [SerializeField] float crashSequenceDuration = 2f;
    [SerializeField] float nextLevelDelay = 1f;

    [SerializeField] AudioClip success;
    [SerializeField] AudioClip failure;

    AudioSource audioSource;


    bool isAlive;

    private void Start()
    {
        isAlive = true;
        audioSource = GetComponent<AudioSource>();
    }





    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hit Friendly");
                break;
            case "Finish":
                Debug.Log("Finished level");
                if (isAlive) startFinishSequence();
                // Invoke("loadNextLevel", nextLevelDelay);
                break;
            case "Fuel":
                Debug.Log("Hit Fuel");
                break;
            default:
                if (isAlive) startCrashSequence();
                break;
        }
    }

    void startCrashSequence()
    {
        disableMovement();
        audioSource.PlayOneShot(failure);
        Invoke("RestartLevel", crashSequenceDuration);
    }

    void startFinishSequence()
    {

        disableMovement();
        audioSource.PlayOneShot(success);
        Invoke("loadNextLevel", 1f);
    }

    void disableMovement()
    {
        isAlive = false;
        GetComponent<AudioSource>().Stop();
        GetComponent<RocketMovement>().enabled = false;
    }

    void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void loadNextLevel()
    {
        disableMovement();
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }


}
