using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // 2. Check if an instance already exists in the scene
        if (Instance != null && Instance != this)
        {
            // If a duplicate exists, destroy this game object to enforce the rule
            Destroy(gameObject);
            return;
        }

        // 3. Set this object as the official single instance
        Instance = this;

        // Optional: Keep this object alive when switching between scenes
        //DontDestroyOnLoad(gameObject);
    }

    public bool isGameOver;

    

    public void ReloadGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
