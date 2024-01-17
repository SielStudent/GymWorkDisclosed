using Microsoft.AspNetCore.SignalR;

namespace GymWorkDisclosed;

public class PersonalTrainerMessageHub: Hub
{
    private readonly SignalService _signalrService;

    public PersonalTrainerMessageHub(SignalService signalrService)
    {
        _signalrService = signalrService;
    }


    public async Task SendMessage(string message)
    {
        await _signalrService.SendMessageToPersonalTrainer(message);
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("You are connected");
    }
}