using AutoMapper;
using SwaggerTraining.Helpers.Extensions;
using SwaggerTraining.Models;

namespace SwaggerTraining.DTOs.Mappings
{
    public class DeveloperMapper : Profile
    {
        public DeveloperMapper()
        {
            CreateMap<Developer, DeveloperDto>()
                .ForMember(destination =>
                    destination.FullName,
                    map => map.MapFrom(source => 
                        $"{source.FirstName} {source.LastName}"))
                .ForMember(destination
                    => destination.Age, map => map.MapFrom(source =>
                    source.DateOfBirth.CalculateAge()));

            CreateMap<NewDeveloperDto, Developer>();
        }
    }
}
