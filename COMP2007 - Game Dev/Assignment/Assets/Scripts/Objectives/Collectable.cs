using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    Objective,
    Weapon,
    Loot,
}

public class Collectable : MonoBehaviour, ICollectable
{
    public CollectableType type;

    public void Collect(GameObject player)
    {
        if (type == CollectableType.Objective)
        {
            player.GetComponent<ObjectiveManager>().NextObjective();
        }
        Destroy(gameObject);
    }
}
