using UnityEngine;
using Quantum;

public class CustomCallbacks : QuantumCallbacks 
{

    public RuntimePlayer PlayerData1;
    //public RuntimePlayer PlayerData2;
    public override void OnGameStart(Quantum.QuantumGame game) {
    // paused on Start means waiting for Snapshot
    if (game.Session.IsPaused) return;

    foreach (var lp in game.GetLocalPlayers()) {
        Debug.Log("CustomCallbacks - sending player: " + lp);
        game.SendPlayerData(lp, PlayerData1);
            //game.SendPlayerData(lp, PlayerData2);
        }
  }

  public override void OnGameResync(Quantum.QuantumGame game)
  {
    Debug.Log("Detected Resync. Verified tick: " + game.Frames.Verified.Number);
  }
}

