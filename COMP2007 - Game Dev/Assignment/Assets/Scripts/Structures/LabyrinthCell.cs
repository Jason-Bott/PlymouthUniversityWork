using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCell : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private GameObject _frontWall;

    [SerializeField]
    private GameObject _backWall;

    [SerializeField]
    private GameObject _leftWall1;

    [SerializeField]
    private GameObject _rightWall1;

    [SerializeField]
    private GameObject _frontWall1;

    [SerializeField]
    private GameObject _backWall1;

    [SerializeField]
    private GameObject _leftWall2;

    [SerializeField]
    private GameObject _rightWall2;

    [SerializeField]
    private GameObject _frontWall2;

    [SerializeField]
    private GameObject _backWall2;

    [SerializeField]
    private GameObject _leftWall3;

    [SerializeField]
    private GameObject _rightWall3;

    [SerializeField]
    private GameObject _frontWall3;

    [SerializeField]
    private GameObject _backWall3;

    [SerializeField]
    private GameObject _leftWallDown1;

    [SerializeField]
    private GameObject _rightWallDown1;

    [SerializeField]
    private GameObject _frontWallDown1;

    [SerializeField]
    private GameObject _backWallDown1;

    [SerializeField]
    private GameObject _leftWallDown2;

    [SerializeField]
    private GameObject _rightWallDown2;

    [SerializeField]
    private GameObject _frontWallDown2;

    [SerializeField]
    private GameObject _backWallDown2;

    [SerializeField]
    private GameObject _leftWallDown3;

    [SerializeField]
    private GameObject _rightWallDown3;

    [SerializeField]
    private GameObject _frontWallDown3;

    [SerializeField]
    private GameObject _backWallDown3;

    [SerializeField]
    private GameObject _Block;

    [SerializeField]
    private GameObject _Ceiling;

    [SerializeField]
    private GameObject _Floor;

    public bool IsVisited { get; private set; }

    //Set cell as visited
    public void Visit()
    {
        IsVisited = true;
        _Block.SetActive(false);
    }

    //Remove left wall on main floor
    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
    }

    //Remove right wall on main floor
    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
    }

    //Remove front wall on main floor
    public void ClearFrontWall()
    {
        _frontWall.SetActive(false);
    }

    //Remove back wall on main floor
    public void ClearBackWall()
    {
        _backWall.SetActive(false);
    }

    //Remove cells ceiling
    public void ClearCeiling()
    {
        _Ceiling.SetActive(false);
    }


    //Remove cells floor
    public void ClearFloor()
    {
        _Floor.SetActive(false);
    }

    //Add left wall 1 floor up
    public void InsertLeftWall1()
    {
        _leftWall1.SetActive(true);
    }

    //Add right wall 1 floor up
    public void InsertRightWall1()
    {
        _rightWall1.SetActive(true);
    }

    //Add front wall 1 floor up
    public void InsertFrontWall1()
    {
        _frontWall1.SetActive(true);
    }

    //Add back wall 1 floor up
    public void InsertBackWall1()
    {
        _backWall1.SetActive(true);
    }

    //Remove left wall 1 floor up
    public void ClearLeftWall1()
    {
        _leftWall1.SetActive(false);
    }

    //Remove right wall 1 floor up
    public void ClearRightWall1()
    {
        _rightWall1.SetActive(false);
    }

    //Remove front wall 1 floor up
    public void ClearFrontWall1()
    {
        _frontWall1.SetActive(false);
    }

    //Remove back wall 1 floor up
    public void ClearBackWall1()
    {
        _backWall1.SetActive(false);
    }

    public void InsertLeftWall2()
    {
        _leftWall2.SetActive(true);
    }

    public void InsertRightWall2()
    {
        _rightWall2.SetActive(true);
    }

    public void InsertFrontWall2()
    {
        _frontWall2.SetActive(true);
    }

    public void InsertBackWall2()
    {
        _backWall2.SetActive(true);
    }

    public void ClearLeftWall2()
    {
        _leftWall2.SetActive(false);
    }

    public void ClearRightWall2()
    {
        _rightWall2.SetActive(false);
    }

    public void ClearFrontWall2()
    {
        _frontWall2.SetActive(false);
    }

    public void ClearBackWall2()
    {
        _backWall2.SetActive(false);
    }

    public void InsertLeftWall3()
    {
        _leftWall3.SetActive(true);
    }

    public void InsertRightWall3()
    {
        _rightWall3.SetActive(true);
    }

    public void InsertFrontWall3()
    {
        _frontWall3.SetActive(true);
    }

    public void InsertBackWall3()
    {
        _backWall3.SetActive(true);
    }

    public void ClearLeftWall3()
    {
        _leftWall3.SetActive(false);
    }

    public void ClearRightWall3()
    {
        _rightWall3.SetActive(false);
    }

    public void ClearFrontWall3()
    {
        _frontWall3.SetActive(false);
    }

    public void ClearBackWall3()
    {
        _backWall3.SetActive(false);
    }

    public void InsertLeftWallDown1()
    {
        _leftWallDown1.SetActive(true);
    }

    public void InsertRightWallDown1()
    {
        _rightWallDown1.SetActive(true);
    }

    public void InsertFrontWallDown1()
    {
        _frontWallDown1.SetActive(true);
    }

    public void InsertBackWallDown1()
    {
        _backWallDown1.SetActive(true);
    }

    public void ClearLeftWallDown1()
    {
        _leftWallDown1.SetActive(false);
    }

    public void ClearRightWallDown1()
    {
        _rightWallDown1.SetActive(false);
    }

    public void ClearFrontWallDown1()
    {
        _frontWallDown1.SetActive(false);
    }

    public void ClearBackWallDown1()
    {
        _backWallDown1.SetActive(false);
    }

    public void InsertLeftWallDown2()
    {
        _leftWallDown2.SetActive(true);
    }

    public void InsertRightWallDown2()
    {
        _rightWallDown2.SetActive(true);
    }

    public void InsertFrontWallDown2()
    {
        _frontWallDown2.SetActive(true);
    }

    public void InsertBackWallDown2()
    {
        _backWallDown2.SetActive(true);
    }

    public void ClearLeftWallDown2()
    {
        _leftWallDown2.SetActive(false);
    }

    public void ClearRightWallDown2()
    {
        _rightWallDown2.SetActive(false);
    }

    public void ClearFrontWallDown2()
    {
        _frontWallDown2.SetActive(false);
    }

    public void ClearBackWallDown2()
    {
        _backWallDown2.SetActive(false);
    }

    public void InsertLeftWallDown3()
    {
        _leftWallDown3.SetActive(true);
    }

    public void InsertRightWallDown3()
    {
        _rightWallDown3.SetActive(true);
    }

    public void InsertFrontWallDown3()
    {
        _frontWallDown3.SetActive(true);
    }

    public void InsertBackWallDown3()
    {
        _backWallDown3.SetActive(true);
    }

    public void ClearLeftWallDown3()
    {
        _leftWallDown3.SetActive(false);
    }

    public void ClearRightWallDown3()
    {
        _rightWallDown3.SetActive(false);
    }

    public void ClearFrontWallDown3()
    {
        _frontWallDown3.SetActive(false);
    }

    public void ClearBackWallDown3()
    {
        _backWallDown3.SetActive(false);
    }
}