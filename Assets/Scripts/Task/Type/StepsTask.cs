    namespace DefaultNamespace.Type
{
    public class StepsTask : TaskObject
    {
        public bool canInteract;    //set to true for first step
        public bool noticeNext = false;
        public StepsTask nextTask;

        public override void TryNotice()
        {
            if (!canInteract) return;
            
            base.TryNotice();
        }

        public override void Interact()
        {
            Complete();
            
            //enable next step
            canInteract = false;
            if (nextTask is not null)
            {
                nextTask.canInteract = true;
                if (noticeNext)
                    nextTask.Notice();
            }
        }

        public override bool CanInteractWith()
        {
            return base.CanInteractWith() && canInteract;
        }
    }
}