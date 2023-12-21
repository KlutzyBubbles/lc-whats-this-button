using LethalCompanyInputUtils.Api;
using UnityEngine.InputSystem;

namespace WhatsThisButton
{
    internal class Keybinds : LcInputActions
    {
        public InputAction Button => Asset["Button"];
        public InputAction Value => Asset["Value"];

        public override void CreateInputActions(in InputActionMapBuilder builder)
        {
            base.CreateInputActions(builder);
            builder.NewActionBinding()
                .WithActionId("Button")
                .WithActionType(InputActionType.Button)
                .WithKbmPath("")
                .WithGamepadPath("")
                .WithBindingName("WTFITK (Button)")
                .Finish();
            builder.NewActionBinding()
                .WithActionId("Value")
                .WithActionType(InputActionType.Value)
                .WithKbmPath("")
                .WithGamepadPath("")
                .WithBindingName("WTFITV (Value)")
                .Finish();
        }
    }
}
