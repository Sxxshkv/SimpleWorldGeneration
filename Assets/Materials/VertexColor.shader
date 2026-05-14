// Shader "Custom/VertexColor"
Shader "Custom/VertexColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // опционально: можно наложить текстуру поверх vertex color
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                fixed4 color : COLOR; // ← это и есть Mesh.colors
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color; // передаём vertex color дальше
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Можно умножить на текстуру, если нужна опция с ней — но для чистого vertex-color:
                return i.color;
                // Или: return tex2D(_MainTex, uv) * i.color; — если нужно сочетать текстуру и цвет
            }
            ENDCG
        }
    }
}
