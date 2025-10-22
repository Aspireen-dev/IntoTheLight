using UnityEngine;

public class StartPlacementManager : MonoBehaviour
{
    [SerializeField] private Transform _startPlayerPlacement;
    [SerializeField] private Transform _startTargetPlacement;

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera mainCamera = Camera.main;

        float screenWidth = ScreenTools.GetScreenWidth(mainCamera);

        _startPlayerPlacement.position = new Vector2(-screenWidth / 2 * 0.8f, 0f);
        _startTargetPlacement.position = new Vector2(screenWidth / 2 * 0.8f, 0f);

        _player.position = _startPlayerPlacement.position;
        _target.position = _startTargetPlacement.position;
    }

    public void ResetPlayerPosition()
    {
        _player.position = _startPlayerPlacement.position;
    }
}
