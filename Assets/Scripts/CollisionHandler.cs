using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f; 
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip landingFinish;

    AudioSource audioSource;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

    }

    
    
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            
            case "Friendly":
                Debug.Log("Friendly jump!");
                break;
            case "Finish":
                StartSuccessSequence();                 
                break;
            default:
                StartCrashSequence();
                break; 

        }
    }

    void StartSuccessSequence() 
    {
        audioSource.PlayOneShot(landingFinish);

        // todo add SFX upon crash
        // todo add particle effect upon success
        GetComponent<Movement>().enabled = false; 
        Invoke("LoadNextLevel", levelLoadDelay);

    }

    void StartCrashSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false; 
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
    }

}
