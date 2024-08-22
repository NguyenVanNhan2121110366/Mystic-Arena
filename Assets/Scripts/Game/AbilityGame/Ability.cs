using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public Button ability;
    public int amoutMana;


    protected virtual void ExcuteAbility()
    {
        if (TurnController.Instance.CurrentTurn == GameTurn.Player)
            GameStateController.Instance.CurrentGameState = GameState.ExcuteAbility;
    }
    protected virtual bool CheckCanUseAbility()
    {
        if (TurnController.Instance.CurrentTurn == GameTurn.Player)
        {
            if (Player.Instance.CurrentScoreMana >= amoutMana)
                return true;
        }
        return false;
    }

    protected virtual void UpdateMana()
    {
        if (TurnController.Instance.CurrentTurn == GameTurn.Player)
        {
            Player.Instance.CurrentScoreMana -= amoutMana;
        }
    }
}
