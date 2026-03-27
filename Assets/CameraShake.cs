
using UnityEngine;

public class CameraShake : MonoBehaviour
{
public bool isShaking = false;
public float shakeAmount = 0.05f;

void LateUpdate()
{
if (isShaking)
{
float offsetX = Random.Range(-1f, 1f) * shakeAmount;
float offsetY = Random.Range(-1f, 1f) * shakeAmount;

transform.localPosition = new Vector3(
transform.localPosition.x + offsetX,
transform.localPosition.y + offsetY,
transform.localPosition.z
);
}
}
}
