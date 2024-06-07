using NLog.LayoutRenderers;
using NLog;
using System.Text;

namespace NLogProject.Extensions.Services
{
    [LayoutRenderer("userid")]
    public class UserIdLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            if (logEvent.Properties.ContainsKey("userid"))
            {
                builder.Append(logEvent.Properties["userid"]);
            }
            else
            {
                builder.Append("UnknownUser");
            }
        }
    }
}
