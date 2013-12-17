namespace FeatureBee.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;

    using FeatureBee.Server.Data.Features;
    using FeatureBee.Server.Models;

    using Machine.Fakes;
    using Machine.Specifications;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    public class BoardHubSpecs : WithSubject<BoardHubSpecs.TestableBoardHub>
    {
        public class TestableBoardHub : BoardHub
        {
            public TestableBoardHub(IFeatureRepository mockChatRepository)
                : base(mockChatRepository)
            {
                const string connectionId = "1234";
                const string hubName = "Chat";
                var mockConnection = An<IConnection>();
                var mockUser = An<IPrincipal>();
                var mockCookies = An<IDictionary<string, Cookie>>();

                var mockRequest = An<IRequest>();
                mockRequest.WhenToldTo(r => r.User).Return(mockUser);
                mockRequest.WhenToldTo(r => r.Cookies).Return(mockCookies);

                this.Clients = new HubConnectionContext(An<IHubPipelineInvoker>(), mockConnection, hubName, connectionId, An<StateChangeTracker>());
                this.Context = new HubCallerContext(mockRequest, connectionId);
            }
        }

        Establish context = () => feature = new Feature { title = "item", index = 0 };

        public class When_adding_a_new_item
        {
            Because of = () => Subject.AddNewItem(feature);

            It should_have_added_the_feature_to_the_repository =
                () =>
                    The<IFeatureRepository>()
                        .WasToldTo(_ => _.Save(Param<string>.IsAnything, Param<Feature>.IsAnything));

            // todo: find a way to test this...
            It should_have_dispatched_the_event;
        }
        
        public class When_editing_a_item
        {
            Because of = () => Subject.EditItem("item", feature);

            It should_have_saved_the_feature_to_the_repository =
                () =>
                    The<IFeatureRepository>()
                        .WasToldTo(_ => _.Save(Param<string>.IsAnything, Param<Feature>.IsAnything));

            // todo: find a way to test this...
            It should_have_dispatched_the_event;
        }

        public class When_moving_an_item
        {
            Establish context = () => The<IFeatureRepository>()
                .WhenToldTo(_ => _.Collection())
                .Return(new List<Feature> { feature }.AsQueryable());

            Because of = () => Subject.MoveItem("item", 0, 1);

            It should_have_saved_the_feature_to_the_repository =
                () =>
                    The<IFeatureRepository>()
                        .WasToldTo(_ => _.Save(Param<string>.IsAnything, Param<Feature>.IsAnything));

            It should_have_moved_the_item_to_the_new_index = () => feature.index.ShouldEqual(1);

            // todo: find a way to test this...
            private It should_have_dispatched_the_event;
        }
        
        private static Feature feature;
    }
}