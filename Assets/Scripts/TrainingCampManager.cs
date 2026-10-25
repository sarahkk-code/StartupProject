
using UnityEngine;
using TMPro;

public class TrainingCampManager : MonoBehaviour
{
    [Header("Ammo")]
    public int gunAmmo = 10;
    public int baseMaxAmmos = 10;
    public int maxAmmos = 10;
    public TMP_Text ammoText;

    [Header("Health")]
    public int health = 100;
    public TMP_Text healthText;

    private void Start()
    {
        UpdateUI();
    }

    public void SetAmmoMultiplier(int index)
    {
        int multiplier = index switch
        {
            0 => 2,
            1 => 5,
            2 => 10,
            _ => 2
        };

        maxAmmos = baseMaxAmmos * multiplier;
        gunAmmo = Mathf.Min(gunAmmo, maxAmmos);

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (ammoText != null)
            ammoText.text = "Ammo: " + gunAmmo + " / " + maxAmmos;

        if (healthText != null)
            healthText.text = "Health: " + health;
    }
}
