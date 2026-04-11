using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonConfirm : MonoBehaviour
{
    public Button button;
    public RectTransform rectTransform;
    public TextMeshProUGUI _des;
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
