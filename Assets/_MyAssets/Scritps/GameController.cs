using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    [SerializeField] Portal portalPrefab;
    [SerializeField] Sprite testSprite;

    bool gameStarted;
    public bool GameStarted
    {
        get
        {
            return gameStarted;
        }
        set
        {
            gameStarted = value;
            MainCharacterController.Instance.GameStarted = gameStarted;
            if (gameStarted)
            {
                CharacterAnimationController.Instance.SetAnimation(CharacterAnimationType.running);
            }
        }
    }

    private void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(portalPrefab, Vector3.forward * i * 10, Quaternion.identity).Setup(
                () => Debug.Log("option1Chosen"),
                () => Debug.Log("option2Chosen"),
                null, testSprite,
                "party", "");
        }
    }
}
