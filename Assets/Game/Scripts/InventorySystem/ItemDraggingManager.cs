using UnityEngine;
using UnityEngine.InputSystem;

namespace RedstoneinventeGameStudio
{
    public class ItemDraggingManager : MonoBehaviour
    {
        public static CardManager dragCard;

        public static CardManager fromCard;
        public static CardManager toCard;

        [SerializeField] Vector3 tooltipOffset;
        [SerializeField] Vector3 draggingCardOffset;

        private void Update()
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame && fromCard != default)
            {
                if (toCard != default)
                {
                    toCard.SetItem(dragCard.itemData);
                }
                else if (fromCard != default)
                {
                    fromCard.SetItem(dragCard.itemData);
                }

                toCard = default;
                fromCard = default;

                dragCard.gameObject.SetActive(false);
            }

            if (Mouse.current.leftButton.wasPressedThisFrame && fromCard != default)
            {
                dragCard.SetItem(fromCard.itemData);
                fromCard.UnSetItem();

                dragCard.gameObject.SetActive(true);
            }

            dragCard.transform.position = Mouse.current.position.value + (Vector2)draggingCardOffset;
            TooltipManagerInventory.instance.transform.position = Mouse.current.position.value + (Vector2)tooltipOffset;
        }
    }

}