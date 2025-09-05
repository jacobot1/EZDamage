using BepInEx;
using BepInEx.Logging;
using EZDamage.Data;
using EZDamage.Controllers;
using HarmonyLib;
using LethalNetworkAPI;

namespace EZDamage
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency("LethalNetworkAPI")]
    public class EZDamage : BaseUnityPlugin
    {
        public const string GUID = "com.jacobot5.EZDamage";
        public const string NAME = "EZDamage";
        public const string VERSION = "1.0.0";

        private readonly Harmony harmony = new Harmony(GUID);
        internal static ManualLogSource Log;

        // Network messages for syncing player damage
        internal static LNetworkMessage<PlayerDamageData> PlayerDamageMessage { get; private set; }

        private void Awake()
        {
            Log = BepInEx.Logging.Logger.CreateLogSource(GUID);
            Log.LogInfo($"{NAME} has awoken.");


            // --- Networking Setup ---
            PlayerDamageMessage = LNetworkMessage<PlayerDamageData>.Connect("EZDamage_Strike");
            PlayerDamageMessage.OnServerReceived += OnDamageServer;
            PlayerDamageMessage.OnClientReceived += OnDamageClient;
        }

        // --- Network Handlers ---

        // When the server receives a request for damage, it broadcasts it to all clients
        private void OnDamageServer(PlayerDamageData data, ulong sender)
        {
            PlayerDamageMessage.SendClients(data);
        }

        // When a client receives the broadcast, it applies damage
        private void OnDamageClient(PlayerDamageData data)
        {
            DamageController.DamageOrKillPlayersInRadius(
                data.center.ToVector3(),
                data.radius,
                data.damage,
                data.causeOfDeath
            );
        }
    }
}

