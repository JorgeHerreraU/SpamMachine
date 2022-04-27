using AutoMapper;
using SpamMachine.DTOs;
using SpamMachine.Models;
using SpamMachine.ViewModels;

namespace SpamMachine.Profiles;

public class MessagesProfile : Profile
{
    public MessagesProfile()
    {
        CreateMap<MessageViewModel, Message>();
        CreateMap<Message, MessageViewModel>();
        CreateMap<Message, MessageQueueReadDTO>()
            .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.Schedule.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsSent ? "Sent" : "Pending"));
        CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages.Select(m => m.Subject).ToArray()));
        CreateMap<UserViewModel, User>()
            .ForMember(dest => dest.Messages, opt => opt.Ignore());
    }
}