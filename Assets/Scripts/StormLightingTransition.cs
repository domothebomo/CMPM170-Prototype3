using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormLightingTransition : MonoBehaviour
{
    public float timeBeforeTransition = 30f;
    public float speedOfTransition = 1f;
    public Material EndSkybox;
    public Light globalLightSource;
    public Color endLightColor;
    private Material transSkybox;
    private Color destColor;
    private float destAtmosThickness;
    // Start is called before the first frame update
    void Start()
    {
        transSkybox = new Material(RenderSettings.skybox);
        RenderSettings.skybox = transSkybox;
        destColor = EndSkybox.GetColor("_SkyTint");
        destAtmosThickness = EndSkybox.GetFloat("_AtmosphereThickness");
        StartCoroutine(skyTransition());
    }

    private IEnumerator skyTransition()
    {
        yield return new WaitForSeconds(timeBeforeTransition);
        Color oldColor = Color.black;
        float oldAtmosThickness = 0;
        if (speedOfTransition <= 0f) { speedOfTransition = 0.0001f; }
        while (Mathf.Abs(oldAtmosThickness - destAtmosThickness) > 0.005f)
        {
            yield return new WaitForSeconds(0.1f / (speedOfTransition));
            oldColor = transSkybox.GetColor("_SkyTint");
            Color newColor = (destColor + oldColor * 99) / 100;
            transSkybox.SetColor("_SkyTint", newColor);

            oldAtmosThickness = transSkybox.GetFloat("_AtmosphereThickness");
            float newAtmosThickness = (destAtmosThickness + oldAtmosThickness * 99) / 100;
            transSkybox.SetFloat("_AtmosphereThickness", newAtmosThickness);

            globalLightSource.color = (endLightColor + globalLightSource.color * 99) / 100;
        }
        Debug.Log("Now Stormy");
    }
}
