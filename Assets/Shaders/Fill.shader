Shader "Hidden/Fill"
{
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM

            #pragma vertex Vertex
            #pragma fragment Fragment

            float _Input;

            float4 Vertex(float4 position : POSITION) : SV_Position
            {
                return UnityObjectToClipPos(position);
            }

            float4 Fragment(float4 position : SV_Position) : SV_Target
            {
                return float4(_Input, _Input, _Input, 1);
            }

            ENDCG
        }
    }
}
