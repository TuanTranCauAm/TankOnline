using System;
using Photon.Deterministic;
using Quantum;
using UnityEngine;

public class LocalInput : MonoBehaviour {
    private void Start() {
      QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
    }

    public void PollInput(CallbackPollInput callback) {
        Quantum.Input i = new Quantum.Input(); 
        i.Fire = UnityEngine.Input.GetMouseButton(0);

        var x = UnityEngine.Input.GetAxis("Horizontal");
        var y = UnityEngine.Input.GetAxis("Vertical");
        i.Direction = new Vector2(x,y).ToFPVector2();
        if(UnityEngine.Input.GetMouseButton(0))
            i.Fire = UnityEngine.Input.GetMouseButton(0);
        //Vector3 mousePos = UnityEngine.Input.mousePosition;
        //i.mousePos = Camera.main.ScreenToWorldPoint(mousePos).ToFPVector3();
        //Debug.Log("Mouse: " + mousePos + "\nMouse Change: " + UnityEngine.Camera.main.ScreenToWorldPoint(mousePos));
        Vector3 screenPosition = UnityEngine.Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 130));
        //Debug.Log("Mouse: " + screenPosition + "\nMouse Change: " + worldPosition);
        i.mousePos = worldPosition.ToFPVector3();
        callback.SetInput(i, DeterministicInputFlags.Repeatable);
    }
}
