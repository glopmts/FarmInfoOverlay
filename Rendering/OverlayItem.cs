using Microsoft.Xna.Framework;

namespace FarmInfoOverlay.Rendering
{
  /// Representa um tooltip a ser desenhado na tela
  public class OverlayItem
  {
    /// Posição em coordenadas de TILE do jogo
    public Vector2 TilePosition { get; set; }

    /// Texto principal (ex: "120/240")
    public string Label { get; set; } = "";

    /// Texto secundário opcional (ex: "Feno")
    public string? Subtitle { get; set; }

    /// Cor da borda do tooltip (identifica o tipo)
    public Color BorderColor { get; set; } = Color.White;

    /// Ícone opcional (sprite index no spritesheet)
    public int? IconIndex { get; set; }
  }
}