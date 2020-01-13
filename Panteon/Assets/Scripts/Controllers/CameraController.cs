using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Reposition the camera right in to the middle of the world
        World world = WorldController.instance.World;
        int x = world.Width / 2;
        int y = world.Height / 2;

        transform.position = new Vector3(x, y, transform.position.z);


    }

    private void Update()
    {

    }

}
