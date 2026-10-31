
using UnityEngine;
using TMPro;

public class LevelRequirementController : MonoBehaviour
{
    public TMP_Dropdown levelRequirementDropdown;
    public GameManager gameManager;

    public int CollectiblesRequired
    {
        get
        {
            if (levelRequirementDropdown == null)
                return 3;

            switch (levelRequirementDropdown.value)
            {
                case 0: return 3;
                case 1: return 5;
                case 2: return 10;
                default: return 3;
            }
        }
    }

    private void Awake()
    {
        if (levelRequirementDropdown == null)
            levelRequirementDropdown = GetComponent<TMP_Dropdown>();

        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();

        if (levelRequirementDropdown == null || gameManager == null)
        {
            Debug.LogError(
                "LevelRequirementController: Dropdown or GameManager missing!");
            enabled = false;
            return;
        }

        levelRequirementDropdown.onValueChanged.AddListener(
            OnRequirementChanged);
    }

    private void Start()
    {
        ApplyRequirement();
    }

    private void OnRequirementChanged(int index)
    {
        ApplyRequirement();
    }

    private void ApplyRequirement()
    {
        int target = CollectiblesRequired;
        gameManager.SetCollectiblesRequired(target);

        Debug.Log("Collectibles required: " + target);
    }

    private void OnDestroy()
    {
        if (levelRequirementDropdown != null)
        {
            levelRequirementDropdown.onValueChanged.RemoveListener(
                OnRequirementChanged);
        }
    }
}
