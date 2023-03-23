using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip textSound;

    public AudioClip correct;
    public AudioClip wrong;

    public static Sounds Instance;
    private void Awake() => Instance = this;
}
