namespace IsIoTWeb.Models.Valve
{
    public class ValveAction
    {
        public ValveAction(int valveId, string action, string userId)
        {
            ValveId = valveId;
            Action = action;
            UserId = userId;
        }

        public int ValveId { get; set; }

        public string Action { get; set; }

        public string UserId { get; set; }
    }
}
