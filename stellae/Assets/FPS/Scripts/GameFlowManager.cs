using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{




    public bool gameIsEnding { get; private set; }

    PlayerCharacterController m_Player;


    void Start()
    {
        m_Player = FindObjectOfType<PlayerCharacterController>();
        DebugUtility.HandleErrorIfNullFindObject<PlayerCharacterController, GameFlowManager>(m_Player, this);   
    }

    void Update()
    {
        
    }

    void EndGame(bool win)
    {
        
    }
}
