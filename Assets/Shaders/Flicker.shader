Shader "Hidden/Flicker"
{
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM

            #pragma vertex Vertex
            #pragma fragment Fragment

            float4 Vertex(float4 position : POSITION) : SV_Position
            {
                return UnityObjectToClipPos(position);
            }

            float4 Fragment(float4 position : SV_Position) : SV_Target
            {
                float g = frac(_Time.y * 16.47289);
                return float4(g, g, g, 1);
            }

            ENDCG
        }
    }
}
