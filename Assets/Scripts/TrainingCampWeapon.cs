
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrainingCampWeapon : MonoBehaviour
{
    public Camera playerCamera;
    public TrainingCampManager campManager;
    public ReloadTimeController reloadController;

    [Header("Shooting")]
    public float range = 100f;
    public float fireCooldown = 0.25f;
    public float tracerDuration = 0.08f;
    public float tracerWidth = 0.025f;

    [Header("Grenade")]
    public GrenadeMovement grenade;
    public KeyCode grenadeKey = KeyCode.G;

    private bool isReloading;
    private float nextFireTime;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();

        if (campManager == null)
            campManager = FindFirstObjectByType<TrainingCampManager>();

        if (reloadController == null)
            reloadController = FindFirstObjectByType<ReloadTimeController>();
    }

    private void Update()
    {
        // Don't process gameplay controls while paused.
        if (Time.timeScale == 0f || isReloading)
            return;

        // Shoot only when the mouse is NOT over UI.
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current == null ||
                !EventSystem.current.IsPointerOverGameObject())
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reload());

        if (Input.GetKeyDown(grenadeKey))
            ThrowGrenade();
    }

    private void ThrowGrenade()
    {
        if (grenade == null)
        {
            Debug.LogWarning("Grenade is not assigned!");
            return;
        }

        if (playerCamera == null)
        {
            Debug.LogWarning("Player camera is missing!");
            return;
        }

        grenade.Throw(playerCamera.transform.forward);
    }

    private void Shoot()
    {
        if (Time.time < nextFireTime || campManager == null)
            return;

        if (campManager.gunAmmo <= 0)
        {
            Debug.Log("Out of ammo. Press R to reload.");
            return;
        }

        if (playerCamera == null)
        {
            Debug.LogError("TrainingCampWeapon: Player Camera is missing.");
            return;
        }

        campManager.gunAmmo--;
        campManager.UpdateUI();
        nextFireTime = Time.time + fireCooldown;

        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;
        Vector3 endPoint = origin + direction * range;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
        {
            endPoint = hit.point;
            Debug.Log("Shot hit: " + hit.collider.name);
        }

        StartCoroutine(ShowTracer(origin + direction * 0.5f, endPoint));
    }

    private IEnumerator ShowTracer(Vector3 start, Vector3 end)
    {
        GameObject tracer = new GameObject("ShotTracer");
        LineRenderer line = tracer.AddComponent<LineRenderer>();

        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.startWidth = tracerWidth;
        line.endWidth = tracerWidth;
        line.useWorldSpace = true;

        Shader shader = Shader.Find("Sprites/Default");

        if (shader != null)
        {
            line.material = new Material(shader);
            line.startColor = Color.yellow;
            line.endColor = Color.red;
        }

        yield return new WaitForSecondsRealtime(tracerDuration);

        Destroy(tracer);
    }

    private IEnumerator Reload()
    {
        if (isReloading || campManager == null)
            yield break;

        if (campManager.gunAmmo >= campManager.maxAmmos)
            yield break;

        isReloading = true;

        float reloadTime = reloadController != null
            ? reloadController.CurrentReloadTime
            : 2f;

        yield return new WaitForSeconds(reloadTime);

        campManager.gunAmmo = campManager.maxAmmos;
        campManager.UpdateUI();

        isReloading = false;
    }
}
