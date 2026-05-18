using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Menus;
using FarmInfoOverlay.Handlers;

namespace FarmInfoOverlay.Rendering
{
  public class OverlayRenderer
  {
    private readonly IMonitor Monitor;
    private readonly ModConfig Config;

    private readonly SiloHandler SiloHandler;
    private readonly ChestHandler ChestHandler;
    private readonly MachineHandler MachineHandler;

    public OverlayRenderer(IMonitor monitor, ModConfig config)
    {
      Monitor = monitor;
      Config = config;
      SiloHandler = new SiloHandler();
      ChestHandler = new ChestHandler();
      MachineHandler = new MachineHandler(config);
    }

    // Render para o local atual (fazenda externa, vila, etc.)
    public void Render(SpriteBatch batch, GameLocation interior, Building building)
    {
      if (!Config.Enabled) return;
      var offset = new Vector2(
          building.tileX.Value * Game1.tileSize,
          building.tileY.Value * Game1.tileSize
      );
      foreach (var item in CollectItems(interior, isInterior: true))
        DrawTooltip(batch, item, offset);
    }


    // Render para interior de um building — converte coordenadas internas para a tela
    public void Render(SpriteBatch batch, GameLocation location)
    {
      if (!Config.Enabled) return;
      foreach (var item in CollectItems(location, isInterior: false))
        DrawTooltip(batch, item, Vector2.Zero);
    }


    private List<OverlayItem> CollectItems(GameLocation location, bool isInterior = false)
    {
      var items = new List<OverlayItem>();

      if (Config.ShowSiloInfo)
        items.AddRange(SiloHandler.GetOverlays(location));

      // Chests e Máquinas só no local onde o jogador está
      // — não projetar de interiores para o mapa externo
      if (!isInterior)
      {
        if (Config.ShowChestInfo)
          items.AddRange(ChestHandler.GetOverlays(location));

        if (Config.ShowMachineStatus)
          items.AddRange(MachineHandler.GetOverlays(location));
      }

      return items;
    }

    private void DrawTooltip(SpriteBatch batch, OverlayItem item, Vector2 offset)
    {

      if (string.IsNullOrWhiteSpace(item.Label)) return;
      if (item.TilePosition.X <= 0 && item.TilePosition.Y <= 0) return;


      if (string.IsNullOrWhiteSpace(item.Label)) return;

      var worldPos = item.TilePosition * Game1.tileSize + offset;
      var screenPos = Game1.GlobalToLocal(worldPos);

      if (screenPos.X < -200 || screenPos.X > Game1.viewport.Width + 200) return;
      if (screenPos.Y < -200 || screenPos.Y > Game1.viewport.Height + 200) return;

      var font = Game1.smallFont;
      var scale = Config.OverlayScale;

      var labelSize = font.MeasureString(item.Label) * scale;
      int padX = 10, padY = 4;
      int boxW = (int)labelSize.X + padX * 2;
      int boxH = (int)labelSize.Y + padY * 2;

      float drawX = screenPos.X + Game1.tileSize / 2f - boxW / 2f;
      float drawY = screenPos.Y + Config.YOffset;

      IClickableMenu.drawTextureBox(
          batch,
          Game1.menuTexture,
          new Rectangle(0, 256, 60, 60),
          (int)drawX - 2, (int)drawY - 2,
          boxW + 4, boxH + 4,
          item.BorderColor * Config.OverlayOpacity,
          scale: 1f
      );

      batch.DrawString(
          font, item.Label,
          new Vector2(drawX + padX, drawY + padY),
          Color.White * Config.OverlayOpacity,
          0f, Vector2.Zero, scale,
          SpriteEffects.None, 1f
      );
    }
  }
}