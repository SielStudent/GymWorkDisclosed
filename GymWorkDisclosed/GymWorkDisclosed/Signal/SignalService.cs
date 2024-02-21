using Microsoft.AspNetCore.SignalR;

namespace GymWorkDisclosed;

public class SignalService
{
    private readonly IHubContext<PersonalTrainerMessageHub> _hubContext;

    public SignalService(IHubContext<PersonalTrainerMessageHub> hubContext)
    {
        _hubContext = hubContext;
    }
    public async Task SendMessageToPersonalTrainer(string message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
    }
}