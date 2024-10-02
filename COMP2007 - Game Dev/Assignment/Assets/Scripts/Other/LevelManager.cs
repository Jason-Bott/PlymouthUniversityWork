using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int labyrinthWidth;
    public static int labyrinthHeight;
    public int level;

    public void SelectLevelChanged(int value)
    {
        //When selected level changes change the values of labyrinth size and level to load
        //0 means nothing selected
        //-1 is the tutorial
        //Anything greater than 0 is the main labyrinth of the size specified
        Debug.Log(value);
        switch (value)
        {
            case 0:
                labyrinthWidth = 0;
                labyrinthHeight = 0;
                level = 0;
                break;
            case 1:
                labyrinthWidth = -1;
                labyrinthHeight = -1;
                level = -1;
                break;
            case 2:
                labyrinthWidth = 25;
                labyrinthHeight = 25;
                level = 25;
                break;
            case 3:
                labyrinthWidth = 35;
                labyrinthHeight = 35;
                level = 35;
                break;
            case 4:
                labyrinthWidth = 50;
                labyrinthHeight = 50;
                level = 50;
                break;
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public static int GetWidth()
    {
        return labyrinthWidth;
    }

    public static int GetHeight()
    {
        return labyrinthHeight;
    }

    void Awake()
    {
        //Destroy any other level managers
        if (FindObjectsOfType<LevelManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            //If only level manager left, make sure it doesn't get destroyed between scenes
            DontDestroyOnLoad(gameObject);
        }
    }
}
