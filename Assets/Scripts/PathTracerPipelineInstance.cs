using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

public class PathTracerPipelineInstance : RenderPipeline
{
    private readonly ComputeShader _shader;
    private readonly int _width;
    private readonly int _height;
    
    private const string KernelName = "CSMain";
    
    public PathTracerPipelineInstance(ComputeShader shader)
    {
        _shader = shader;
        _width = Screen.width;
        _height = Screen.height;
    }

    public override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        base.Render(context, cameras);

        var cmd = new CommandBuffer();
        cmd.ClearRenderTarget(true, true, Color.yellow);
        
        if (_shader != null && _shader.HasKernel(KernelName))
        {
            int index = _shader.FindKernel("CSMain");
            var target = new RenderTargetIdentifier(BuiltinRenderTextureType.CurrentActive);
            cmd.SetComputeTextureParam(_shader, index, "Result", target);
            cmd.DispatchCompute(_shader, index, _width, _height, 1);
        }
        
        context.ExecuteCommandBuffer(cmd);
        
        context.Submit();
    }
}
