using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Reposition the camera right in to the middle of the world
        World world = WorldController.Instance.World;
        int x = world.Width / 2;
        int y = world.Height / 2;

        transform.position = new Vector3(x, y, transform.position.z);


    }

    private void Update()
    {

    }

}
