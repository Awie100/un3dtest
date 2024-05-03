using System.Collections;
using System.Collections.Generic;

public class Game
{
    private static Game instance;

    public delegate void OnFire();
    public static OnFire onFire;

    private Game() { }

    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Game();
            }

            return instance;
        }
    }


}
