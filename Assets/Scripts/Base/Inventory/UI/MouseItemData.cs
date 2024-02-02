using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class MouseItemData : MonoBehaviour
    {
        [SerializeField] private Image _itemSprite;
        [SerializeField] private TextMeshProUGUI _textItemAmount;
        
        public InventorySlot AssingedInventorySlot { get; private set; }
        
        private void Start()
        {
            _itemSprite.color = Color.clear;
            _textItemAmount.text = string.Empty;
            AssingedInventorySlot = new InventorySlot();
        }

        public void UpdateMouseSlot(InventorySlot slot)
        {
            AssingedInventorySlot.AssignItem(slot);
            _itemSprite.sprite = slot.GetItemData.GetIcon;
            _textItemAmount.text = slot.GetStackSize.ToString();
            _itemSprite.color = Color.white;
        }

        private void Update()
        {
            if (AssingedInventorySlot.GetItemData is null)
            {
                return;
            }

            transform.position = Input.mousePosition;
            
            //Rewrite input system
            if ((Input.GetMouseButtonDown(0)) && (!IsPointerOverUIObject()))
            {
                ClearSlot();
            }
        }

        public void ClearSlot()
        {
            AssingedInventorySlot.ClearSlot();
            _textItemAmount.text = string.Empty;
            _itemSprite.color = Color.clear;
            _itemSprite.sprite = null;
        }

        private static bool IsPointerOverUIObject()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    } 
}