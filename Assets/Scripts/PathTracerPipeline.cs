using UnityEngine;
using UnityEngine.Experimental.Rendering;

[ExecuteInEditMode]
public class PathTracerPipeline : RenderPipelineAsset
{
    [SerializeField] private ComputeShader _shader;
    
#if UNITY_EDITOR
    // Call to create a simple pipeline
    [UnityEditor.MenuItem("Create/Path Tracer Pipeline")]
    private static void CreateBasicAssetPipeline()
    {
        var instance = CreateInstance<PathTracerPipeline>();
        UnityEditor.AssetDatabase.CreateAsset(instance, "Assets/PathTracerPipeline.asset");
    }
#endif
    
    protected override IRenderPipeline InternalCreatePipeline()
    {
        return new PathTracerPipelineInstance(_shader);
    }
}
