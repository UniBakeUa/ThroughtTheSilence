using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryTurnOn : MonoBehaviour
{
    bool isOn = false;
    [SerializeField] InputActionReference inventoryAction;
    [SerializeField] GameObject InventoryUI;
    private void OnEnable()
    {
        inventoryAction.action.performed += Turn;
    }
    private void OnDisable()
    {
        inventoryAction.action.performed -= Turn;
    }
    void Turn(InputAction.CallbackContext ctx) 
    {
        isOn = !isOn;
        PlayerLook.canLook = !isOn;
        InventoryUI.SetActive(isOn);
        Cursor.lockState = isOn ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOn;
    }
}
