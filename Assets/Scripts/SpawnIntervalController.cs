
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnIntervalController : MonoBehaviour
{
    public Slider spawnIntervalSlider;
    public TMP_Text spawnIntervalLabel;

    public float SpawnInterval
    {
        get
        {
            if (spawnIntervalSlider == null)
                return 1f;

            return Mathf.Round(spawnIntervalSlider.value);
        }
    }

    private void Awake()
    {
        if (spawnIntervalSlider == null)
        {
            spawnIntervalSlider = GetComponent<Slider>();
        }

        if (spawnIntervalSlider == null)
        {
            Debug.LogError("SpawnIntervalController: No Slider assigned!");
            enabled = false;
            return;
        }

        spawnIntervalSlider.minValue = 1f;
        spawnIntervalSlider.maxValue = 10f;
        spawnIntervalSlider.wholeNumbers = true;

        spawnIntervalSlider.onValueChanged.AddListener(OnIntervalChanged);
        OnIntervalChanged(spawnIntervalSlider.value);
    }

    private void OnIntervalChanged(float value)
    {
        int interval = Mathf.Clamp(Mathf.RoundToInt(value), 1, 10);

        if (!Mathf.Approximately(spawnIntervalSlider.value, interval))
        {
            spawnIntervalSlider.SetValueWithoutNotify(interval);
        }

        if (spawnIntervalLabel != null)
        {
            spawnIntervalLabel.text = "Spawn Interval: " + interval + " seconds";
        }

        Debug.Log("Spawn Interval: " + interval + " seconds");
    }

    private void OnDestroy()
    {
        if (spawnIntervalSlider != null)
        {
            spawnIntervalSlider.onValueChanged.RemoveListener(OnIntervalChanged);
        }
    }
}
