
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
    private static EnemyController enemyController;
    private static readonly HashSet<IGameDataChangeListener> GAMEDATA_CHANGE_LISTENERS = new HashSet<IGameDataChangeListener>();
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
        data = new GameData();
        currentState = new GameState();
        enemyController = new EnemyController();
        ILevelBuilder generator = new LevelFromImage();

        data.level = generator.Generate();
        data.SpawnPlayer();

        foreach (IGameDataChangeListener listener in GAMEDATA_CHANGE_LISTENERS)
        {
            listener.OnGameDataChange(data);
        }
    }

    public static void Add(IGameDataChangeListener listener)
    {
        GAMEDATA_CHANGE_LISTENERS.Add(listener);
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

    }
    public static GameData GetData()
    {
        return data;
    }
    public static IBaseController GetEnemyController()
    {
        return enemyController;
    }
    public static void SetData(GameData value)
    {
        data = value;
    }

}