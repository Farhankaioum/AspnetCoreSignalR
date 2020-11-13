﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace OneToOneChatApplication
{
    public class LearningHub : Hub<ILearningHubClient>
    {
        [Authorize]
        public async Task BroadcastMessage(string message)
        {
            await Clients.All.ReceiveMessage(GetMessageWithName(message));
        }

        public async Task SendToCaller(string message)
        {
            await Clients.Caller.ReceiveMessage(GetMessageWithName(message));
        }

        [Authorize]
        public async Task SendMessageToSpecificUser(string userId, string message)
        {
            await Clients.User(userId).ReceiveMessage(GetMessageWithName(message));
        }

        public async Task SendToOthers(string message)
        {
            await Clients.Others.ReceiveMessage(GetMessageWithName(message));
        }

        public async Task SendToUser(string userName, string message)
        {
            await Clients.Group(userName).ReceiveMessage(GetMessageWithName(message));
        }

        public async Task SendToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).ReceiveMessage(GetMessageWithName(message));
        }

        [Authorize(Roles = "Admin")]
        public async Task AddUserToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.ReceiveMessage($"Current user added to {groupName} group");
            await Clients.Others.ReceiveMessage($"User {Context.ConnectionId} added to {groupName} group");
        }

        [Authorize(Roles = "Admin")]
        public async Task RemoveUserFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.ReceiveMessage($"Current user removed from {groupName} group");
            await Clients.Others.ReceiveMessage($"User {Context.ConnectionId} removed from {groupName} group");
        }

        public override async Task OnConnectedAsync()
        {
            if (Context?.User?.Identity?.Name != null)
                await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);

            await Groups.AddToGroupAsync(Context.ConnectionId, "HubUsers");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context?.User?.Identity?.Name != null)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "HubUsers");
            await base.OnDisconnectedAsync(exception);
        }

        private string GetMessageWithName(string message)
        {
            if (Context?.User?.Identity?.Name != null)
                return $"{Context.User.Identity.Name.Split('@')[0]} said: {message}";

            return message;
        }
    }
}
