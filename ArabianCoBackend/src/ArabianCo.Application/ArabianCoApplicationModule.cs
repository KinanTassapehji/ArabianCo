using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ArabianCo.Areas.Dto;
using ArabianCo.Attributes.Dto;
using ArabianCo.Authorization;
using ArabianCo.Brands.Dto;
using ArabianCo.Categories.Dto;
using ArabianCo.Cities.Dto;
using ArabianCo.Countries.Dto;
using ArabianCo.Domain.Areas;
using ArabianCo.Domain.Attributes;
using ArabianCo.Domain.AttributeValues;
using ArabianCo.Domain.Brands;
using ArabianCo.Domain.Categories;
using ArabianCo.Domain.Cities;
using ArabianCo.Domain.Countries;
using ArabianCo.Domain.FrequentlyQuestions;
using ArabianCo.Domain.Products;
using ArabianCo.FrequentlyQuestionService.Dto;
using ArabianCo.Products.Dto;
using AutoMapper;

namespace ArabianCo
{
    [DependsOn(
    typeof(ArabianCoCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ArabianCoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ArabianCoAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ArabianCoApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                CustomDtoMapper.CreateMappings(configuration, new MultiLingualMapContext(
                    IocManager.Resolve<ISettingManager>()
                ));
            });
        }
        internal static class CustomDtoMapper
        {
            public static void CreateMappings(IMapperConfigurationExpression configuration, MultiLingualMapContext context)
            {
                #region Country
                // Country Translation Configuration
                configuration.CreateMultiLingualMap<Country, CountryTranslation, CountryDetailsDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Country, CountryTranslation, CountryDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region City
                // City Translation Configuration
                configuration.CreateMultiLingualMap<City, CityTranslation, LiteCityDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<City, CityTranslation, CityDetailsDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<City, CityTranslation, CityDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region Area
                // Region Translation Configuration
                configuration.CreateMultiLingualMap<Area, AreaTranslation, LiteAreaDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Area, AreaTranslation, AreaDetailsDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Area, AreaTranslation, AreaDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region Category
                configuration.CreateMultiLingualMap<Category, CategoryTranslation, LiteCategoryDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                configuration.CreateMultiLingualMap<Category, CategoryTranslation, CategoryDetaisDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                configuration.CreateMultiLingualMap<Category, CategoryTranslation, CategoryDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                configuration.CreateMultiLingualMap<Category, CategoryTranslation, IndexDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region Brand
                configuration.CreateMultiLingualMap<Brand, BrandTranslation, LiteBrandDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                configuration.CreateMultiLingualMap<Brand, BrandTranslation, BrandDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                configuration.CreateMultiLingualMap<Brand, BrandTranslation, IndexDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion

                #region FrequentlyQuestion
                configuration.CreateMultiLingualMap<FrequentlyQuestion, FrequentlyQuestionTranslation, FrequentlyQuestionDetailsDto>(context).TranslationMap
                 .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                 .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer));
                configuration.CreateMultiLingualMap<FrequentlyQuestion, FrequentlyQuestionTranslation, LiteFrequentlyQuestionDto>(context).TranslationMap
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                 .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer));
                #endregion

                #region Attribute
                configuration.CreateMultiLingualMap<Attribute, AttributeTranslation, LiteAttributeDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                configuration.CreateMultiLingualMap<Attribute, AttributeTranslation, IndexDto>(context).TranslationMap
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                #endregion


                #region Product
                configuration.CreateMultiLingualMap<Product, ProductTranslation, LiteProductDto>(context).TranslationMap
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                configuration.CreateMultiLingualMap<Product, ProductTranslation, ProductDto>(context).TranslationMap
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
                configuration.CreateMultiLingualMap<AttributeValue, AttributeValueTranslation, AttributValueDto>(context).TranslationMap
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

                #endregion

            }
        }
            }
        }
