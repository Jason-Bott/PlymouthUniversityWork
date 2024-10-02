using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType< LevelManager>();
        Debug.Log(levelManager);
    }

    public void Level(int value)
    {
        levelManager.SelectLevelChanged(value);
    }
}
