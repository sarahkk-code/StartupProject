
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class AssignmentTests
{
    [UnityTest]
    public IEnumerator OffmeshSceneLoads()
    {
        SceneManager.LoadScene("offmesh");
        yield return null;

        Assert.IsNotNull(GameObject.Find("FPSController"));
        Assert.IsNotNull(GameObject.Find("AIThirdPersonController"));
        Assert.IsNotNull(GameObject.Find("ground"));
    }

    [UnityTest]
    public IEnumerator OffmeshNPCHasNavigationAgent()
    {
        SceneManager.LoadScene("offmesh");
        yield return null;

        GameObject npc = GameObject.Find("AIThirdPersonController");

        Assert.IsNotNull(npc);
        Assert.IsNotNull(npc.GetComponent<UnityEngine.AI.NavMeshAgent>());
    }

    [UnityTest]
    public IEnumerator PathsSceneHasFixedPathNPC()
    {
        SceneManager.LoadScene("paths");
        yield return null;

        GameObject npc = GameObject.Find("NPC");

        Assert.IsNotNull(npc);
        Assert.IsNotNull(npc.GetComponent<UnityEngine.AI.NavMeshAgent>());
        Assert.IsNotNull(npc.GetComponent("NPCPathMovement"));
    }

    [UnityTest]
    public IEnumerator PathsSceneHasRandomNPC()
    {
        SceneManager.LoadScene("paths");
        yield return null;

        GameObject npc = GameObject.Find("RandomNPC");

        Assert.IsNotNull(npc);
        Assert.IsNotNull(npc.GetComponent<UnityEngine.AI.NavMeshAgent>());
        Assert.IsNotNull(npc.GetComponent("NPCRandomPath"));
    }
}
