using SG;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    private Camera _camera;
    private Camera Camera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    private void Update()
    {
        transform.rotation = Camera.transform.rotation;
    }

}
