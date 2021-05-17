using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class InstantiationExample : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public Person myPrefab;
    public int noOfAgents = 40;
    public GameObject Safehouse;

    public MeshFilter roadMeshParent;
    private Mesh roadMesh;
    public GameObject roadgo;

    List<NavMeshAgent> navAgent = new List<NavMeshAgent>();
    List<NavMeshPath> paths = new List<NavMeshPath>();


    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {

        Vector3[] tot = GetVerticesInChildren(roadgo);

        transform.position = roadgo.transform.position;
        transform.rotation = roadgo.transform.rotation;
        //roadMesh = roadMeshParent.mesh;


        var points = evenlyDistributedPointsOnMesh(noOfAgents, tot); //this method returns a list of seperated verticies

        int count = 0;
        for (int i = 0; i < noOfAgents -1; i++)
        {



            //Vector3 worldsp = transform.TransformPoint(points[i]);

            Person agent = Instantiate(myPrefab, points[i], Quaternion.identity, transform);

            Vector3 currentposition = agent.transform.position;

            agent.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(currentposition);
            agent.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(Safehouse.transform.position);
       
            //paths.Add(agent.GetPath(Safehouse));
            //navAgent.Add(agent.GetComponent<NavMeshAgent>());
            
            
            count++;
           
             

        }

        for (int i = 0; i < noOfAgents - 1; i++)
        {
            navAgent[i].SetPath(paths[i]);
        }

    }




    Vector3[] GetVerticesInChildren(GameObject go)
    {
        MeshFilter[] mfs = go.GetComponentsInChildren<MeshFilter>();
        List<Vector3> vList = new List<Vector3>();

        foreach (MeshFilter mf in mfs)
        {
            Mesh me = mf.GetComponent<MeshFilter>().mesh;

            foreach (Vector3 veccy in me.vertices)
            {
                Vector3 worldspac = mf.transform.TransformPoint(veccy);
                vList.Add(worldspac);
            }
            
        }
        return vList.ToArray();
    }



    List<Vector3> evenlyDistributedPointsOnMesh(int pointCount, Vector3[] listofveccys) 
    {

        var result = new List<Vector3>();
        
        int spacing = listofveccys.Length / pointCount;

        for (int i = 1; i< pointCount; i++) 
        {
            var point = listofveccys[i * spacing];
            result.Add(point);
        }
        return result;
    }
}
        
