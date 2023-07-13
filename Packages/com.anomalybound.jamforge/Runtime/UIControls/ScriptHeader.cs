using UnityEngine.UIElements;

namespace JamForge.UIControls
{
    public class ScriptHeader : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ScriptHeader, UxmlTraits> { }

        public new class UxmlTraits : BindableElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _label = new() { name = "label" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var header = (ScriptHeader)ve;
                header.Label = _label.GetValueFromBag(bag, cc);
                header.LabelField.text = header.Label;

                header.AddToClassList("script-header");
                header.AddToClassList("section");
            }
        }

        public string Label { get; set; }

        public Label LabelField { get; set; }

        public ScriptHeader()
        {
            LabelField = new Label();
            Add(LabelField);
        }

        public ScriptHeader(string label)
        {
            Label = label;
            LabelField = new Label(Label);
            Add(LabelField);
        }
    }
}
