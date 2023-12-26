using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MyPostEffect : ScriptableRendererFeature
{
    [SerializeField] private GrayscaleSetting settings = new GrayscaleSetting();
    private GrayScalePass scriptablePass;

    public override void Create()
    {
        if (settings.material != null)
        {
            scriptablePass = new GrayScalePass();
            scriptablePass.postEffectMaterial = settings.material;
            scriptablePass.renderPassEvent = settings.renderPassEvent;
        }
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (scriptablePass != null && scriptablePass.postEffectMaterial != null)
        {
            // �����_�[�L���[�ɓo�^ (�|�X�g�G�t�F�N�g���s)
            renderer.EnqueuePass(scriptablePass);
        }
    }


    [System.Serializable]
    public class GrayscaleSetting
    {
        // �|�X�g�G�t�F�N�g�Ɏg�p����}�e���A��
        public Material material;

        // �����_�����O�̎��s�^�C�~���O
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    /// <summary>
    /// Grayscale���sPass
    /// </summary>
    class GrayScalePass : ScriptableRenderPass
    {
        private readonly string profilerTag = "GrayScale Pass";

        public Material postEffectMaterial; // �O���[�X�P�[���v�Z�p�}�e���A��

        [System.Obsolete]
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cameraColorTarget = renderingData.cameraData.renderer.cameraColorTarget;

            // �R�}���h�o�b�t�@
            var cmd = CommandBufferPool.Get(profilerTag);

            // �}�e���A�����s
            cmd.Blit(cameraColorTarget, cameraColorTarget, postEffectMaterial);

            context.ExecuteCommandBuffer(cmd);
        }
    }
}