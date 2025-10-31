using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedstoneinventeGameStudio
{
    public class SlotManager : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
#nullable enable
        public InventoryItemData? itemData;
        public int stackCount;
        public bool isOccupied;
#nullable disable

        [SerializeField] bool useAsDrag;
        [SerializeField] GameObject emptyCard;

        [SerializeField] TMP_Text itemName;
        [SerializeField] Image itemIcon;

        private void Awake()
        {
            if (useAsDrag)
            {
                ItemDraggingManager.dragCard = this;
                isOccupied = true;

                gameObject.SetActive(false);
            }

            if (itemData == null)
            {
                RefreshDisplay();
            }
            else
            {
                SetItem(itemData);
            }
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (useAsDrag || !isOccupied)
            {
                return;
            }

            ItemDraggingManager.fromCard = this;
            TooltipManagerInventory.UnSetToolTip();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isOccupied)
            {
                ItemDraggingManager.toCard = ItemDraggingManager.fromCard;

                if (ItemDraggingManager.toCard == default)
                {
                    TooltipManagerInventory.SetTooltip(itemData);
                }

                return;
            }

            ItemDraggingManager.toCard = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isOccupied)
            {
                return;
            }

            TooltipManagerInventory.UnSetToolTip();
        }

        public bool SetItem(InventoryItemData itemData)
        {
            if(this.itemData.name == itemData.name) 
            {
                int rest = 
                stackCount += itemData.currentStackSize;
                itemData.currentStackSize -= itemData.currentStackSize;
                RefreshDisplay();
                return true;
            }
            if ((isOccupied && !useAsDrag) || itemData == null)
            {
                return false;
            }

            this.itemData = itemData;
            this.itemIcon.sprite = itemData.itemIcon;
            this.stackCount = itemData.currentStackSize;
            this.itemName.text = itemData.name + stackCount;
            this.isOccupied = true;

            RefreshDisplay();

            return true;
        }

        public void UnSetItem()
        {
            itemData = null;
            this.itemName.text = null;
            this.itemIcon.sprite = null;
            this.isOccupied = false;
            RefreshDisplay();
        }

        void RefreshDisplay()
        {
            emptyCard.SetActive(!isOccupied);
        }
    }

}