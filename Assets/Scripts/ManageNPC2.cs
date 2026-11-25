
using UnityEngine;

public class ManageNPC2 : MonoBehaviour
{
    Animator anim;
    float health = 100;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public float getHealth()
    {
        return health;
    }

    public void setHealth(float newHealth)
    {
        health = newHealth;
        anim.SetFloat("health", health);
    }

    void Update()
    {
        setHealth(health - Time.deltaTime * 0.1f); if (health < 0)
            setHealth(0);
    }
}
