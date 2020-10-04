using SpecFlowTrelloTests.Helpers;
using SpecFlowTrelloTests.Client;
using TechTalk.SpecFlow;

namespace SpecFlowTrelloTests.Hooks
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
        [Scope(Tag = "deleteTrelloBoard")]
        public void DeleteBoard()
        {
            foreach (string id in _helper.getIds())
            {
                var request = new TrelloRequest("boards/"+id);
                RequestManager.Delete(TrelloClient.GetInstance(), request);
            }
        }
    }
}
