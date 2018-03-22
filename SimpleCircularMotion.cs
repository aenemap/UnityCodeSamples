using UnityEngine;
using System.Collections

public class CircularMotion : MonoBehaviour
{
  float timeCounter;
  float speed;
  float width;
  float height;

  void Start()
  {
    speed = 5;
    width = 5;
    height = 5;
  }

  void Update()
  {
    timeCounter += Time.deltaTime * speed;

    float x = Math.Cos (timeCounter) * width ;
    float y = Math.Sin (timeCounter) * height;
    float z = 0;

    transform.position = new Vector3(x, y, z);
  }


}
