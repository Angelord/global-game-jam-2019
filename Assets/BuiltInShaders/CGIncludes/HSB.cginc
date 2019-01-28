inline float3 applyHue(float3 aColor, float aHue)
{
    float angle = radians(aHue);
    float3 k = float3(0.57735, 0.57735, 0.57735);
    float cosAngle = cos(angle);
    //Rodrigues' rotation formula
    return aColor * cosAngle + cross(k, aColor) * sin(angle) + k * dot(k, aColor) * (1 - cosAngle);
}
 
 
inline float4 applyHSBEffect(float4 startColor, float inHue, float inBrightness, float inSat, float contrast) {
    float hue = 360 * inHue;
    float _Brightness = inBrightness * 2 - 1;
    float _Saturation = inSat * 2;
    float _Contrast = contrast * 2;

    float4 outputColor = startColor;
    outputColor.rgb = applyHue(outputColor.rgb, hue);
    outputColor.rgb = (outputColor.rgb - 0.5f) * (_Contrast) + 0.5f;
    outputColor.rgb = outputColor.rgb + _Brightness;      
    float3 intensity = dot(outputColor.rgb, float3(0.299,0.587,0.114));
    outputColor.rgb = lerp(intensity, outputColor.rgb, _Saturation);
 
    return outputColor;
}