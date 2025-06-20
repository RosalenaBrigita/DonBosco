using Inventory.Model;
using Inventory.UI;
using DonBosco.Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DonBosco.SaveSystem;
using DonBosco;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryController : MonoBehaviour, ISaveLoad
    {
        [SerializeField]
        public UIInventoryPage inventoryUI;
        public Button button;

        [SerializeField]
        private InventorySO inventoryData;

        [SerializeField] private GameObject itemAddedPanelPrefab;
        [SerializeField] private Transform panelParent;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField]
        private AudioClip dropClip;

        [SerializeField]
        private AudioSource audioSource;

        private bool isDialogueActive = false;

        public static InventoryController Instance { get; private set; } 

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);  // Hindari duplikasi instance
            }
        }

        private void Start()
        {
            SaveManager.Instance.Subscribe(this);
            SaveManager.Instance.LoadEquippedItems(gameObject);
            PrepareUI();
            button.gameObject.SetActive(false); 
            PrepareInventoryData();

            // Pastikan ada instance dari DialogueManager
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            if (dialogueManager != null)
            {
                dialogueManager.OnDialogueStarted += () => isDialogueActive = true;
                dialogueManager.OnDialogueEnded += () => isDialogueActive = false;
            }
            else
            {
                Debug.LogError("DialogueManager not found in scene!");
            }
        }

        private void OnDestroy()
        {
            SaveManager.Instance.Unsubscribe(this);
        }

        private void Update()
        {
            if (DonBosco.InputManager.Instance != null && DonBosco.InputManager.Instance.GetInventoryPressed())
            {
                if (!isDialogueActive) // Cek apakah dialog sedang berjalan
                {
                    ToggleInventory();
                }
            }
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            if (inventoryUI == null)
            {
                Debug.LogError("Inventory UI is not initialized!");
                return;
            }

            inventoryUI.ResetAllItems();

            foreach (var item in inventoryState)
            {
                if (item.Value.item != null) // Hindari akses ke item yang null
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
                else
                {
                    Debug.LogWarning($"Item di slot {item.Key} kosong atau rusak.");
                }
            }
        }


        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {

                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }
        }

        private void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
            audioSource.PlayOneShot(dropClip);
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                audioSource.PlayOneShot(itemAction.actionSFX);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage,
                item.name, description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} " +
                    $": {inventoryItem.itemState[i].value} / " +
                    $"{inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private void ToggleInventory()
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                button.gameObject.SetActive(true); 

                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }

                // Matikan input gerak
                //DonBosco.InputManager.Instance.SetMovementActionMap(false);
            }
        }

        public void HideInventory()
        {
            inventoryUI.Hide();
            button.gameObject.SetActive(false); 
        }

        public InventorySO GetInventorySO()
        {
            return inventoryData;
        }

        public void ShowItemAddedPanel(ItemSO item)
        {
            if (itemAddedPanelPrefab == null || panelParent == null)
            {
                Debug.LogWarning("ItemAddedPanelPrefab atau PanelParent belum diassign di Inspector!");
                return;
            }

            GameObject panelInstance = Instantiate(itemAddedPanelPrefab, panelParent);

            // Cari component di panel
            UnityEngine.UI.Image itemImage = panelInstance.transform.Find("ImageItem").GetComponent<UnityEngine.UI.Image>();
            TMPro.TMP_Text itemName = panelInstance.transform.Find("ItemName").GetComponent<TMPro.TMP_Text>();

            if (itemImage != null)
            {
                itemImage.sprite = item.ItemImage;
            }

            if (itemName != null)
            {
                itemName.text = item.Name;
            }

            // Auto destroy panel setelah beberapa detik
            StartCoroutine(HidePanelAfterDelay(panelInstance, 2f)); // 2 detik
        }

        private IEnumerator HidePanelAfterDelay(GameObject panel, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(panel);
        }

        public async Task Save(SaveData saveData)
        {
            saveData.inventoryItems = new List<InventoryItem>(inventoryData.GetCurrentInventoryState().Values);
            await Task.CompletedTask;
        }

        public async Task Load(SaveData saveData)
        {
            if (saveData.inventoryItems != null)
            {
                inventoryData.Initialize();
                foreach (var item in saveData.inventoryItems)
                {
                    inventoryData.AddItem(item);
                }
            }
            await Task.CompletedTask;
        }

    }
}