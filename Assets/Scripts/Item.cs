using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private StoreManager _storeManager;
    [SerializeField]
    public uint _price;
    [SerializeField]
    public bool _used;
    public bool _bought;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(GetInfo);
    }
    public void GetInfo()
    {
        _storeManager._item = this;
        if (CheckItemOnBought())
        {
            if (_used)
            {
                _storeManager.ChangeTextOnUsedItem();
            }
            else
            {
                _storeManager.ChangeTextOnUseItem();
            }
        }
        else
        {
            _storeManager.ChangeTextOnBuyItem();
        }
    }
    private bool CheckItemOnBought()
    {
        if (_bought)
            return false;

        return true;
    }
}
