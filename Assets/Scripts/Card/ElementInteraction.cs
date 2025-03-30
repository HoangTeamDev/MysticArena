using System.Collections.Generic;
using UnityEngine;

public static class ElementInteraction
{
    // Danh sách các hệ tương khắc
    private static Dictionary<ElementType, ElementType> elementWeakness = new Dictionary<ElementType, ElementType>()
    {
        { ElementType.Fire, ElementType.Wind },  // 🔥 Hỏa → 🌿 Phong
        { ElementType.Wind, ElementType.Earth }, // 🌿 Phong → ⛰️ Địa
        { ElementType.Earth, ElementType.Water }, // ⛰️ Địa → 🌊 Thủy
        { ElementType.Water, ElementType.Fire }, // 🌊 Thủy → 🔥 Hỏa
        { ElementType.Thunder, ElementType.Light }, // ⚡ Lôi → 🌕 Quang
        { ElementType.Light, ElementType.Dark }, // 🌕 Quang → 🌑 Ám
        { ElementType.Dark, ElementType.Thunder } // 🌑 Ám → ⚡ Lôi
    };

    /// <summary>
    /// Kiểm tra hệ có khắc hay bị khắc không
    /// </summary>
    public static float GetDamageMultiplier(ElementType attacker, ElementType defender)
    {
        if (elementWeakness.ContainsKey(attacker) && elementWeakness[attacker] == defender)
        {
            Debug.Log($"Hệ {attacker} khắc {defender}: +15% sát thương!");
            return 1.15f;
        }
        else if (elementWeakness.ContainsKey(defender) && elementWeakness[defender] == attacker)
        {
            Debug.Log($"Hệ {attacker} bị {defender} khắc: -15% sát thương!");
            return 0.85f;
        }
        return 1.0f; // Không bị ảnh hưởng
    }
}
