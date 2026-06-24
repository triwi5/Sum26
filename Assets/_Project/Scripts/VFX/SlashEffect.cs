using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SlashEffect : MonoBehaviour
{
    [Header("Appearance")] 
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private int arcSegments = 16;

    [Header("Mesh height")] [SerializeField]
    private float yOffset = 0.05f;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Material materialInstance;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        materialInstance = meshRenderer.material;
    }

    public void Play(float coneAngle, float range)
    {
        meshFilter.mesh = BuildArcMesh(coneAngle, range);
        Color startColor = materialInstance.GetColor("_BaseColor");
        startColor.a=1f;
        materialInstance.SetColor("_BaseColor", startColor);
        materialInstance.DOFade(0f, "_BaseColor", duration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => Destroy(gameObject));
    }

    private Mesh BuildArcMesh(float coneAngle, float range)
    {
        Mesh mesh = new Mesh();

        int vertexCount = arcSegments + 2;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[arcSegments * 3];

        vertices[0] = new Vector3(0f, yOffset, 0f);
        
    }
   
        
}
