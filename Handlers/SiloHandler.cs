using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Buildings;
using FarmInfoOverlay.Rendering;

namespace FarmInfoOverlay.Handlers
{
  public class SiloHandler
  {
    public IEnumerable<OverlayItem> GetOverlays(GameLocation location)
    {
      if (location is not Farm farm)
        yield break;

      foreach (var building in farm.buildings)
      {
        // 1.6+: Silo não é mais uma subclasse, checar pelo tipo
        if (building.buildingType.Value != "Silo") continue;

        int current = farm.GetHayCapacity();
        int max = farm.GetHayCapacity();

        var pct = max > 0 ? (float)current / max : 0f;
        var color = pct < 0.25f ? Color.OrangeRed
                  : pct < 0.75f ? Color.Yellow
                  : Color.LightGreen;

        yield return new OverlayItem
        {
          TilePosition = new Vector2(building.tileX.Value, building.tileY.Value),
          Label = $"{current}/{max}",
          Subtitle = "Feno",
          BorderColor = color
        };
      }
    }
  }
}