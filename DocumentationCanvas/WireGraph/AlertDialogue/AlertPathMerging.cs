using Eto.Forms;
using WireEventImplementor;

namespace DocumentationCanvas.WireGraph.AlertDialogue
{
    internal class AlertPathMerging : AlertBase
    {
        public AlertPathMerging(WireStatus wireStatus, string message) : base(wireStatus)
        {
            DynamicLayout layout = Content as DynamicLayout;

            layout.Rows.Insert(0, new DynamicRow(new Label { Text = message }));
        }
    }
}
