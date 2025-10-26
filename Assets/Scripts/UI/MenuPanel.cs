using UnityEngine;

public class MenuPanel : BasePanel
{
    [SerializeField] private BasePanel _nextPanel;

    public void OnPlayButtonClick()
    {
        _nextPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
