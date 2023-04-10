using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _enemyHP;
    private float _maxValue;
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

    public void Init(float maxValue) => _maxValue = maxValue;
    public void SetValue(float value) => _enemyHP.fillAmount = (1F / _maxValue) * value;
}