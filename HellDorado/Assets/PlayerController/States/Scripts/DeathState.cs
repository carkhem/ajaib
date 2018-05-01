using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Death")]
public class DeathState : State
{

    private PlayerController _controller;

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;

    }

    // Use this for initialization
    void Start()
    {

    }

    public override void Enter()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        Restart();
    }

    private void Restart()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            GameManager.instance.Respawn();
            _controller.TransitionTo<GroundState>();
        }

    }
}
