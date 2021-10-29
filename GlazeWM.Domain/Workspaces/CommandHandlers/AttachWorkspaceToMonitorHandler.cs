﻿using GlazeWM.Domain.Containers.Commands;
using GlazeWM.Domain.Workspaces.Commands;
using GlazeWM.Domain.Workspaces.Events;
using GlazeWM.Infrastructure.Bussing;

namespace GlazeWM.Domain.Workspaces.CommandHandlers
{
  class AttachWorkspaceToMonitorHandler : ICommandHandler<AttachWorkspaceToMonitorCommand>
  {
    private WorkspaceService _workspaceService;
    public Bus _bus { get; }

    public AttachWorkspaceToMonitorHandler(WorkspaceService workspaceService, Bus bus)
    {
      _workspaceService = workspaceService;
      _bus = bus;
    }

    public CommandResponse Handle(AttachWorkspaceToMonitorCommand command)
    {
      var monitor = command.Monitor;
      var workspace = command.Workspace;

      _bus.Invoke(new AttachContainerCommand(monitor, workspace));
      _workspaceService.InactiveWorkspaces.Remove(command.Workspace);

      _bus.RaiseEvent(new WorkspaceAttachedEvent(command.Workspace));

      return CommandResponse.Ok;
    }
  }
}
