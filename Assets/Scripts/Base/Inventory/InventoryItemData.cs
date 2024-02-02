using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Inventory system/New inventory item")]
    public class InventoryItemData : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _title;
        [TextArea, SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _maxStackSize;

        #region [PublicVars]
        
        public int GetId => _id;
        public string GetTitle => _title;
        public string GetDescription => _description;
        public Sprite GetIcon => _icon;
        public int GetMaxStackSize => _maxStackSize;
        
        #endregion
    }
}