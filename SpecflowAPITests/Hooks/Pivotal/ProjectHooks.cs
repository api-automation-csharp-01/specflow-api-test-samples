using SpecflowAPITests.Client;
using SpecflowAPITests.Helpers;
using TechTalk.SpecFlow;

namespace SpecflowAPITests.Hooks.Pivotal
{
    [Binding]
    public class ProjectHooks
    {
        private Helper _helper;
        public ProjectHooks(Helper helper)
        {
            _helper = helper;
        }

        [AfterScenario(Order = 100)]
        [Scope(Tag = "deletePivotalProject")]
        public void DeleteProject()
        {
            foreach (string id in _helper.GetIds())
            {
                var request = new PivotalRequest("projects/" + id);
                RequestManager.Delete(PivotalClient.GetInstance(), request);
            }
        }
    }
}
