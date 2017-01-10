using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Prefab("AuraManager", true, "")]
public class AuraManager : Singleton<AuraManager>
{
    [System.Serializable]
    public class ColorAuraAmount {
        public Types.colortypes color;
        public float amount;

        public ColorAuraAmount(Types.colortypes _color, float _amount) {
            color = _color;
            amount = _amount;
        }
    }
    public int auraPercentage;

    public float currentAuraPower;
    public float auraLevelPercentage;
    public float percentageAverage;

    public string A_C;
    public string A_C_C;
    public int colorTypeCode;
    public bool isBlend;
    public List<ColorAuraAmount> ColorAuraAmounts = new List<ColorAuraAmount>();
    private Types.colortypes firstColor;
    private Types.colortypes secondColor;

    public void Load() { return; }

    public void Start() {
        ColorAuraAmounts.Add(new ColorAuraAmount(Types.colortypes.Blue, 0));
        ColorAuraAmounts.Add(new ColorAuraAmount(Types.colortypes.Red, 0));
        ColorAuraAmounts.Add(new ColorAuraAmount(Types.colortypes.Green, 0));
        ColorAuraAmounts.Add(new ColorAuraAmount(Types.colortypes.Yellow, 0));
        if (isBlend) {
            int[] colors = ColorManager.I.getColorsOfBlend(colorTypeCode);
            firstColor = (Types.colortypes)colors[0];
            secondColor = (Types.colortypes)colors[1];
        }
    }

    public void CalculateAuraPercentage()
    {
        CalculateCurrentAuraPower();
        percentageAverage = (Mathf.Round(LevelManager.I.M_T_A / 100 * 55));
        auraLevelPercentage = (currentAuraPower / LevelManager.I.A_P_T) * 100;
        ChangeAuraFogColor();
    }

    public int EffectOnCurrentAuraColor(Types.colortypes color) {
        // return values 0 = negative, 1 = possitive, 2 = neutral (no effect)

        if (isBlend)
        {
            // The auracolor goal is a blend, we need to get the associated colors
            if (color != firstColor && color != secondColor)
            {
                return 0;
            }
            else {
                return 1;
            }
        }
        else {
            if (color == (Types.colortypes)colorTypeCode)
            {
                return 1;
            }
            else {
                return 0;
            }
        }

    }
   
    private void CalculateCurrentAuraPower() {

        if (isBlend) {
            float blendingPower = 0;
            float firstAmount = GetColorAmount(firstColor);
            float secondAmount = GetColorAmount(secondColor);
            if (firstAmount != 0 && secondAmount != 0) {
                if (firstAmount != secondAmount)
                {
                    float lowest = Mathf.Min(firstAmount, secondAmount);
                    blendingPower = lowest * 2;
                }
                else
                {
                    blendingPower = firstAmount + secondAmount;
                }
                currentAuraPower = blendingPower;
            }
        }

        foreach (ColorAuraAmount colorAura in ColorAuraAmounts)
        {
            int effect = EffectOnCurrentAuraColor(colorAura.color);
            if (effect == 1)
            {
                if (!isBlend)
                {
                    currentAuraPower = currentAuraPower + colorAura.amount;
                }
            }
            else
            {
                if (currentAuraPower != 0)
                {
                    currentAuraPower = currentAuraPower - colorAura.amount;
                }

            }
        }
    }

    public void AddColorAmount(Types.colortypes color, float auraPower) {
        foreach (ColorAuraAmount colorAura in ColorAuraAmounts)
        {
            if (color == colorAura.color)
            {
                colorAura.amount = colorAura.amount + auraPower;
            }

        }
    }

    public float GetColorAmount(Types.colortypes color)
    {
        foreach (ColorAuraAmount colorAura in ColorAuraAmounts)
        {
            if (color == colorAura.color)
            {
                return colorAura.amount;
            }

        }
        return 0;
    }

    private void ChangeAuraFogColor() {
        float totalRGB = 255;
        float colorPercentage = Mathf.Round(((totalRGB / 100) * auraLevelPercentage));
        Color newColor = new Color();
        if (isBlend)
        {
            if ((Types.blendedColors)colorTypeCode == Types.blendedColors.Purple)
            {
                newColor.r = colorPercentage / totalRGB;
                newColor.b = colorPercentage / totalRGB;
            }
            else if ((Types.blendedColors)colorTypeCode == Types.blendedColors.Orange)
            {
                newColor.r = colorPercentage / totalRGB;
                newColor.g = (colorPercentage / totalRGB) / 2;
            }
            else if ((Types.blendedColors)colorTypeCode == Types.blendedColors.Brown)
            {
                newColor.r = colorPercentage / totalRGB;
                newColor.g = (colorPercentage / totalRGB) / 2;
            }
            else if ((Types.blendedColors)colorTypeCode == Types.blendedColors.Lime)
            {
                newColor.g = colorPercentage / totalRGB;
                newColor.r = (colorPercentage / totalRGB) / 2;
            }
            else if ((Types.blendedColors)colorTypeCode == Types.blendedColors.Turquoise)
            {
                newColor.g = colorPercentage / totalRGB;
                newColor.b = (colorPercentage / totalRGB);
            }
        }
        else {
            if ((Types.colortypes)colorTypeCode == Types.colortypes.Red) {
                newColor.r = (colorPercentage / totalRGB);
            }
            else if ((Types.colortypes)colorTypeCode == Types.colortypes.Blue)
            {
                newColor.b = (colorPercentage / totalRGB);
            }
            else if ((Types.colortypes)colorTypeCode == Types.colortypes.Green)
            {
                newColor.g = (colorPercentage / totalRGB);
            }
            else if ((Types.colortypes)colorTypeCode == Types.colortypes.Yellow)
            {
                newColor.r = (colorPercentage / totalRGB);
                newColor.g = (colorPercentage / totalRGB);
            }
        }

        newColor.a = 1;
        RenderSettings.fogColor = newColor;
    }
}
