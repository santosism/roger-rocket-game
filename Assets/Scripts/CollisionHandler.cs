using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f; 
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip landingFinish;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

    }

    
    
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

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
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(landingFinish);
        successParticles.Play();

        // todo add SFX upon crash
        // todo add particle effect upon success
        GetComponent<Movement>().enabled = false; 
        Invoke("LoadNextLevel", levelLoadDelay);

    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        // todo add SFX upon crash
        // todo add particle effect upon crash
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
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
