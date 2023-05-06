using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyAuthServer.Service
{
    public static class ObjectMapper
    {
        // lazy => biz isdediyimiz anda yani methodu ve ya classi cagirdimiz anda yuklesin databasadan butun melumatlar.
        //proqram ayaga qalxanda bos bosuna databasaya muraciyet elemesin deye Lazy-den istifade eliyiriy
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.Internal().MethodMappigEnabled = false;
                cfg.AddProfile<DtoMapper>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}