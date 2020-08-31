using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{

    [SerializeField]
    private uint _price;
    [SerializeField]
    private Item[] _items;
    public Item _item;
    [SerializeField]
    private Text _textButtonBuy, _textCoins;

    public void ChangeTextOnBuyItem()
    {
        _textButtonBuy.text = "BUY";
    }

    public void ChangeTextOnUsedItem()
    {
        _textButtonBuy.text = "USED";
        
    }
    public void ChangeTextOnUseItem()
    {
        _textButtonBuy.text = "USE";

    }

    public void OnClickButton()
    {
        if (_item)
        {
            if (_item._bought)
            {
                if (_item._used)
                {

                }
                else
                {
                    ChangeTextOnUsedItem();
                    SwitchUsedOnUnused();
                    _item._used = true;
                }
            }
            else
            {
                if (_price >= _item._price)
                {
                    ChangeTextOnUseItem();    
                    _price -= _item._price;
                    _item._bought = true;
                    GetPriceOnText();
                }
                
            }
        } 
    }

    private void SwitchUsedOnUnused()
    {
        foreach (var item in _items)
        {
            item._used = false;
        }
    }
    private void Start()
    {
        GetPriceOnText();
    }

    private void GetPriceOnText()
    {
        _textCoins.text = _price.ToString();
    }
}
