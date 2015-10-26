
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Application
{
    public Application()
    {
    }

    private static GameData data;
    private static IBaseState currentState;
    private static Dictionary<GameStates, IBaseState> STATE_ARCHIVE;

    public static void Load(string filename)
    {
        // TODO implement here
    }

    public static void Save(string filename)
    {
        // TODO implement here
    }

    public static void NewGame()
    {
        // TODO implement here
    }

    public static void Add(IGameDataChangeListener listener)
    {
        // TODO implement here
    }

    public static void Remove(IGameDataChangeListener listener)
    {
        // TODO implement here
    }

    public static void Add(IGameStateChangeListener listener)
    {
        // TODO implement here
    }

    public static void Remove(IGameStateChangeListener listener)
    {
        // TODO implement here
    }

    public static void ChangeGameState(GameStates state)
    {
        // TODO implement here
    }

    public static void ChangeGameData(GameData data)
    {
        // TODO implement here
    }

}