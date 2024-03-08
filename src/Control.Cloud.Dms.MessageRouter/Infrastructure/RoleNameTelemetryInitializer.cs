namespace Control.Cloud.Dms.MessageRouter.Infrastructure;

internal class RoleNameTelemetryInitializer : ITelemetryInitializer
{
    private readonly string _roleName;

    public RoleNameTelemetryInitializer(string roleName)
    {
        _roleName = roleName;
    }

    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry == null || telemetry.Context == null)
        {
            return;
        }

        telemetry.Context.Cloud.RoleName = _roleName;
    }
}