namespace FeatureBee.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;

    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    using Machine.Fakes;
    using Machine.Specifications;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    //public class EditPanelHubSpecs : WithSubject<EditPanelHubSpecs.TestableEditPanelHub>
    //{
    //    public class TestableEditPanelHub : EditPanelHub
    //    {
    //        public TestableEditPanelHub(IDomainRepository mockChatRepository)
    //            : base(mockChatRepository)
    //        {
    //            const string connectionId = "1234";
    //            const string hubName = "Chat";
    //            var mockConnection = An<IConnection>();
    //            var mockUser = An<IPrincipal>();
    //            var mockCookies = An<IDictionary<string, Cookie>>();

    //            var mockRequest = An<IRequest>();
    //            mockRequest.WhenToldTo(r => r.User).Return(mockUser);
    //            mockRequest.WhenToldTo(r => r.Cookies).Return(mockCookies);

    //            this.Clients = new HubConnectionContext(An<IHubPipelineInvoker>(), mockConnection, hubName, connectionId, An<StateChangeTracker>());
    //            this.Context = new HubCallerContext(mockRequest, connectionId);
    //        }
    //    }

    //    Establish context = () => feature = new FeatureAggregate { name = "item", index = 0 };

    //    public class When_editing_a_item
    //    {
    //        Because of = () => Subject.EditItem("item", feature);

    //        It should_have_saved_the_feature_to_the_repository =
    //            () =>
    //                The<IDomainRepository>()
    //                    .WasToldTo(_ => _.Save(Param<string>.IsAnything, Param<FeatureAggregate>.IsAnything));

    //        // todo: find a way to test this...
    //        It should_have_dispatched_the_event;
    //    }

    //    public class When_adding_a_condition
    //    {
    //        Establish context = () => The<IDomainRepository>()
    //               .WhenToldTo(_ => _.Collection())
    //               .Return(new List<FeatureAggregate> { feature }.AsQueryable());

    //        Because of = () => Subject.AddConditionValue("item", "cond", new []{ "a" });

    //        It should_not_have_saved_the_feature_to_the_repository =
    //            () =>
    //                The<IDomainRepository>()
    //                    .WasNotToldTo(_ => _.Save(Param<string>.IsAnything, Param<FeatureAggregate>.IsAnything));

    //        It should_have_a_condition_to_the_feature =
    //            () => feature.conditions.ShouldContain(_ => _.type == "cond");

    //        It should_have_added_the_condition_to_the_feature =
    //            () => feature.conditions.ShouldContain(_ => _.values.Any(v => v == "a"));

    //        // todo: find a way to test this...
    //        It should_have_dispatched_the_event;
    //    }

    //    public class When_removing_a_condition_value
    //    {
    //        Establish context = () =>
    //        {
    //            feature.conditions.Add(new Condition { type = "cond", values = new List<string>() { "a", "b" } });
    //            The<IDomainRepository>()
    //                .WhenToldTo(_ => _.Collection())
    //                .Return(new List<FeatureAggregate> { feature }.AsQueryable());
    //        };

    //        Because of = () => Subject.RemoveConditionValue("item", "cond", new[] { "a" });

    //        It should_not_have_saved_the_feature_to_the_repository =
    //            () =>
    //                The<IDomainRepository>()
    //                    .WasNotToldTo(_ => _.Save(Param<string>.IsAnything, Param<FeatureAggregate>.IsAnything));

    //        It should_have_removed_the_condition_from_the_feature = () => feature.conditions.ShouldNotContain(_ => _.values.Any(v => v == "a"));

    //        It should_not_have_removed_the_other_conditions_from_the_feature =
    //            () => feature.conditions.ShouldContain(_ => _.values.Any(v => v == "b"));

    //        // todo: find a way to test this...
    //        It should_have_dispatched_the_event;
    //    }

    //    public class When_removing_all_condition_values
    //    {
    //        Establish context = () =>
    //        {
    //            feature.conditions.Add(new Condition { type = "cond", values = new List<string>() { "a" } });
    //            The<IDomainRepository>()
    //                .WhenToldTo(_ => _.Collection())
    //                .Return(new List<FeatureAggregate> { feature }.AsQueryable());
    //        };

    //        Because of = () => Subject.RemoveConditionValue("item", "cond", new[] { "a" });

    //        It should_not_have_saved_the_feature_to_the_repository =
    //            () =>
    //                The<IDomainRepository>()
    //                    .WasNotToldTo(_ => _.Save(Param<string>.IsAnything, Param<FeatureAggregate>.IsAnything));

    //        It should_not_have_the_condition =
    //            () => feature.conditions.ShouldBeEmpty();

    //        // todo: find a way to test this...
    //        It should_have_dispatched_the_event;
    //    }

    //    public class When_creating_a_new_condition
    //    {
    //        Establish context = () => The<IDomainRepository>()
    //            .WhenToldTo(_ => _.Collection())
    //            .Return(new List<FeatureAggregate> { feature }.AsQueryable());

    //        Because of = () => Subject.CreateCondition("item", "cond");

    //        It should_not_have_saved_the_feature_to_the_repository =
    //            () =>
    //                The<IDomainRepository>()
    //                    .WasNotToldTo(_ => _.Save(Param<string>.IsAnything, Param<FeatureAggregate>.IsAnything));

    //        It should_contain_a_condition = () => feature.conditions.Count.ShouldEqual(1);

    //        It should_have_added_the_condition =
    //            () => feature.conditions.ShouldContain(_ => _.type == "cond");

    //        // todo: find a way to test this...
    //        It should_have_dispatched_the_event;
    //    }

    //    private static FeatureAggregate feature;
    //}
}