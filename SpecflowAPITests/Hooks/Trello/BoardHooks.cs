using SpecflowAPITests.Client;
using SpecflowAPITests.Helpers;
using TechTalk.SpecFlow;

namespace SpecflowAPITests.Hooks.Trello
{
    [Binding]
    public sealed class BoardHooks
    {
        private Helper _helper;
        public BoardHooks(Helper helper)
        {
            _helper = helper;
        }

        
        [AfterScenario(Order = 101)]
        [Scope(Tag = "deleteTrelloBoard")]
        public void AfterScenario()
        {
            foreach (var id in _helper.GetIds())
            {
                var request = new TrelloRequest("boards/" + id);

                RequestManager.Delete(TrelloClient.GetInstance(), request);
            }
        }
    }
}
