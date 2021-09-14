using AutoMapper;
using SwaggerTraining.DTOs.V2;
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

            CreateMap<Developer, DeveloperDtoV2>()
                .ForMember(destination =>
                        destination.FullName,
                    map => map.MapFrom(source =>
                        $"{source.FirstName} {source.LastName}"))
                .ForMember(destination
                    => destination.Age, map => map.MapFrom(source =>
                    source.DateOfBirth.CalculateAge()))
                .ForMember(destination
                => destination.BirthDate, map => map.MapFrom(source =>
                source.DateOfBirth.ToShortDateString()));

            CreateMap<NewDeveloperDto, Developer>();
        }
    }
}
