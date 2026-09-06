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
        //DontDestroyOnLoad(gameObject);
    }

    public TextMeshProUGUI metersIndicator;
    [SerializeField] private float metersPerUnit = 0f;
    public Transform cameraHeigth;
    public Image[] waterDrops;

    public GameObject buttonObj;

    private float startHeight;

    private void Start()
    {
        buttonObj.SetActive(false);
        startHeight = cameraHeigth.position.y;
    }

    private void Update()
    {
        float traveledHeight = cameraHeigth.position.y - startHeight;

        int height = Mathf.FloorToInt(traveledHeight * metersPerUnit);
        //int height = Mathf.FloorToInt(cameraHeigth.position.y * metersPerUnit);
        metersIndicator.text = height.ToString() + "m";

        if(GameManager.Instance.isGameOver)
        {
            buttonObj.SetActive(true);
        }
    }

    public void UpdateWaterCharges(int charges)
    {
        for (int i = 0; i < waterDrops.Length; i++)
        {
            waterDrops[i].gameObject.SetActive(i < charges);
        }
    }
}
