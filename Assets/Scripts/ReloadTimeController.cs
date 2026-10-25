
using UnityEngine;
using TMPro;

public class ReloadTimeController : MonoBehaviour
{
    public TMP_Dropdown reloadDropdown;

    // Base reload time in seconds.
    public float baseReloadTime = 2f;

    public float CurrentReloadTime { get; private set; }

    private void Awake()
    {
        if (reloadDropdown == null)
            reloadDropdown = GetComponent<TMP_Dropdown>();

        if (reloadDropdown == null)
        {
            Debug.LogError("ReloadTimeController: Dropdown not assigned!");
            enabled = false;
            return;
        }

        reloadDropdown.ClearOptions();
        reloadDropdown.AddOptions(new System.Collections.Generic.List<string>
        {
            "Short (0.5x)",
            "Medium (1x)",
            "Long (2x)"
        });

        reloadDropdown.onValueChanged.AddListener(OnReloadTimeChanged);
        ApplyReloadTime(reloadDropdown.value);
    }

    private void OnReloadTimeChanged(int index)
    {
        ApplyReloadTime(index);
    }

    private void ApplyReloadTime(int index)
    {
        float multiplier = index switch
        {
            0 => 0.5f,
            1 => 1f,
            2 => 2f,
            _ => 1f
        };

        CurrentReloadTime = baseReloadTime * multiplier;

        Debug.Log("Reload time: " + CurrentReloadTime + " seconds");
    }

    private void OnDestroy()
    {
        if (reloadDropdown != null)
            reloadDropdown.onValueChanged.RemoveListener(OnReloadTimeChanged);
    }
}
