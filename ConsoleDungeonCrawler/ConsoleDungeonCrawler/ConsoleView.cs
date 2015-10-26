
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ConsoleView : IBaseView
{

    public ConsoleView()
    {
    }

    public bool score;
    public bool hud;
    public IConsoleRenderer currentRenderer;

    public void Build()
    {
        // TODO implement here
    }

    public void BuildInvetory()
    {
        // TODO implement here
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}