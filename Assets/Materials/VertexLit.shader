Shader "Custom/VertexLit"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base Texture (RGB)", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Name "ForwardBase"
            Tags { "LightMode"="ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase      // ← для ForwardBase + теней
            #include "UnityCG.cginc"
            #include "Lighting.cginc"          // ← КРИТИЧНО ВАЖНО!
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                fixed4 color : COLOR; // Vertex colors from Mesh.colors
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed4 vertexColor : COLOR0;
                float3 worldNormal : NORMAL;
                float3 worldPos : TEXCOORD1;
                SHADOW_COORDS(2)
                #ifdef _MAINTEX_EXISTS
                float2 uv : TEXCOORD3;
                #endif
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                // Нормаль в мировом пространстве
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                // Передаём vertex color и UV (если есть)
                o.vertexColor = v.color * _Color;

                #ifdef _MAINTEX_EXISTS
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                #endif

                TRANSFER_SHADOW(o);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Нормаль и свет — теперь корректно доступны благодаря Lighting.cginc
                float3 worldNormal = normalize(i.worldNormal);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz); // ← работает!

                fixed diff = max(0, dot(worldNormal, lightDir));
                fixed3 ambient = unity_AmbientSky; // ← тоже доступен

                fixed3 lightColor = _LightColor0.rgb; // ← теперь OK!

                fixed3 diffuse = lightColor * diff;

                #ifdef _MAINTEX_EXISTS
                    fixed4 texCol = tex2D(_MainTex, i.uv);
                    fixed3 finalColor = (texCol.rgb * i.vertexColor.rgb) * (diffuse + ambient);
                #else
                    fixed3 finalColor = i.vertexColor.rgb * (diffuse + ambient);
                #endif

                // Применяем тени
                fixed shadow = SHADOW_ATTENUATION(i);
                finalColor *= shadow;

                return fixed4(finalColor, i.vertexColor.a);
            }
            ENDCG
        }

        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
