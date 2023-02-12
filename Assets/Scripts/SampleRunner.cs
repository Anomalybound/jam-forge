using JamForge;
using JamForge.Events;
using UnityEngine;

public class SampleRunner : MonoBehaviour
{
    private void Start()
    {
        Jam.Events.Register(this);
        Jam.Events.Register<Payloads>(OnClickEvent2);

        OnClick();
    }

    private void OnDestroy()
    {
        Jam.Events.Unregister(this);
        Jam.Events.Unregister<Payloads>(OnClickEvent2);
    }

    [ContextMenu("Click")]
    public void OnClick()
    {
        Jam.Events.Fire(new Payloads());
    }

    [Subscribe]
    private void OnClickEvent(Payloads payloads)
    {
        Debug.Log("Event received!");
    }

    public void OnClickEvent2(Payloads payloads)
    {
        Debug.Log("Event handler 2!");
    }
}
