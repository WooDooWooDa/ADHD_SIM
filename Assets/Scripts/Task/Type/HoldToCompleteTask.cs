namespace DefaultNamespace.Type
{
    public class HoldToCompleteTask : TaskObject
    {
        public override void Interact()
        {
            //basic task just completes it when interacted
            Complete();
        }
    }
}