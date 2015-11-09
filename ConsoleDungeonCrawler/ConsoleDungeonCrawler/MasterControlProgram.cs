
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class MasterControlProgram : IGameDataChangeListener, IGameStateChangeListener
{

    public MasterControlProgram()
    {
        turn = 0;
        Application.Add((IGameDataChangeListener)this);
    }

    public static bool running = true;
    public GameData data;
    public IBaseView view;
    private static IBaseController controller;

    private int turn = -1;

    public void Run()
    {
        Application.NewGame();

        while (running)
        {
            controller.Execute();
            if (ConsolePlayerController.done && EnemyController.done)
            {
                EndTurn();
            }

            view.Execute();
        }
    }

    public void Save()
    {
        // TODO implement here
    }

    public void Load()
    {
        // TODO implement here
    }

    public void GetCurrentState()
    {
        // TODO implement here
    }

    public void OnGameDataChange(GameData data)
    {
        this.data = data;
    }

    public void OnGameStateChange()
    {
        throw new NotImplementedException();
    }
    public void EndTurn()
    {
        turn++;
        data.player.actions = data.player.maxActions;
        for (int i = 0; i < data.collision.Count; i++)
        {
            data.collision[i].actions = data.collision[i].maxActions;
        }

        ConsolePlayerController.done = false;
        EnemyController.done = false;
    }

    public static void SetController(IBaseController c)
    {
        controller = c;
    }
}