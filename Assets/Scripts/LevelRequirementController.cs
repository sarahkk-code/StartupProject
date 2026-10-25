
using UnityEngine;
using TMPro;

public class LevelRequirementController : MonoBehaviour
{
    public TMP_Dropdown levelRequirementDropdown;

    public int CollectiblesRequired
    {
        get
        {
            if (levelRequirementDropdown == null)
                return 3;

            switch (levelRequirementDropdown.value)
            {
                case 0:
                    return 3;

                case 1:
                    return 5;

                case 2:
                    return 10;

                default:
                    return 3;
            }
        }
    }

    private void Awake()
    {
        if (levelRequirementDropdown == null)
        {
            levelRequirementDropdown = GetComponent<TMP_Dropdown>();
        }

        if (levelRequirementDropdown != null)
        {
            levelRequirementDropdown.onValueChanged.AddListener(OnRequirementChanged);
            OnRequirementChanged(levelRequirementDropdown.value);
        }
        else
        {
            Debug.LogError("LevelRequirementController: No dropdown assigned!");
        }
    }

    private void OnRequirementChanged(int index)
    {
        Debug.Log("Collectibles required for next level: " + CollectiblesRequired);
    }

    private void OnDestroy()
    {
        if (levelRequirementDropdown != null)
        {
            levelRequirementDropdown.onValueChanged.RemoveListener(OnRequirementChanged);
        }
    }
}
