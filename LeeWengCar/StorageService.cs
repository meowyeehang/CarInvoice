namespace LeeWengCar.Data  // <--- Check this name!
{
    public class InventoryItem
    {
        public string Name { get; set; } = "";
        public int Quantity { get; set; }
    }

    public class StorageService
    {
        private const string StorageKey = "rong_motor_inventory";
        public List<InventoryItem> Inventory { get; private set; } = new();

        public StorageService() { LoadData(); }

        public void AddItem(string name, int qty)
        {
            var existing = Inventory.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());
            if (existing != null) existing.Quantity += qty;
            else Inventory.Add(new InventoryItem { Name = name, Quantity = qty });
            SaveData();
        }

        public void ReduceStock(string name, int qty)
        {
            // StringComparison.OrdinalIgnoreCase makes "Engine Oil" match "engine oil"
            var item = Inventory.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                item.Quantity -= qty;
                if (item.Quantity < 0) item.Quantity = 0;
                SaveData();
            }
        }

        public void SaveData() => Preferences.Default.Set(StorageKey, System.Text.Json.JsonSerializer.Serialize(Inventory));

        private void LoadData()
        {
            string json = Preferences.Default.Get(StorageKey, "");
            if (!string.IsNullOrEmpty(json))
                Inventory = System.Text.Json.JsonSerializer.Deserialize<List<InventoryItem>>(json) ?? new();
        }
    }
}