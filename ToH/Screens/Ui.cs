﻿using ToH.Data;
using ToH.Log;

namespace ToH.Screens;

public class Ui : IUi, IObserver
{
    private readonly Controller _controller;
    private readonly ILog _log;
    private Screen _screen;

    public Screen Screen
    {
        private get => _screen;
        set
        {
            _screen = value;
            _screen.Init();
        }
    }
    public IScreenFactory ScreenFactory { get; }

    public Ui(Controller controller, Screen? screen, ILog log, IScreenFactory screenFactory)
    {
        Screen = screen;
        ScreenFactory = screenFactory;
        _controller = controller;
        _log = log;
        _controller.Add(this);
    }

    public void Update()
    {
        switch (_controller.Action)
        {
            case Action.None:
                Screen.None(this);
                break;
            case Action.Down:
                Screen.Down(this);
                break;
            case Action.Up:
                Screen.Up(this);
                break;
            case Action.Enter:
                Screen.Enter(this);
                break;
            case Action.Escape:
                Screen.Escape(this);
                break;
            default:
                _log.Log($"Unhandled action: {_controller.Action}");
                break;
        }
    }
}