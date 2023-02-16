using JamForge;
using JamForge.Events;
using UnityEngine;

public class EventExample : MonoBehaviour
{
    public class TestPayload : Payloads { }

    private void Start()
    {
        Jam.Events.Register(this);
        Jam.Events.Register<Payloads>(OnClickEvent2);
        Jam.Events.Register<Payloads>(EventBroker.DefaultPath, OnClickEvent3);

        OnClick();
    }

    private void OnDestroy()
    {
        Jam.Events.Unregister(this);
        Jam.Events.Unregister<Payloads>(OnClickEvent2);
        Jam.Events.Unregister<Payloads>(EventBroker.DefaultPath, OnClickEvent3);
    }

    [ContextMenu("Click")]
    public void OnClick()
    {
        Jam.Events.Fire(new Payloads());
    }

    [ContextMenu("Click2")]
    public void OnClick2()
    {
        Jam.Events.Fire(new TestPayload());
    }

    [Subscribe]
    private void OnClickEvent(Payloads payloads)
    {
        Jam.Logger.Debug("Event received!");
    }

    private void OnClickEvent2(Payloads payloads)
    {
        Jam.Logger.Debug("Event handler 2!");
    }

    private void OnClickEvent3(Payloads payloads)
    {
        Jam.Logger.Debug("Event handler 3!");
    }
}
