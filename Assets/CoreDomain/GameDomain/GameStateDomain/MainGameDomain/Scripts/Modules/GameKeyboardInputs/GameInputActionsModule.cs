using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule
{
    public class GameInputActionsModule : IGameInputActionsModule
    {
        private readonly GameInputActions _gameInputActions;
        private readonly JumpInputInvokedCommand.Factory _jumpInputInvokedCommandFactory;
        private readonly ShootInputInvokedCommand.Factory _shootInputInvokedCommandFactory;

        public GameInputActionsModule(
            GameInputActions gameInputActions,
            JumpInputInvokedCommand.Factory jumpInputInvokedCommandFactory,
            ShootInputInvokedCommand.Factory shootInputInvokedCommandFactory)
        {
            _gameInputActions = gameInputActions;
            _jumpInputInvokedCommandFactory = jumpInputInvokedCommandFactory;
            _shootInputInvokedCommandFactory = shootInputInvokedCommandFactory;
        }

        public void EnableInputs()
        {
            _gameInputActions.Enable();
            _gameInputActions.MainGame.Jump.started += OnJumpInput;
            _gameInputActions.MainGame.Shoot.performed += OnShootInput;
        }

        public void DisableInputs()
        {
            _gameInputActions.MainGame.Jump.started -= OnJumpInput;
            _gameInputActions.MainGame.Shoot.performed -= OnShootInput;
            _gameInputActions.Disable();
        }
        
        private void OnShootInput(InputAction.CallbackContext obj)
        {
            _shootInputInvokedCommandFactory.Create().Execute();
        }

        private void OnJumpInput(InputAction.CallbackContext context)
        {
            _jumpInputInvokedCommandFactory.Create().Execute();
        }
    }
}