namespace Core.Actor
{
    abstract class RootActor
    {
        public abstract void OnPreactivate();
        public abstract void OnActivate();
        public abstract void OnReady();
        public abstract void OnDestroy();
    }
}
