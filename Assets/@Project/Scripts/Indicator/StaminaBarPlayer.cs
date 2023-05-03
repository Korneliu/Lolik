using UnityEngine;
using UnityEngine.UI;

public class StaminaBarPlayer : MonoBehaviour
{
    [SerializeField] Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxStamina(float maxStamina)
    {
        slider.maxValue = maxStamina;
        slider.value = maxStamina;
    }

    public void SetCurrentStamina(float currentStamina)
    {
        slider.value = currentStamina;
    }
}
