using UnityEngine;
using UnityEngine.UI;

public class SaveAllData : MonoBehaviour
{
    public AudioSource audioSource;
    public void SaveDataGoldPlayer()
    {
        if (Player.Instance != null)
            SaveGame.Instance.saveData.goldPlayer[0] = Player.Instance.CurrentGold;
        SaveGame.Instance.saveData.goldPlayer[1] = SaveGame.Instance.saveData.goldPlayer[0] + SaveGame.Instance.saveData.goldPlayer[1];
        SaveGame.Instance.saveData.goldPlayer[0] = 0;
        SaveGame.Instance.Save();
        Debug.Log("Save data");
    }

    public void ResetData()
    {
        SaveGame.Instance.saveData.goldPlayer[0] = 0;
        SaveGame.Instance.saveData.goldPlayer[1] = 0;
        SaveGame.Instance.saveData.bloodItem[0, 0] = 0;
        SaveGame.Instance.saveData.bloodItem[0, 1] = 0;
        SaveGame.Instance.saveData.bloodItem[0, 2] = 0;


        SaveGame.Instance.saveData.checkFill[0] = true;
        SaveGame.Instance.saveData.checkBuySkill[0] = false;
        SaveGame.Instance.saveData.checkBuySkill[1] = false;
        SaveGame.Instance.saveData.checkBuySkill[2] = false;

        SaveGame.Instance.saveData.scoreMana[0] = 50;
        SaveGame.Instance.saveData.scoreShield[0] = 0;

        SaveGame.Instance.saveData.currentLevel[0] = 0;
        SaveGame.Instance.Save();
    }
    public void SaveAllDataGame()
    {
        //Player
        SaveGame.Instance.saveData.scoreBlood[0] = Player.Instance.CurrentScoreHeal;
        SaveGame.Instance.saveData.scoreMana[0] = Player.Instance.CurrentScoreMana;
        SaveGame.Instance.saveData.scoreShield[0] = Player.Instance.CurrentScoreShield;

        //Enemy
        SaveGame.Instance.saveData.scoreBlood[1] = Enemy.Instance.CurrentScoreHeal;
        SaveGame.Instance.saveData.scoreMana[1] = Enemy.Instance.CurrentScoreMana;
        SaveGame.Instance.saveData.scoreShield[1] = Enemy.Instance.CurrentScoreShield;
        SaveGame.Instance.Save();
    }
    public void SaveDataPlayer()
    {
        SaveGame.Instance.saveData.scoreBlood[0] = Player.Instance.CurrentScoreHeal;
        SaveGame.Instance.saveData.scoreMana[0] = Player.Instance.CurrentScoreMana;
        SaveGame.Instance.saveData.scoreShield[0] = Player.Instance.CurrentScoreShield;
        SaveGame.Instance.Save();
    }

    // public void LoadAllDataGame()
    // {
    //     SaveGame.Instance.Load();
    //     Player.Instance.CurrentScoreHeal = SaveGame.Instance.saveData.scoreBlood[0];
    //     Player.Instance.CurrentScoreMana = SaveGame.Instance.saveData.scoreMana[0];
    //     Player.Instance.CurrentScoreShield = SaveGame.Instance.saveData.scoreShield[0];

    //     Enemy.Instance.CurrentScoreHeal = SaveGame.Instance.saveData.scoreBlood[1];
    //     Enemy.Instance.CurrentScoreMana = SaveGame.Instance.saveData.scoreMana[1];
    //     Enemy.Instance.CurrentScoreShield = SaveGame.Instance.saveData.scoreShield[1];
    // }

    public void LoadDataPlayer()
    {
        SaveGame.Instance.Load();
        Player.Instance.CurrentScoreHeal = SaveGame.Instance.saveData.scoreBlood[0];
        Player.Instance.CurrentScoreMana = SaveGame.Instance.saveData.scoreMana[0];
        Player.Instance.CurrentScoreShield = SaveGame.Instance.saveData.scoreShield[0];
    }
}
