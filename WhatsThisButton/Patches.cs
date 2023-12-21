using HarmonyLib;
using GameNetcodeStuff;
using UnityEngine.InputSystem;

namespace WhatsThisButton
{
    internal class Patches
    {
        public static Keybinds keybinds;

        public static bool devMode = false;

        private static PlayerControllerB localPlayerController
        {
            get
            {
                StartOfRound instance = StartOfRound.Instance;
                return (instance != null) ? instance.localPlayerController : null;
            }
        }

        [HarmonyPatch(typeof(PlayerControllerB), "Start")]
        [HarmonyPostfix]
        private static void StartPostfix(PlayerControllerB __instance)
        {
            keybinds.Value.started += onValueStarted;
            keybinds.Value.performed += onValuePerformed;
            keybinds.Value.canceled += onValueCanceled;
            keybinds.Button.started += onButtonStarted;
            keybinds.Button.performed += onButtonPerformed;
            keybinds.Button.canceled += onButtonCanceled;
        }

        [HarmonyPatch(typeof(PlayerControllerB), "Update")]
        [HarmonyPrefix]
        private static void UpdatePrefix(PlayerControllerB __instance)
        {
            GameTips.UpdateInternal();
        }

        [HarmonyPatch(typeof(PlayerControllerB), "OnDisable")]
        [HarmonyPostfix]
        public static void OnDisablePostfix(PlayerControllerB __instance)
        {
            if (__instance == localPlayerController)
            {
                keybinds.Value.started -= onValueStarted;
                keybinds.Value.performed -= onValuePerformed;
                keybinds.Value.canceled -= onValueCanceled;
                keybinds.Button.started -= onButtonStarted;
                keybinds.Button.performed -= onButtonPerformed;
                keybinds.Button.canceled -= onButtonCanceled;
                keybinds.Value.Disable();
            }
        }

        private static void onValueStarted(InputAction.CallbackContext context)
        {
            logInfo(context, true, "onValueStarted");
        }

        private static void onValuePerformed(InputAction.CallbackContext context)
        {
            logInfo(context, true, "onValuePerformed");
        }

        private static void onValueCanceled(InputAction.CallbackContext context)
        {
            logInfo(context, true, "onValueCanceled");
        }

        private static void onButtonStarted(InputAction.CallbackContext context)
        {
            logInfo(context, false, "onButtonStarted");
        }

        private static void onButtonPerformed(InputAction.CallbackContext context)
        {
            logInfo(context, false, "onButtonPerformed");
        }

        private static void onButtonCanceled(InputAction.CallbackContext context)
        {
            logInfo(context, false, "onButtonCancelled");
        }

        private static void logInfo(InputAction.CallbackContext context, bool isValue, string actionName)
        {
            Plugin.StaticLogger.LogInfo($"{actionName} -----");
            Plugin.StaticLogger.LogInfo($"context.ReadValueAsObject(): {context.ReadValueAsObject()}");
            if (!isValue)
            {
                Plugin.StaticLogger.LogInfo($"context.ReadValueAsButton(): {context.ReadValueAsButton()}");
            }
            string message = "";
            foreach (InputBinding binding in context.action.bindings)
            {
                message += $"{binding.effectivePath}\n";
                Plugin.StaticLogger.LogInfo($"context.action.bindings[{binding.name}].effectivePath: {binding.effectivePath}");
            }
            GameTips.ShowTip(context.action.name, message);
        }
    }
}
