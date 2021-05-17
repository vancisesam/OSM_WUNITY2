using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class Parent: MonoBehaviour
{


    public Transform CreateParent(string name)
    {


        GameObject go = new GameObject("name");
        MeshFilter mf = go.AddComponent<MeshFilter>();
        MeshRenderer mr = go.AddComponent<MeshRenderer>();

        return  go.transform;

    }

}
