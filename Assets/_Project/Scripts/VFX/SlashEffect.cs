using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SlashEffect : MonoBehaviour
{
    [Header("Appearance")]
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private int arcSegments = 24;       

    [Header("Slash Behavior")]
    [Tooltip("How wide the visible slash 'streak' is, as a fraction of the full cone (0-1).")]
    [Range(0.05f, 1f)]
    [SerializeField] private float streakWidth = 0.35f;

    [Header("Mesh height")]
    [SerializeField] private float yOffset = 0.05f;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Material materialInstance;
    private Mesh mesh;
    
    private float coneAngle;
    private float range;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        materialInstance = meshRenderer.material;
        
        mesh = new Mesh();
        mesh.MarkDynamic();  
        meshFilter.mesh = mesh;
    }

    public void Play(float coneAngle, float range)
    {
        this.coneAngle = coneAngle;
        this.range = range;

        // Reset alpha to full.
        Color startColor = materialInstance.GetColor("_BaseColor");
        startColor.a = 1f;
        materialInstance.SetColor("_BaseColor", startColor);
        
        float progress = 0f;
        DOTween.To(
            () => progress,
            value => {
                progress = value;
                UpdateSlash(progress);
            },
            1f,
            duration
        )
        .SetEase(Ease.OutQuad)          
        .OnComplete(() => Destroy(gameObject));
        
        materialInstance.DOFade(0f, "_BaseColor", duration).SetEase(Ease.InQuad);
    }
    
    private void UpdateSlash(float progress)
    {
        float halfAngle = coneAngle * 0.5f;
        
        float extendedRange = coneAngle * (1f + streakWidth);
        float leadingAngle = Mathf.Lerp(-halfAngle - coneAngle * streakWidth, halfAngle, progress);
        float trailingAngle = leadingAngle - coneAngle * streakWidth;
        
        leadingAngle = Mathf.Clamp(leadingAngle, -halfAngle, halfAngle);
        trailingAngle = Mathf.Clamp(trailingAngle, -halfAngle, halfAngle);
        
        if (leadingAngle <= trailingAngle)
        {
            mesh.Clear();
            return;
        }

        BuildArcMesh(trailingAngle, leadingAngle);
    }
    
    private void BuildArcMesh(float startAngle, float endAngle)
    {
        int vertexCount = arcSegments + 2;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[arcSegments * 3];

        vertices[0] = new Vector3(0f, yOffset, 0f);

        for (int i = 0; i <= arcSegments; i++)
        {
            float t = (float)i / arcSegments;
            float angleDegrees = Mathf.Lerp(startAngle, endAngle, t);
            float angleRadians = angleDegrees * Mathf.Deg2Rad;

            float x = Mathf.Sin(angleRadians) * range;
            float z = Mathf.Cos(angleRadians) * range;

            vertices[i + 1] = new Vector3(x, yOffset, z);
        }

        for (int i = 0; i < arcSegments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}