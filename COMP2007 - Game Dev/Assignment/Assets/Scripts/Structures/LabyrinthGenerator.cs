using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LabyrinthGenerator : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [SerializeField]
    private GameObject _labyrinthGeneratorObject;

    [Header("Cell")]
    [SerializeField]
    private LabyrinthCell _labyrinthCellPrefab;

    [SerializeField]
    public int _labyrinthCellScale;

    [Header("Dimensions")]
    [SerializeField]
    public int _labyrinthWidth;

    [SerializeField]
    public int _labyrinthHeight;

    [Header("Structures")]
    [SerializeField]
    private GameObject _trooperShip;

    [SerializeField]
    private GameObject _powerRoom;

    [SerializeField]
    private GameObject _hangar;

    [SerializeField]
    private GameObject _droneSpawner;

    [Header("Enemies")]
    [SerializeField]
    private GameObject _drone;

    [SerializeField]
    private EnemyManager _enemyManager;

    private LabyrinthCell[,] _labyrinthGrid;

    private bool[,] _containsRoom;

    public void StartGeneration(LevelLoader levelLoader)
    {
        //Start generation routine
        StartCoroutine(Generation(levelLoader));
    }

    private IEnumerator Generation(LevelLoader levelLoader)
    {
        //Get navigation baker script and set its array
        NavigationBaker navBaker = _labyrinthGeneratorObject.GetComponent<NavigationBaker>();
        navBaker.surfaces = new NavMeshSurface[_labyrinthWidth * _labyrinthHeight];

        //Create array to contain labyrinth cell and bool to show if a cell contains a room
        _labyrinthGrid = new LabyrinthCell[_labyrinthWidth, _labyrinthHeight];
        _containsRoom = new bool[_labyrinthWidth, _labyrinthHeight];

        //For each row
        for(int x = 0; x < _labyrinthWidth; x++)
        {
            //For each cell in that row
            for(int z = 0; z < _labyrinthHeight; z++)
            {
                //Spawn the cell
                _labyrinthGrid[x, z] = Instantiate(_labyrinthCellPrefab, new Vector3(x * _labyrinthCellScale, 0, z * _labyrinthCellScale), Quaternion.identity);
                //Set its room containing boolean to false
                _containsRoom[x, z] = false;
                //Add nav mesh to nav mesh array
                navBaker.surfaces[x * _labyrinthHeight + z] = _labyrinthGrid[x, z].GetComponentInChildren<NavMeshSurface>();

                //Tell level loader another cell has been added
                levelLoader.BuildingShip();
                yield return null;
            }
        }

        //Tell level loader the labyrinth paths are being generated
        levelLoader.MakingLabyrinth();
        //Generate the paths
        GenerateLabyrinth(null, _labyrinthGrid[0,0]);
        yield return null;

        //Tell level loader the rooms are being added
        levelLoader.MakingRooms();
        //Add rooms
        AddRooms();
        yield return null;

        //Tell level load the nav mesh is being baked
        levelLoader.Baking();
        //Bake
        navBaker.Bake(levelLoader);
        yield return null;
    }

    private void GenerateLabyrinth(LabyrinthCell previousCell, LabyrinthCell currentCell)
    {
        //Visit current cell and clear wall between this cell and the last
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        LabyrinthCell nextCell;
        
        //While there is an unvisited cell next to this one path towards it
        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateLabyrinth(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private LabyrinthCell GetNextUnvisitedCell(LabyrinthCell currentCell)
    {
        //Get unvisited cells
        var unvisitedCells = GetUnvisitedCells(currentCell);

        //Return in random order
        return unvisitedCells.OrderBy(_ => UnityEngine.Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<LabyrinthCell> GetUnvisitedCells(LabyrinthCell currentCell)
    {
        //Position of current cell
        int x = (int)currentCell.transform.position.x / _labyrinthCellScale;
        int z = (int)currentCell.transform.position.z / _labyrinthCellScale;

        //Make sure there is a cell to the right and if unvisited return it as unvisited
        if(x + 1 < _labyrinthWidth)
        {
            var cellToRight = _labyrinthGrid[x + 1, z];

            if(cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        //Make sure there is a cell to the left and if unvisited return it as unvisited
        if (x - 1 >= 0)
        {
            var cellToLeft = _labyrinthGrid[x - 1, z];

            if(cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        //Make sure there is a cell forward and if unvisited return it as unvisited
        if (z + 1 < _labyrinthHeight)
        {
            var cellToFront = _labyrinthGrid[x, z + 1];

            if(cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        //Make sure there is a cell backward and if unvisited return it as unvisited
        if (z - 1 >= 0)
        {
            var cellToBack = _labyrinthGrid[x, z - 1];

            if(cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(LabyrinthCell previousCell, LabyrinthCell currentCell)
    {
        //If no previous cell return without doing anything
        if(previousCell ==  null)
        {
            return;
        }

        //Find relative positions and clear relevant walls
        if(previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }
        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }
        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }
        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

    void AddRooms()
    {
        //Add spawn ship
        AddTrooperShip();
        //Add power room (Main Objective)
        AddPowerRoom();
        //Add a hangar
        AddHangar();
        //Add drone spawners
        AddDroneSpawners();
    }

    void AddHangar()
    {
        if (_labyrinthHeight > 20 && _labyrinthWidth > 20)
        {
            int side = UnityEngine.Random.Range(1, 5);
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            int tile = 0;
            Vector3 position = Vector3.zero;

            switch (side)
            {
                case 1:
                    rotation = Quaternion.Euler(0, 270, 0);
                    tile = UnityEngine.Random.Range(10, _labyrinthWidth - 1);
                    position = new Vector3(tile * _labyrinthCellScale, 0, (_labyrinthHeight - 5) * _labyrinthCellScale);
                    break;
                case 2:
                    rotation = Quaternion.Euler(0, 0, 0);
                    tile = UnityEngine.Random.Range(1, _labyrinthHeight - 10);
                    position = new Vector3((_labyrinthWidth - 5) * _labyrinthCellScale, 0, tile * _labyrinthCellScale);
                    break;
                case 3:
                    rotation = Quaternion.Euler(0, 90, 0);
                    tile = UnityEngine.Random.Range(1, _labyrinthWidth - 10);
                    position = new Vector3(tile * _labyrinthCellScale, 0, 4 * _labyrinthCellScale);
                    break;
                case 4:
                    rotation = Quaternion.Euler(0, 180, 0);
                    tile = UnityEngine.Random.Range(10, _labyrinthHeight - 1);
                    position = new Vector3(4 * _labyrinthCellScale, 0, tile * _labyrinthCellScale);
                    break;
            }

            Instantiate(_hangar, position, rotation);

            switch (side)
            {
                case 1:
                    RemoveWalls(tile - 9, tile, _labyrinthHeight - 5, _labyrinthHeight - 1);
                    RemoveCeiling(tile - 9, tile, _labyrinthHeight - 5, _labyrinthHeight - 1);
                    AddHeight(tile - 9, tile, _labyrinthHeight - 5, _labyrinthHeight - 1, 1);

                    for (int i = 0; i < 5; i++)
                    {
                        _labyrinthGrid[tile - i, _labyrinthHeight - 1].ClearFrontWall();
                        _labyrinthGrid[tile - i, _labyrinthHeight - 1].ClearFrontWall1();

                    }

                    AddRoomToBool(tile - 9, tile, _labyrinthHeight - 5, _labyrinthHeight - 1);
                    break;
                case 2:
                    RemoveWalls(_labyrinthWidth - 5, _labyrinthWidth - 1, tile, tile + 9);
                    RemoveCeiling(_labyrinthWidth - 5, _labyrinthWidth - 1, tile, tile + 9);
                    AddHeight(_labyrinthWidth - 5, _labyrinthWidth - 1, tile, tile + 9, 1);

                    for (int i = 0; i < 5; i++)
                    {
                        _labyrinthGrid[_labyrinthWidth - 1, tile + i].ClearRightWall();
                        _labyrinthGrid[_labyrinthWidth - 1, tile + i].ClearRightWall1();
                    }

                    AddRoomToBool(_labyrinthWidth - 5, _labyrinthWidth - 1, tile, tile + 9);
                    break;
                case 3:
                    RemoveWalls(tile, tile + 9, 0, 4);
                    RemoveCeiling(tile, tile + 9, 0, 4);
                    AddHeight(tile, tile + 9, 0, 4, 1);

                    for (int i = 0; i < 5; i++)
                    {
                        _labyrinthGrid[tile + i, 0].ClearBackWall();
                        _labyrinthGrid[tile + i, 0].ClearBackWall1();
                    }

                    AddRoomToBool(tile, tile + 9, 0, 4);
                    break;
                case 4:
                    RemoveWalls(0, 4, tile - 9, tile);
                    RemoveCeiling(0, 4, tile - 9, tile);
                    AddHeight(0, 4, tile - 9, tile, 1);

                    for (int i = 0; i < 5; i++)
                    {
                        _labyrinthGrid[0, tile - i].ClearLeftWall();
                        _labyrinthGrid[0, tile - i].ClearLeftWall1();
                    }

                    AddRoomToBool(0, 4, tile - 9, tile);
                    break;
            }
        }
    }

    void AddPowerRoom()
    {
        if(_labyrinthHeight > 10 && _labyrinthWidth > 10)
        {
            int x = (_labyrinthWidth / 2) - 5;
            int z = (_labyrinthHeight / 2) + 5;


            Instantiate(_powerRoom, new Vector3((x * _labyrinthCellScale), 0, ((z - 9) * _labyrinthCellScale)), Quaternion.identity);

            RemoveWalls(x, x + 9, z - 9, z);
            RemoveCeiling(x, x + 9, z - 9, z);
            RemoveFloor(x + 1, x + 8, z - 8, z - 1);
            AddHeight(x, x + 9, z - 9, z, 3);
            AddDepth(x + 1, x + 8, z - 8, z - 1, 3);
            AddRoomToBool(x, x + 9, z - 9, z);
        }
    }

    void AddTrooperShip()
    {
        Instantiate(_trooperShip, new Vector3(0, 0, 0), Quaternion.identity);
        _labyrinthGrid[0,0].ClearLeftWall();
        _containsRoom[0,0] = true;

        //Player positioning
        Vector3 spawnPoint = new Vector3(-5.5f, 3, 0);
        player.position = spawnPoint;
        player.rotation = Quaternion.Euler(0, 90, 0);
    }

    void AddDroneSpawners()
    {
        if(_labyrinthHeight > 15 && _labyrinthWidth > 15)
        {
            int numRoomsX = _labyrinthWidth / 10;
            int numRoomsZ = _labyrinthHeight / 10;

            _enemyManager.droneSpawners = new GameObject[numRoomsX * numRoomsZ];

            for(int x = 0;  x < numRoomsX; x++)
            {
                for(int z = 0; z < numRoomsZ; z++)
                {
                    bool canPlace = true;

                    if(x == numRoomsX / 2 && z == numRoomsZ / 2)
                    {
                        continue;
                    }

                    float posX = UnityEngine.Random.Range(0, 8) + (x * 10);
                    float posZ = UnityEngine.Random.Range(0, 7) + (z * 10);

                    for(int gridX = (int)posX; gridX <= posX + 2; gridX++)
                    {
                        for(int gridZ = (int)posZ; gridZ <= posZ + 3; gridZ++)
                        {
                            if (_containsRoom[gridX, gridZ])
                            {
                                canPlace = false;
                                continue;
                            }
                        }
                    }

                    if(canPlace)
                    {
                        AddRoomToBool((int)posX, (int)posX + 2, (int)posZ, (int)posZ + 3);
                        RemoveWalls((int)posX, (int)posX + 2, (int)posZ, (int)posZ + 3);
                        RemoveCeiling((int)posX, (int)posX + 2, (int)posZ, (int)posZ + 3);
                        AddHeight((int)posX, (int)posX + 2, (int)posZ, (int)posZ + 3, 1);

                        posX *= _labyrinthCellScale;
                        posZ *= _labyrinthCellScale;

                        Vector3 spawnPosition = new Vector3(posX, 0, posZ);
                        _enemyManager.droneSpawners[(x * numRoomsZ) + z] = Instantiate(_droneSpawner, spawnPosition, Quaternion.identity);
                    }
                }
            }
        }
    }

    void RemoveWalls(int lowerX, int higherX, int lowerZ, int higherZ)
    {
        for (int x = lowerX; x <= higherX; x++)
        {
            for (int z = lowerZ; z <= higherZ; z++)
            {
                if (x == lowerX)
                {
                    if (z == lowerZ)
                    {
                        _labyrinthGrid[x, z].ClearFrontWall();
                        _labyrinthGrid[x, z].ClearRightWall();
                    }
                    else if (z == higherZ)
                    {
                        _labyrinthGrid[x, z].ClearRightWall();
                        _labyrinthGrid[x, z].ClearBackWall();
                    }
                    else
                    {
                        _labyrinthGrid[x, z].ClearFrontWall();
                        _labyrinthGrid[x, z].ClearRightWall();
                        _labyrinthGrid[x, z].ClearBackWall();
                    }
                }
                else if (x == higherX)
                {
                    if (z == lowerZ)
                    {
                        _labyrinthGrid[x, z].ClearFrontWall();
                        _labyrinthGrid[x, z].ClearLeftWall();
                    }
                    else if (z == higherZ)
                    {
                        _labyrinthGrid[x, z].ClearBackWall();
                        _labyrinthGrid[x, z].ClearLeftWall();
                    }
                    else
                    {
                        _labyrinthGrid[x, z].ClearFrontWall();
                        _labyrinthGrid[x, z].ClearLeftWall();
                        _labyrinthGrid[x, z].ClearBackWall();
                    }
                }
                else
                {
                    if (z == lowerZ)
                    {
                        _labyrinthGrid[x, z].ClearFrontWall();
                        _labyrinthGrid[x, z].ClearLeftWall();
                        _labyrinthGrid[x, z].ClearRightWall();
                    }
                    else if (z == higherZ)
                    {
                        _labyrinthGrid[x, z].ClearBackWall();
                        _labyrinthGrid[x, z].ClearLeftWall();
                        _labyrinthGrid[x, z].ClearRightWall();
                    }
                    else
                    {
                        _labyrinthGrid[x, z].ClearFrontWall();
                        _labyrinthGrid[x, z].ClearBackWall();
                        _labyrinthGrid[x, z].ClearLeftWall();
                        _labyrinthGrid[x, z].ClearRightWall();
                    }
                }
            }
        }
    }

    void RemoveCeiling(int lowerX, int higherX, int lowerZ, int higherZ)
    {
        for (int x = lowerX; x <= higherX; x++)
        {
            for (int z = lowerZ; z <= higherZ; z++)
            {
                _labyrinthGrid[x, z].ClearCeiling();
            }
        }
    }

    void RemoveFloor(int lowerX, int higherX, int lowerZ, int higherZ)
    {
        for (int x = lowerX; x <= higherX; x++)
        {
            for (int z = lowerZ; z <= higherZ; z++)
            {
                _labyrinthGrid[x, z].ClearFloor();
            }
        }
    }

    void AddHeight(int lowerX, int higherX, int lowerZ, int higherZ, int height)
    {
        if (height == 3)
        {
            for (int x = lowerX; x <= higherX; x++)
            {
                _labyrinthGrid[x, lowerZ].InsertBackWall1();
                _labyrinthGrid[x, higherZ].InsertFrontWall1();

                _labyrinthGrid[x, lowerZ].InsertBackWall2();
                _labyrinthGrid[x, higherZ].InsertFrontWall2();

                _labyrinthGrid[x, lowerZ].InsertBackWall3();
                _labyrinthGrid[x, higherZ].InsertFrontWall3();
            }

            for (int z = lowerZ; z <= higherZ; z++)
            {
                _labyrinthGrid[lowerX, z].InsertLeftWall1();
                _labyrinthGrid[higherX, z].InsertRightWall1();

                _labyrinthGrid[lowerX, z].InsertLeftWall2();
                _labyrinthGrid[higherX, z].InsertRightWall2();

                _labyrinthGrid[lowerX, z].InsertLeftWall3();
                _labyrinthGrid[higherX, z].InsertRightWall3();
            }
        }
        else if (height == 2)
        {
            for (int x = lowerX; x <= higherX; x++)
            {
                _labyrinthGrid[x, lowerZ].InsertBackWall1();
                _labyrinthGrid[x, higherZ].InsertFrontWall1();

                _labyrinthGrid[x, lowerZ].InsertBackWall2();
                _labyrinthGrid[x, higherZ].InsertFrontWall2();
            }

            for (int z = lowerZ; z <= higherZ; z++)
            {
                _labyrinthGrid[lowerX, z].InsertLeftWall1();
                _labyrinthGrid[higherX, z].InsertRightWall1();

                _labyrinthGrid[lowerX, z].InsertLeftWall2();
                _labyrinthGrid[higherX, z].InsertRightWall2();
            }
        }
        else
        {
            for (int x = lowerX; x <= higherX; x++)
            {
                _labyrinthGrid[x, lowerZ].InsertBackWall1();
                _labyrinthGrid[x, higherZ].InsertFrontWall1();
            }

            for (int z = lowerZ; z <= higherZ; z++)
            {
                _labyrinthGrid[lowerX, z].InsertLeftWall1();
                _labyrinthGrid[higherX, z].InsertRightWall1();
            }
        }
    }

    void AddDepth(int lowerX, int higherX, int lowerZ, int higherZ, int depth)
    {
        if (depth == 3)
        {
            for (int x = lowerX; x <= higherX; x++)
            {
                _labyrinthGrid[x, lowerZ].InsertBackWallDown1();
                _labyrinthGrid[x, higherZ].InsertFrontWallDown1();

                _labyrinthGrid[x, lowerZ].InsertBackWallDown2();
                _labyrinthGrid[x, higherZ].InsertFrontWallDown2();

                _labyrinthGrid[x, lowerZ].InsertBackWallDown3();
                _labyrinthGrid[x, higherZ].InsertFrontWallDown3();
            }

            for (int z = lowerZ; z <= higherZ; z++)
            {
                _labyrinthGrid[lowerX, z].InsertLeftWallDown1();
                _labyrinthGrid[higherX, z].InsertRightWallDown1();

                _labyrinthGrid[lowerX, z].InsertLeftWallDown2();
                _labyrinthGrid[higherX, z].InsertRightWallDown2();

                _labyrinthGrid[lowerX, z].InsertLeftWallDown3();
                _labyrinthGrid[higherX, z].InsertRightWallDown3();
            }
        }
        else if (depth == 2)
        {
            for (int x = lowerX; x <= higherX; x++)
            {
                _labyrinthGrid[x, lowerZ].InsertBackWallDown1();
                _labyrinthGrid[x, higherZ].InsertFrontWallDown1();

                _labyrinthGrid[x, lowerZ].InsertBackWallDown2();
                _labyrinthGrid[x, higherZ].InsertFrontWallDown2();
            }

            for (int z = lowerZ; z <= higherZ; z++)
            {
                _labyrinthGrid[lowerX, z].InsertLeftWallDown1();
                _labyrinthGrid[higherX, z].InsertRightWallDown1();

                _labyrinthGrid[lowerX, z].InsertLeftWallDown2();
                _labyrinthGrid[higherX, z].InsertRightWallDown2();
            }
        }
        else
        {
            for (int x = lowerX; x <= higherX; x++)
            {
                _labyrinthGrid[x, lowerZ].InsertBackWallDown1();
                _labyrinthGrid[x, higherZ].InsertFrontWallDown1();
            }

            for (int z = lowerZ; z <= higherZ; z++)
            {
                _labyrinthGrid[lowerX, z].InsertLeftWallDown1();
                _labyrinthGrid[higherX, z].InsertRightWallDown1();
            }
        }
    }

    void AddRoomToBool(int lowerX, int higherX, int lowerZ, int higherZ)
    {
        for(int x = lowerX; x <= higherX; x++)
        {
            for(int z = lowerZ; z <= higherZ; z++)
            {
                _containsRoom[x, z] = true;
            }
        }
    }
}
