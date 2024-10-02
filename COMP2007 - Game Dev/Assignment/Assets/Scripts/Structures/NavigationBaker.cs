using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    public NavMeshSurface[] surfaces;

    public void Bake(LevelLoader levelLoader)
    {
        //Remove any nav mesh data currently loaded
        surfaces[0].RemoveData();
        //Build nav mesh from first cell
        surfaces[0].BuildNavMesh();
        //Tell level loader this is done
        levelLoader.Done();
    }
}