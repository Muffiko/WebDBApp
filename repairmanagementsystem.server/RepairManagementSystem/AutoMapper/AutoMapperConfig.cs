using AutoMapper;

public static class AutoMapperConfig
{
    public static void RegisterMappings(IMapperConfigurationExpression cfg)
    {
        cfg.AddProfile(new UserProfile());
    }
}

