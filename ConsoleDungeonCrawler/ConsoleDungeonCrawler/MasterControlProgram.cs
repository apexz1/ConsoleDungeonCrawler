
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class MasterControlProgram : IGameDataChangeListener, IGameStateChangeListener
{

    public MasterControlProgram()
    {
    }

    public GameData model;
    public IBaseView view;
    public IBaseController controller;

    public void Run()
    {
        Console.WriteLine("MCP initating...");
        Console.WriteLine("MCP; initiation completed.");
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
        throw new NotImplementedException();
    }

    public void OnGameStateChange()
    {
        throw new NotImplementedException();
    }
}