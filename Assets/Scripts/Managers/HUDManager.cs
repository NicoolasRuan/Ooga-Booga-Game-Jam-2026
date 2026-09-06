using System.IO.IsolatedStorage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }


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
        DontDestroyOnLoad(gameObject);
    }

    public TextMeshProUGUI metersIndicator;
    public Transform cameraHeigth;
    public Image[] waterDrops;

    private void Update()
    {
        metersIndicator.text = Mathf.FloorToInt(cameraHeigth.position.y).ToString() + "m";
    }

    public void UpdateWaterCharges(int charges)
    {
        for (int i = 0; i < waterDrops.Length; i++)
        {
            waterDrops[i].gameObject.SetActive(i < charges);
        }
    }
}
