using UnityEngine;

public class GunRotation : MonoBehaviour
{
    [Header("Mouse Aiming Rotation Limits")]
    [SerializeField] float minYRotation = -30f;
    [SerializeField] float maxYRotation = 30f;

    void Update()
    {
        float mouseX = Input.mousePosition.x / Screen.width;
        float targetYRotation = Mathf.Lerp(minYRotation, maxYRotation, mouseX);
        transform.rotation = Quaternion.Euler(0, targetYRotation, 0f);
    }
}
