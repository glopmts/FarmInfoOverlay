using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;
using FarmInfoOverlay.Rendering;

namespace FarmInfoOverlay.Handlers
{
  public class ChestHandler
  {
    public IEnumerable<OverlayItem> GetOverlays(GameLocation location)
    {
      foreach (var obj in location.objects.Values)
      {
        if (obj is not Chest chest) continue;

        int used = chest.Items.Count(i => i != null);
        int total = chest.GetActualCapacity();

        var pct = total > 0 ? (float)used / total : 0f;
        var color = pct >= 1f ? Color.OrangeRed
                  : pct >= 0.8f ? Color.Yellow
                  : Color.White;

        yield return new OverlayItem
        {
          TilePosition = obj.TileLocation,
          Label = $"{used}/{total}",
          BorderColor = color
        };
      }
    }
  }
}