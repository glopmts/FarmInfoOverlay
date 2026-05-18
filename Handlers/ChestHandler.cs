using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;
using FarmInfoOverlay.Rendering;

namespace FarmInfoOverlay.Handlers
{
  public class ChestHandler
  {
    private static readonly HashSet<string> Blacklist = new()
        {
            "Mini-Fridge",
            "Mini-Shipping Bin",
            "Auto-Grabber",
            "Junimo Chest"
        };

    public IEnumerable<OverlayItem> GetOverlays(GameLocation location)
    {
      foreach (var obj in location.objects.Values)
      {
        if (obj is not Chest chest) continue;
        if (Blacklist.Contains(chest.name)) continue;

        if (chest.SpecialChestType != Chest.SpecialChestTypes.None &&
            chest.SpecialChestType != Chest.SpecialChestTypes.BigChest)
          continue;

        int capacity = chest.GetActualCapacity();
        if (capacity <= 0) continue;

        int used = chest.Items.Count(i => i != null);

        // Não mostra baús completamente vazios
        if (used == 0) continue;

        var pct = (float)used / capacity;
        var color = pct >= 1f ? Color.OrangeRed
                  : pct >= 0.8f ? Color.Yellow
                  : Color.White;

        yield return new OverlayItem
        {
          TilePosition = obj.TileLocation,
          Label = $"{used}/{capacity}",
          BorderColor = color
        };
      }
    }
  }
}