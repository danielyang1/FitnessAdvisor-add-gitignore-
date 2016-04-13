using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FitnessAdvisor.Startup))]
namespace FitnessAdvisor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
