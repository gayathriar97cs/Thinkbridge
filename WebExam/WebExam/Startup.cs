using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebExam.Startup))]
namespace WebExam
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
