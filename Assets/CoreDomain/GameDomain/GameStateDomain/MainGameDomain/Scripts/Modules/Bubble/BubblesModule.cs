using UnityEngine;

public class BubblesModule : IBubblesModule
{
    private readonly PopBubbleCommand.Factory _popBubbleCommand;
    public int BubblesPopScore => _bubblesData.PopScore;

    private readonly BubblesCreator _bubblesCreator;
    private BubblesData _bubblesData;

    public BubblesModule(IResourcesLoaderService resourcesLoaderService, PopBubbleCommand.Factory popBubbleCommand)
    {
        _popBubbleCommand = popBubbleCommand;
        _bubblesCreator = new BubblesCreator(resourcesLoaderService);
    }

    public void LoadData()
    {
        _bubblesData = _bubblesCreator.LoadBubblesData();
    }

    public void SetupBubbles()
    {
        var bubblesViews = GameObject.FindObjectsOfType<BubblesView>();

        for (int i = 0; i < bubblesViews.Length; i++)
        {
            bubblesViews[i].Setup(OnBubblePopTriggered);
        }
    }
    
    private void OnBubblePopTriggered(Vector3 position)
    {
        _popBubbleCommand.Create(new PopBubbleCommandData(position)).Execute();
    }
}
