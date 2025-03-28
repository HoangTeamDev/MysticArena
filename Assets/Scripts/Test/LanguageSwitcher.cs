using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    public void SetToEnglish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales
            .Find(locale => locale.Identifier.Code == "en");
    }

    public void SetToVietnamese()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales
            .Find(locale => locale.Identifier.Code == "vi-VN");
    }
}
