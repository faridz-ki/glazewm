using LarsWM.Domain.Common.Services;
using LarsWM.Domain.Monitors;
using LarsWM.Domain.Monitors.Commands;
using LarsWM.Domain.UserConfigs.Commands;
using LarsWM.Infrastructure.Bussing;
using System.Windows.Forms;

namespace LarsWM.Bootstrapper
{
    class Startup
    {
        private IBus _bus;
        private MonitorService _monitorService;

        private KeybindingService _keybindingService;

        public Startup(IBus bus, MonitorService monitorService, KeybindingService keybindingService)
        {
            _bus = bus;
            _monitorService = monitorService;
            _keybindingService = keybindingService;
        }

        public void Init()
        {
            _keybindingService.Init();

            // Populate initial monitors, windows, workspaces and user config.
            PopulateInitialState();

            // Subscribe to windows hooks

            // Create a command for forcing the initial layout
        }

        /// <summary>
        /// Populate initial monitors, windows, workspaces and user config.
        /// </summary>
        private void PopulateInitialState()
        {
            // Read user config file and set its values in state.
            _bus.Invoke(new ReadUserConfigCommand());

            // Create a Monitor and consequently a Workspace for each detected Screen.
            foreach (var screen in Screen.AllScreens)
                _bus.Invoke(new AddMonitorCommand(screen));

            // TODO: move the below code to its own command
            //var windows = GetOpenWindows();

            //foreach (var window in windows)
            //{
            //    // Add window to its nearest workspace
            //    var targetMonitor = _monitorService.GetMonitorFromWindowHandle(window);
            //    targetMonitor.DisplayedWorkspace.WindowsInWorkspace.Add(window);
            //}
        }
    }
}

