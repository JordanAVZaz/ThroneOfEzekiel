using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CachedMaterials : MonoBehaviour
{
    private static Dictionary<string, Material> materials = new();
    public static Material GetMaterial(string texturePath)
    {
        if (materials.ContainsKey(texturePath))
        {
            return materials[texturePath];
        }

        Texture2D texture = Resources.Load<Texture2D>(texturePath);
        if (texture != null)
        {
            Material material = new Material(Shader.Find("Custom/2DPlaneOutline"));  
            material.mainTexture = texture;
            materials[texturePath] = material;
            return material;
        }
        else
        {
            Debug.LogError("Failed to load texture from: " + texturePath);
            return null;
        }
    }
}
