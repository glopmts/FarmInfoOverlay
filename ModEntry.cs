using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using FarmInfoOverlay.Handlers;
using FarmInfoOverlay.Rendering;

namespace FarmInfoOverlay
{
  public class ModEntry : Mod
  {
    private ModConfig Config = new();
    private OverlayRenderer Renderer = null!;

    public override void Entry(IModHelper helper)
    {
      Config = helper.ReadConfig<ModConfig>();
      Renderer = new OverlayRenderer(Monitor, Config);

      helper.Events.Display.RenderedWorld += OnRenderedWorld;
      helper.Events.Input.ButtonPressed += OnButtonPressed;

      Monitor.Log("FarmInfoOverlay carregado com sucesso!", LogLevel.Info);
    }

    private void OnRenderedWorld(object? sender, RenderedWorldEventArgs e)
    {
      if (!Context.IsWorldReady) return;

      var location = Game1.currentLocation;
      Renderer.Render(e.SpriteBatch, location);

      // Renderiza objetos dentro dos buildings visíveis na tela
      if (location is Farm farm)
      {
        foreach (var building in farm.buildings)
        {
          var interior = building.GetIndoors();
          if (interior != null)
            Renderer.Render(e.SpriteBatch, interior, building);
        }
      }
    }

    private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
    {
      if (e.Button == Config.ToggleKey && Context.IsWorldReady)
      {
        Config.Enabled = !Config.Enabled;
        Helper.WriteConfig(Config);
        Monitor.Log($"Overlay {(Config.Enabled ? "ativado" : "desativado")}", LogLevel.Info);
      }
    }
  }
}