using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
public class ShopManager : MonoBehaviour
{
    protected int goldPlayer;
    protected TextMeshProUGUI txtGold;
    protected Button bntTest;
    protected Item item;
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
        StartCoroutine(Delay());
        SceneManager.LoadScene("Menu");
    }
    protected virtual void BuyItem(int index, int itemIndex)
    {
        if (goldPlayer >= this.item.Price)
        {
            AudioManager.Instance.audioSrc.PlayOneShot(AudioManager.Instance.SoundBuyItem);
            this.goldPlayer -= this.item.Price;
            this.SaveGoldBuyPlayer();
            this.LoadGold();
            SaveGame.Instance.saveData.bloodItem[index, itemIndex]++;
            SaveGame.Instance.Save();
            Debug.Log(SaveGame.Instance.saveData.bloodItem[index, itemIndex]);
        }
        else
        {
            AudioManager.Instance.audioSrc.PlayOneShot(AudioManager.Instance.SoundBuyFalse);
        }
    }

    protected virtual void BuySkill(int index, bool isCheck)
    {
        // Debug.Log(goldPlayer);
        // Debug.Log(this.item.Price);
        if (goldPlayer >= this.item.Price)
        {
            AudioManager.Instance.audioSrc.PlayOneShot(AudioManager.Instance.SoundBuyItem);
            this.goldPlayer -= this.item.Price;
            this.SaveGoldBuyPlayer();
            this.LoadGold();
            SaveGame.Instance.saveData.checkBuySkill[index] = isCheck;
            SaveGame.Instance.Save();
            Debug.Log(SaveGame.Instance.saveData.checkBuySkill[index]);
        }
        else
        {
            AudioManager.Instance.audioSrc.PlayOneShot(AudioManager.Instance.SoundBuyFalse);
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

    private IEnumerator Delay()
    {
        AudioManager.Instance.audioSrc.PlayOneShot(AudioManager.Instance.SoundClick);
        yield return new WaitForSeconds(0.3f);
    }
}
