using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class ShopManager : MonoBehaviour
{
    protected int goldPlayer;
    protected TextMeshProUGUI txtGold;
    protected Button bntTest;
    protected Item item;
    public SaveAllData saveAllData;
    public int GoldPlayer { get => goldPlayer; set => goldPlayer = value; }

    void Start()
    {
        this.LoadGold();
    }

    protected virtual void LoadGold()
    {
        if (txtGold != null)
            this.txtGold.text = this.goldPlayer.ToString();
    }

    protected virtual void CheckClick()
    {
        SceneManager.LoadScene("Menu");
    }
    protected virtual void BuyItem(int index, int itemIndex)
    {
        if (goldPlayer >= this.item.Price)
        {
            SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundBuyItem);
            this.goldPlayer -= this.item.Price;
            this.SaveGoldBuyPlayer();
            this.LoadGold();
            SaveGame.Instance.saveData.bloodItem[index, itemIndex]++;
            SaveGame.Instance.Save();
            Debug.Log(SaveGame.Instance.saveData.bloodItem[index, itemIndex]);
        }
        else
        {
            SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundBuyFalse);
        }
    }

    protected virtual void BuySkill(int index, bool isCheck)
    {
        if (goldPlayer >= this.item.Price)
        {
            SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundBuyItem);
            this.goldPlayer -= this.item.Price;
            this.SaveGoldBuyPlayer();
            this.LoadGold();
            SaveGame.Instance.saveData.checkBuySkill[index] = isCheck;
            SaveGame.Instance.Save();
            Debug.Log(SaveGame.Instance.saveData.checkBuySkill[index]);
        }
        else
        {
            SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundBuyFalse);
        }
    }



    protected virtual void SaveGoldBuyPlayer()
    {
        SaveGame.Instance.saveData.goldPlayer[1] = goldPlayer;
        Debug.Log("Save Price");
    }

    protected virtual void LoadDataGold()
    {
        SaveGame.Instance.Load();
        this.goldPlayer = SaveGame.Instance.saveData.goldPlayer[1];
    }
}
