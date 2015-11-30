
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameState : IBaseState
{
    private GameStates state = new GameStates();

    public GameState()
    {
    }

    public GameStates Get()
    {
        return state;
    }

    public void Enter(GameStates state)
    {
        this.state = state;

        if (state == GameStates.GAME) MasterControlProgram.SetController(new ConsolePlayerController());
        if (state == GameStates.MENU && state == GameStates.MAPS) MasterControlProgram.SetController(new ConsoleMenuController());
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void GetEnemies()
    {
        // TODO implement here
    }

    public void GetPlayer()
    {
        // TODO implement here
    }

    public void Update()
    {
        // TODO implement here
    }
}