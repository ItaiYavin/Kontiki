using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItem : MonoBehaviour
{
	public Text text;

    public Texture2D iconTexture;
    public Material iconMaterial
    {
        get;
        private set;
    }

    void Start()
    {
        iconMaterial = new Material(Shader.Find("Sprites/Default"));
        iconMaterial.SetTexture("_MainTex", iconTexture);
    }
}
