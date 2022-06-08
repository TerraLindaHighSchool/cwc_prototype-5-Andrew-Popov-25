using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//List of GameObjects
[System.Serializable]
public class GameObjList
{
    public List<GameObject> list;
}

//List of GameObjLists = List of List of GameObjects
//For groups of any size, with however many groups are needed.
[System.Serializable]
public class ListList
{
    public List<GameObjList> listList;
}
