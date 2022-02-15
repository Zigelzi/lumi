// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""e9520407-38e6-484c-935f-c6a101b60120"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""16802953-8694-4315-b652-17ce79e6f16d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""CastPrimarySpell"",
                    ""type"": ""Button"",
                    ""id"": ""be7704a7-8150-4b95-850c-1465c83187fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Deflect"",
                    ""type"": ""Button"",
                    ""id"": ""37ab2cbf-46bd-483e-8c5f-8e6343a9b886"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CastSecondarySpell"",
                    ""type"": ""Button"",
                    ""id"": ""0b0cb02f-4f58-44bc-9015-da9e191c80c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CastThirdSpell"",
                    ""type"": ""Button"",
                    ""id"": ""0694cc9d-1e0b-4c84-aaee-0a5b0f14b83a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""097aee83-80b6-42f1-a41a-0a65e0e1bdce"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c61066fe-ca21-4f06-a0cd-0aa6cbb686d1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""95ed4cc5-6368-4733-aa4e-2a3a9800693c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e61c8ff6-1dc8-441d-aa47-c41caa98f676"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6b4bdba9-65ee-45a5-8772-664b734dc06d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""39668839-c401-4e83-91cc-fb47a92dc4f5"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2544a189-6e57-4a90-b1c3-39154c3f0691"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CastPrimarySpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61243852-fd09-44a9-a15e-0ee904f3274d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CastPrimarySpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8b4885e-c68a-449c-b69e-08a6673201e4"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CastSecondarySpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c680fc1-86e8-4724-a32f-5e0ebb38284d"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CastSecondarySpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2910c37-d52e-4518-a540-ca3bc8b8a6f3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CastThirdSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b7b6297-8ae6-4684-b0d8-8385ee529769"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CastThirdSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6233ad7a-2348-4b5e-b217-05af2970cb22"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Deflect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8825cdbc-64d9-4683-85c3-5fa77e62800c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Deflect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_CastPrimarySpell = m_Player.FindAction("CastPrimarySpell", throwIfNotFound: true);
        m_Player_Deflect = m_Player.FindAction("Deflect", throwIfNotFound: true);
        m_Player_CastSecondarySpell = m_Player.FindAction("CastSecondarySpell", throwIfNotFound: true);
        m_Player_CastThirdSpell = m_Player.FindAction("CastThirdSpell", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_CastPrimarySpell;
    private readonly InputAction m_Player_Deflect;
    private readonly InputAction m_Player_CastSecondarySpell;
    private readonly InputAction m_Player_CastThirdSpell;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @CastPrimarySpell => m_Wrapper.m_Player_CastPrimarySpell;
        public InputAction @Deflect => m_Wrapper.m_Player_Deflect;
        public InputAction @CastSecondarySpell => m_Wrapper.m_Player_CastSecondarySpell;
        public InputAction @CastThirdSpell => m_Wrapper.m_Player_CastThirdSpell;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @CastPrimarySpell.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastPrimarySpell;
                @CastPrimarySpell.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastPrimarySpell;
                @CastPrimarySpell.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastPrimarySpell;
                @Deflect.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeflect;
                @Deflect.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeflect;
                @Deflect.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeflect;
                @CastSecondarySpell.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastSecondarySpell;
                @CastSecondarySpell.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastSecondarySpell;
                @CastSecondarySpell.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastSecondarySpell;
                @CastThirdSpell.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastThirdSpell;
                @CastThirdSpell.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastThirdSpell;
                @CastThirdSpell.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastThirdSpell;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @CastPrimarySpell.started += instance.OnCastPrimarySpell;
                @CastPrimarySpell.performed += instance.OnCastPrimarySpell;
                @CastPrimarySpell.canceled += instance.OnCastPrimarySpell;
                @Deflect.started += instance.OnDeflect;
                @Deflect.performed += instance.OnDeflect;
                @Deflect.canceled += instance.OnDeflect;
                @CastSecondarySpell.started += instance.OnCastSecondarySpell;
                @CastSecondarySpell.performed += instance.OnCastSecondarySpell;
                @CastSecondarySpell.canceled += instance.OnCastSecondarySpell;
                @CastThirdSpell.started += instance.OnCastThirdSpell;
                @CastThirdSpell.performed += instance.OnCastThirdSpell;
                @CastThirdSpell.canceled += instance.OnCastThirdSpell;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCastPrimarySpell(InputAction.CallbackContext context);
        void OnDeflect(InputAction.CallbackContext context);
        void OnCastSecondarySpell(InputAction.CallbackContext context);
        void OnCastThirdSpell(InputAction.CallbackContext context);
    }
}
