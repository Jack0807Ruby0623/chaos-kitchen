using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DynamicCircle : MonoBehaviour
{
    [Header("🎨 默认颜色（当没有全局管理器时使用）")]
    public Color defaultColor = Color.white;

    [Header("⚙️ 是否跟随全局颜色？")]
    public bool useGlobalColor = true;

    private Image image;
    private bool hasColorManager = false;

    void Awake()
    {
        image = GetComponent<Image>();
        // 如果 Image 不存在，自动创建（但建议手动挂 Image）
        if (image == null)
        {
            image = gameObject.AddComponent<Image>();
            // 尝试使用默认圆形 Sprite
            image.sprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/UISprite.psd");
        }
    }

    void OnEnable()
    {
        ColorManager.OnColorChanged += UpdateColor;
        UpdateColor();
    }

    void OnDisable()
    {
        ColorManager.OnColorChanged -= UpdateColor;
    }

    void UpdateColor()
    {
        if (useGlobalColor)
        {
            image.color = ColorManager.currentColor;
        }
        else
        {
            image.color = defaultColor;
        }
    }

    public void SetColor(Color color)
    {
        useGlobalColor = false;
        defaultColor = color;
        image.color = color;
    }
}
