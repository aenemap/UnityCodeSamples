using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour
{
  public float initialVelocity;
  public float acceleration;
  float currentVelocity;

  void Start()
  {
    currentVelocity = initialVelocity;
  }

  void FixedUpdate()
  {
    if(Time.fixedTime < Timer.predictedTime)
    {
      //current velocity is increasing by acceleration every second
      currentVelocity += acceleration * Time.fixedDeltaTime;
      //Move Object
      transform.Translate(Vector3.right * currentVelocity * Time.fixedDeltaTime);
    }
  }
}

/********************************************************************************/

//Timer Class separate cs

using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
  public static float predictedTime;
  public Motor objectA;
  public Motor objectB;

  public float timeStep = 0.02f;


  void Start()
  {
    //Deacrising the time step will improve the simulation
    //Not for actual game
    // Time.fixedDeltaTime = timeStep;
    float h = objectA.transform.position.x - objectB.transform.position.x;
    // a = acceleration objectB - acceleration objectA;
    // b = 2 * initialVelocity objectB - 2 * initialVelocity objectA
    // c = -2 distance

    float a = objectB.acceleration - objectA.acceleration;
    float b = 2 * (objectB.initialVelocity - objectA.initialVelocity);
    float c = -2 * h;

    predictedTime = (-b * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    print(predictedTime);

  }
}
