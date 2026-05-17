using Microsoft.Xna.Framework;
using StardewValley;
using FarmInfoOverlay.Rendering;

namespace FarmInfoOverlay.Handlers
{
  public class AnimalHandler
  {
    public IEnumerable<OverlayItem> GetOverlays(GameLocation location)
    {
      foreach (var animal in location.animals.Values)
      {
        // OK — não mostra, é o estado normal
        bool hasProduce = animal.currentProduce.Value != null;
        bool isSad = animal.happiness.Value < 100;

        if (!hasProduce && !isSad) continue;

        yield return new OverlayItem
        {
          TilePosition = new Vector2(
                (int)(animal.Position.X / Game1.tileSize),
                (int)(animal.Position.Y / Game1.tileSize)
            ),
          Label = hasProduce ? "✓" : "!",
          BorderColor = hasProduce ? Color.LightGreen : Color.OrangeRed
        };
      }
    }
  }
}