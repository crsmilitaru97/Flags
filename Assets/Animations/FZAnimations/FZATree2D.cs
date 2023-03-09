using System.Collections;
using UnityEngine;

//10.10.22

public class FZATree2D : MonoBehaviour
{
    public float minRotation = 1f;
    public float maxRotation = 1.75f;
    Vector3 direction;

    void Start()
    {
        StartCoroutine(Anim());
    }

    void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(direction);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime);
    }

    private IEnumerator Anim()
    {
        while (true)
        {
            float val = Random.Range(minRotation, maxRotation);
            direction = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -val);
            yield return new WaitForSeconds(val);

            val = Random.Range(minRotation, maxRotation);
            direction = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, val);
            yield return new WaitForSeconds(val);
        }
    }
}
