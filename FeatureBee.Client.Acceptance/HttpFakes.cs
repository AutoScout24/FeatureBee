using System.Security.Principal;
using System.Web;
using Moq;

namespace FeatureBee.Acceptance
{
    public static class HttpFakes
    {
        public static HttpContextBase FakeHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var user = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();

            request.Expect(req => req.ApplicationPath).Returns("~/");
            request.Expect(req => req.AppRelativeCurrentExecutionFilePath).Returns("~/");
            request.Expect(req => req.PathInfo).Returns(string.Empty);
            response.Expect(res => res.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns((string virtualPath) => virtualPath);
            user.Expect(usr => usr.Identity).Returns(identity.Object);
            identity.ExpectGet(ident => ident.IsAuthenticated).Returns(true);

            context.Expect(ctx => ctx.Request).Returns(request.Object);
            context.Expect(ctx => ctx.Response).Returns(response.Object);
            context.Expect(ctx => ctx.Session).Returns(session.Object);
            context.Expect(ctx => ctx.Server).Returns(server.Object);
            context.Expect(ctx => ctx.User).Returns(user.Object);

            return context.Object;
        }        
    }
}