using AutoMapper;
using Workshop.Data.DTOs;
using Workshop.Models;

namespace Workshop.App.Core
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();

            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>();

            CreateMap<Invitation, InvitationDto>();
            CreateMap<InvitationDto, Invitation>();
        }
    }
}
