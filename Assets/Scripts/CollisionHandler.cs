using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            
            case "Friendly":
                Debug.Log("You bumped into a friendly one!");
                break;

            case "Finish":
                Debug.Log("You bumped onto the Landing Pad, finished!");
                break;

            case "Fuel":
                Debug.Log("In space, no one can hear you pick up even more jet fuel...maybe");
                break;

            default:
                ReloadLevel();
                break; 

        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }

}
