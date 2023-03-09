using UnityEngine;

public class FZMaterial
{
    public static Material[] ChangeAllMaterials(GameObject objectWithMats, Material newMaterial)
    {
        Material[] mats = objectWithMats.GetComponent<MeshRenderer>().materials;
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i] = newMaterial;
        }
        return mats;
    }
}