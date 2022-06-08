// GENERATED AUTOMATICALLY FROM 'Assets/_Game/Player/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""CharacterControls1"",
            ""id"": ""d9be1231-19b2-42a9-92ea-0c684edc41b6"",
            ""actions"": [
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""3e9a7065-657f-4ea2-8a0e-b4042826fe97"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f37b473e-acf0-47e2-826c-befd3b159cda"",
                    ""path"": ""<AndroidJoystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""04ad802c-bc60-4801-acd9-2c45d81c7c40"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1dffffc2-44fc-46fa-9d24-e6e16a7ab747"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fc1726b4-a36d-4740-a209-a0eab477b1d4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b4d51aff-986a-4dba-86a2-61d128d839e8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0e1363bc-e7b3-4757-8cb0-71f60ec9a220"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterControls1
        m_CharacterControls1 = asset.FindActionMap("CharacterControls1", throwIfNotFound: true);
        m_CharacterControls1_Run = m_CharacterControls1.FindAction("Run", throwIfNotFound: true);
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

    // CharacterControls1
    private readonly InputActionMap m_CharacterControls1;
    private ICharacterControls1Actions m_CharacterControls1ActionsCallbackInterface;
    private readonly InputAction m_CharacterControls1_Run;
    public struct CharacterControls1Actions
    {
        private @PlayerInput m_Wrapper;
        public CharacterControls1Actions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Run => m_Wrapper.m_CharacterControls1_Run;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControls1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControls1Actions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControls1Actions instance)
        {
            if (m_Wrapper.m_CharacterControls1ActionsCallbackInterface != null)
            {
                @Run.started -= m_Wrapper.m_CharacterControls1ActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_CharacterControls1ActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_CharacterControls1ActionsCallbackInterface.OnRun;
            }
            m_Wrapper.m_CharacterControls1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
            }
        }
    }
    public CharacterControls1Actions @CharacterControls1 => new CharacterControls1Actions(this);
    public interface ICharacterControls1Actions
    {
        void OnRun(InputAction.CallbackContext context);
    }
}
