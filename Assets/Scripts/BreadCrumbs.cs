
using UnityEngine;

public class BreadCrumbs : MonoBehaviour
{
    Vector3 previousPosition;
    float counter = 0;

    public GameObject BC;

    void Start()
    {
        previousPosition = gameObject.transform.position;
    }

    void Update()
    {
        Vector3 currentLocation = gameObject.transform.position;

        float distance = Vector3.Distance(
            previousPosition, currentLocation
        );

        if (distance > 1.0f)
        {
            previousPosition = currentLocation;

            GameObject g = Instantiate(
                BC, currentLocation, Quaternion.identity
            );

            g.name = "BC" + counter;
            counter++;
        }
    }
}
