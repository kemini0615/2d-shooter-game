# 2D Shooter Game

## Scenes

### Game Scene

- @UIManager
    - [UIManager]
- @LevelManager
    - [LevelManager]
- Player
    - [PlayerMovement]
    - [PlayerHealth]
    - [PlayerEnergy]
    - [PlayerWeapon]
    - [Rigidbody2D]
        - Constraints > Freeze Rotation > Z: On
    - [CircleCollider2D]
    - PlayerRenderer
        - [SpriteRenderer]
            - Additional Settings > Sorting Layer: Player
            - Addtional Settings > Order in Layer: 10
        - [Animator]
    - PlayerCircleRenderer
        - [SpriteRenderer]
            - Additional Settings > Sorting Layer: Player
    - WeaponPosition
    - Main Camera
        - [Camera]
            - Environment > Background Type: Solid Color
            - Sorting Layer: UI
- Canvas
    - [Canvas]
        - Render Mode: Screen Space - Camera
    - [CanvasScaler]
        - UI Scale Mode: Scale With Screen Size
    - PlayerPanelBorder
        - [RectTransform]
            - Pos: (50, -50, 0)
            - Width: 470, Height: 320
            - Anchors > Min: (0, 1)
            - Anchors > Max: (0, 1)
            - Pivot: (0, 1)
        - [Image]
            - Color: (0, 0, 0, 255)
        - [VerticalLayoutGroup]
            - Spacing: 10
            - Child Alignment: Middle Center
    - FadePanel
        - [Image]
            - Color: (0, 0, 0, 255)
        - [CanvasGroup]
            - Alpha: 0
- EventSystem