using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    // [SerializeField] private CuttingCounter cuttingCounter;  // 被替换：不再直接依赖 CuttingCounter
    [SerializeField] private GameObject hasProgressGameObject; // 用于在编辑器中指定实现了 IHasProgress 的 GameObject

    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        // 从指定的 GameObject 中获取实现了 IHasProgress 接口的组件
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("Game Object " + hasProgressGameObject + " does not have a component that implements IHasProgress!");
            return;
        }

        // cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;  // 被替换：订阅接口事件
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        barImage.fillAmount = 0;
        Hide();
    }

    // private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    // 被替换：使用接口定义的事件参数
    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}