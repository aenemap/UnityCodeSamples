using System.Collections
using System.Collections.Generic;
using UnityEngine;

public class PlanetRing: MonoBehaviour
{

  //manual settings
  [Range(3,360)]
  public int segments = 3;
  public float innerRadius = 0.7f; //How far from the planet the ring starts
  public float thickness = 0.5f;
  public Material ringMat;

  //cached references
  GameObject ring;
  Mesh ringMesh;
  MeshFilter ringMF;
  MeshRenderer = ringMR;

  void Start()
  {
    //create ring Object
    ring  = new GameObject(name + " Ring"); //Create a game object with the name of the current transform + the ring
    ring.transform.parent = transform; //Set as parent of the new object the currnet tranform
    ring.transform.localScale = Vector3.one;
    ring.transform.localPosition = Vector3.zero;
    ring.transform.localRotation = Quaternion.identity;
    ringMF = ring.AddComponent<MeshFilter>();
    ringMesh = ringMF.mesh;
    ringMR = ring.AddComponent<MeshRenderer>();
    ringMR.material = ringMat;

    //build ring mesh
    Vector3[] vertices = new Vector3[(segments + 1) * 2 * 2];
    int[] triangles = new int[segments * 6 * 2];
    Vector2[] uv = new Vector2[(segments + 1) * 2 * 2];
    int halfway = (segments + 1) * 2;

    for (int i = 0; i < segments + 1; i++)
    {
      //looping from 0 to 1. At start the division is 0 because i is zero . A the last loop the division is segments / segments = 1
      //normalized representation around the circle
      float progress = (float)i / (float)segments;
      //progress * 360 is the progress in degrees and we convert to radians by multiplying with the Mathf.Deg2Rad
      float angle = Mathf.Deg2Rad * progress * 360;
      //get the x and z coordinates by calculating the Sin and the Cos of the angle
      float x = Mathf.Sin(angle);
      float z = Mathf.Cos(angle); //z is the y representation

      //the flolowing gives as the 2 point that we are inside and outside of the ring
      vertices[i * 2] = vertices[i * 2 + halfway] = new Vector3(x, 0f, z) * (innerRadius + thickness);
      vertices[i * 2 + 1] = vertices[i * 2 + 1 + halfway] = new Vector3(x, 0f, z) * innerRadius;

      uv[i * 2] = uv [i * 2 + halfway] = new Vector2(progress, 0f);
      uv [i * 2 + 1] = uv[i * 2 + 1 + halfway] = new Vector2(progress, 1f);

      if (i != segments)
      {
        triangles[i * 12] = i * 2;
        triangles[i * 12 + 1] = triangles[i * 12 + 4] = (i + 1) * 2;
        triangles[i * 12 + 2] = triangles[i * 12 + 3] = i * 2 + 1;
        triangles[i * 12 + 5] = (i + 1) * 2 + 1;

        triangles[i * 12 + 6] = i * 2 + halfway;
        triangles[i * 12 + 7] = triangles[i * 12 + 10] = i * 2 + 1 + halfway;
        triangles[i * 12 + 8] = triangles[i * 12 + 9] = (i + 1) * 2 + halfway;
        triangles[i * 12 + 11] = (i + 1) * 2 + 1 + halfway;
      }

    }
    ringMesh.vertices = vertices;
    ringMesh.triangles = triangles;
    ringMesh.uv = uv;
    ringMesh.RecalculateNormals();



  }
}
