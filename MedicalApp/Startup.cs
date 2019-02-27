using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicalApp.Startup))]
namespace MedicalApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
