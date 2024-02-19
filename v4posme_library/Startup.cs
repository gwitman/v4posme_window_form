using Microsoft.Extensions.Configuration;
namespace v4posme_library;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}
