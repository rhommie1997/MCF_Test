using AutoMapper;
using MCF_TestAPI.Models;
using MCF_TestAPI.ViewModels;
using System;

namespace MCF_TestAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapConfig = new MapperConfiguration(x => {

                x.CreateMap<BpkbViewModel, tr_bpkb>();
                x.CreateMap<tr_bpkb, BpkbViewModel>();

                x.CreateMap<LocationViewModel, ms_storage_location>();
                x.CreateMap<ms_storage_location, LocationViewModel>();

            });

            return mapConfig;
        }
    }
}
