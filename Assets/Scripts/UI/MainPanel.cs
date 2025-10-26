using UnityEngine;

public class MainPanel : BasePanel
{
    [SerializeField] private BasePanel _nextPanel;

    public void OnPlayButtonClick()
    {
        _nextPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
