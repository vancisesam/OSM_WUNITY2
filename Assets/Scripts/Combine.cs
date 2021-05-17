using UnityEngine;

// This script should be put on an empty GameObject
// Objects to be combined should be children of the empty GameObject
//@script RequireComponent(MeshFilter)
//@script RequireComponent(MeshRenderer)


public class Combine : MonoBehaviour
{
  
    

    public static void CombineMeshes(List<MeshFilter> objedtsToMerge)
    {

        obj.transform.position = Vector3.zero;

        GameObject Roads = new GameObject("WalkableLand");

        MeshFilter finalMF = Roads.AddComponent<MeshFilter>();
        Roads.AddComponent<MeshRenderer>();
        Mesh finalmesh = Roads.GetComponent<MeshFilter>().mesh;


        MeshFilter[] meshFilters = obj.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(true);
            i++;
        }

        finalmesh.CombineMeshes(combine);
        finalMF.sharedMesh = finalmesh;
        

        //obj.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        //obj.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, true, true);
        //obj.transform.gameObject.SetActive(true);

        //Reset position
        //obj.transform.position = position;


    }
}
