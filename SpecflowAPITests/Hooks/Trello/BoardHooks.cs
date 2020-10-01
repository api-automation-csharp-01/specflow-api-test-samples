using SpecflowAPITests.Client;
using SpecflowAPITests.Helpers;
using TechTalk.SpecFlow;

namespace SpecflowAPITests.Hooks.Pivotal
{
    [Binding]
    public class BoardHooks
    {
        private Helper _helper;
        public BoardHooks(Helper helper)
        {
            _helper = helper;
        }

        [AfterScenario(Order = 100)]
        [Scope(Tag = "deleteTrelloBoard")]
        public void DeleteBoard()
        {
            foreach (string id in _helper.GetIds())
            {
                var request = new TrelloRequest("boards/" + id);
                RequestManager.Delete(TrelloClient.GetInstance(), request);
            }
        }
    }
}
