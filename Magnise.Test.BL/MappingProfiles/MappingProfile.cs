
using AutoMapper;
using Magnise.Test.BL.DTO;
using Magnise.Test.DAL.Entities;

namespace Magnise.Test.BL.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<CryptocurrencyDTO, Cryptocurrency>().ReverseMap();

            CreateMap<CryptocurrencyUpdateDTO, Cryptocurrency>()
                .ForMember(dest => dest.AssetID, opt => opt.MapFrom(src => GetAssetID(src.SymbolID)))
                .ForMember(dest => dest.PriceInUSD, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.TimeCoinapi));
        }

        private string GetAssetID(string symbol)
        {
            var parts = symbol.Split('_');
            return parts.Length >= 3 ? parts[^2] : string.Empty;
        }
    }
}
