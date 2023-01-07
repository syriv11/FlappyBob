public abstract class GameState
{
    public Menu Menu { get; private set; }

    public void SetMenu(Menu menu) => Menu = menu;
}
