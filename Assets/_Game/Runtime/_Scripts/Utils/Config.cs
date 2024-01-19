using UnityEngine;

public static class ShaderConfig
{
    public static string brightnessName => "_Brightness";
    public static string dirAlphaFadeFadeName => "_DirectionalAlphaFadeFade";
    public static string timeValueName => "_TimeValue";
}

public static class TimeConfig
{
    public static float refFPS => 60f;
}