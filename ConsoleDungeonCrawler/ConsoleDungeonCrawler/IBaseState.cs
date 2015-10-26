
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IBaseState
{

    void Enter();

    void Exit();

    void Execute();

}