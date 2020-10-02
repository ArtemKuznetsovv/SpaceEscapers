using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public enum ElevatorState
    {
        Down,
        MovingUp,
        MovingDown,
        Up
    }

    public ElevatorState m_CurrentState = ElevatorState.Down;

    public Animator Level0_Door;
    public Animator Level2_Door;
    public Animator Elevator_Door;

    public float ElevatorSpeed;
    public static readonly float LoopTime = 30;
    private float TimerUp = 1;
    private float TimerDown = LoopTime + 1;
    private static Vector3 TargetDown = new Vector3(20f, -2f, 4.7f);
    private static Vector3 TargetUp = new Vector3(20f, 23f, 4.7f);



    private bool startTimer(ref float i_Timer)
    {
        bool timerEnded = false;
        if (i_Timer > 0)
        {
            i_Timer -= Time.deltaTime;
        }
        else
        {
            timerEnded = true;
        }

        return timerEnded;
    }

    private void handleMovement(Vector3 i_Target, ref bool i_ReachedTarget)
    {
        this.transform.position = Vector3.MoveTowards
            (this.transform.position, i_Target, ElevatorSpeed * Time.deltaTime);
        i_ReachedTarget = this.transform.position == i_Target;
    }

    private void handleDoors(Animator i_LevelDoor, Animator i_ElevatorDoor, bool i_DoorState)
    {
        i_LevelDoor.SetBool("OpenDoor", i_DoorState);
        i_ElevatorDoor.SetBool("OpenDoor", i_DoorState);
    }

    private bool handleElevatorWaiting(ref float i_Timer, Animator i_LevelDoor)
    {
        bool isTimerEnded = startTimer(ref i_Timer);
        bool openDoors;
        bool elevatorWaiting = true;
        if (isTimerEnded)
        {
            openDoors = false;
            i_Timer = LoopTime;
            handleDoors(i_LevelDoor, Elevator_Door, openDoors);
            elevatorWaiting = false;
        }

        return elevatorWaiting;
    }

    private bool handleElevatorMoving(Vector3 i_Target, Animator i_LevelDoor)
    {
        bool reachedTarget = false;
        bool openDoors;
        bool elevatorMoving = true;
        handleMovement(i_Target, ref reachedTarget);
        if (reachedTarget == true)
        {
            openDoors = true;
            handleDoors(i_LevelDoor, Elevator_Door, openDoors);
            elevatorMoving = false;
        }

        return elevatorMoving;
    }
    // Update is called once per frame
    void Update()
    {
        // TODO: Can do better:
        // 1) organize in a nice function
        // 2) operate only ONE timer
        // 3) Timer / Console?
        // 4) Stand on the door when close?
        // 5) Maybe ONE function to move (no moveUp and moveDown) and this function recive as paramater the direction
        switch (m_CurrentState)
        {
            case ElevatorState.Down:
                if (!handleElevatorWaiting(ref TimerUp, Level0_Door))
                {
                    m_CurrentState = ElevatorState.MovingUp;
                }

                break;

            case ElevatorState.MovingUp:
                if (!handleElevatorMoving(TargetUp, Level2_Door))
                {
                    m_CurrentState = ElevatorState.Up;
                }

                break;

            case ElevatorState.Up:
                if (!handleElevatorWaiting(ref TimerDown, Level2_Door))
                {
                    m_CurrentState = ElevatorState.MovingDown;
                }

                break;

            case ElevatorState.MovingDown:
                if (!handleElevatorMoving(TargetDown, Level0_Door))
                {
                    m_CurrentState = ElevatorState.Down;
                }

                break;
        }









        //private bool MoveUp()
        //{
        //    if (m_CurrentState == ElevatorState.Down)
        //    {
        //        if (TimerUp <= 0)
        //        {
        //            TimerUp = LoopTime;
        //            return true;
        //        }
        //        else
        //        {
        //            TimerUp -= Time.deltaTime;
        //        }
        //    }
        //    return false;
        //}

        //private bool MoveDown()
        //{
        //    if (m_CurrentState == ElevatorState.Up)
        //    {
        //        if (TimerDown <= 0)
        //        {
        //            TimerDown = LoopTime;
        //            return true;
        //        }
        //        else
        //        {
        //            TimerDown -= Time.deltaTime;
        //        }
        //    }
        //    return false;
        //}



        //if (MoveUp())
        //{
        //    Level0_Door.SetBool("OpenDoor", false);
        //    Elevator_Door.SetBool("OpenDoor", false);
        //    m_CurrentState = ElevatorState.MovingUp;
        //}
        //if (m_CurrentState == ElevatorState.MovingUp)
        //{
        //    this.transform.position = Vector3.MoveTowards(this.transform.position, TargetUp, ElevatorSpeed * Time.deltaTime);
        //    if (this.transform.position == TargetUp)
        //    {
        //        Elevator_Door.SetBool("OpenDoor", true);
        //        Level2_Door.SetBool("OpenDoor", true);
        //        m_CurrentState = ElevatorState.Up;
        //    }
        //}
        //if (MoveDown())
        //{
        //    Level2_Door.SetBool("OpenDoor", false);
        //    Elevator_Door.SetBool("OpenDoor", false);
        //    m_CurrentState = ElevatorState.MovingDown;
        //}
        //if (m_CurrentState == ElevatorState.MovingDown)
        //{
        //    this.transform.position = Vector3.MoveTowards(this.transform.position, TargetDown, ElevatorSpeed * Time.deltaTime);
        //    if (this.transform.position == TargetDown)
        //    {
        //        Elevator_Door.SetBool("OpenDoor", true);
        //        Level0_Door.SetBool("OpenDoor", true);
        //        m_CurrentState = ElevatorState.Down;
        //    }
        //}
    }
}
