
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class MasterControlProgram : IGameDataChangeListener, IGameStateChangeListener
{

    public MasterControlProgram()
    {
        Application.Add((IGameDataChangeListener)this);
    }

    public static bool control = true;
    public GameData data;
    public IBaseView view;
    public IBaseController controller;

    public void Run()
    {
        Application.NewGame();

        while(control)
        {
            controller.Execute();
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


}