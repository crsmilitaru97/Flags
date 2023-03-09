using UnityEngine;

public class FZShake2DEffect : MonoBehaviour
{
    // Desired duration of the shake effect
    private float shakeDuration = 0f;
    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude = 0.7f;
    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;

    bool shake;
    Vector3 initialPosition;

    void Update()
    {
        if (!shake)
            return;

        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shake = false;
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake(float shakeDuration)
    {
        initialPosition = transform.localPosition;
        this.shakeDuration = shakeDuration;
        shake = true;
    }
}
