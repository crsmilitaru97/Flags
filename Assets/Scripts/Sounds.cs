using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip textSound;

    [Header("Drill")]
    public AudioClip[] digIn;
    public AudioClip[] digOut;
    public AudioClip laserShoot;
    public AudioClip enterWater;
    public AudioClip underwaterAmbience;
    public AudioClip hurt;
    [Header("Workshop")]
    public AudioClip[] craftItem;
    public AudioClip burnItem;
    public AudioClip duplicateItem;
    [Header("Powerups")]
    public AudioClip fuelCan;
    public AudioClip explosion;
    public AudioClip boost;
    public AudioClip repairKit;
    [Header("Build")]
    public AudioClip build;
    public AudioClip teleport;

    public static Sounds Instance;
    private void Awake() => Instance = this;
}
