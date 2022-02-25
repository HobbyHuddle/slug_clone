using DataModels;
using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AmmoStatusPanel : MonoBehaviour
    {
        public Item item;
        public Image ammoIcon;

        private void Start()
        {
            ammoIcon.sprite = item.icon;
        }

        public void UpdateItem(RangedWeapon newItem)
        {
            item = newItem;
            ammoIcon.sprite = item.icon;
        }
    }
}