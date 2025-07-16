namespace DefaultNamespace.Type
{
    public class StepsTask : TaskObject
    {
        public bool canInteract;    //set to true for first step
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
            if (nextTask is not null) nextTask.canInteract = true;
            canInteract = false;
            //nextTask.Notice();            //maybe not
        }

        public override bool CanInteractWith()
        {
            return base.CanInteractWith() && canInteract;
        }
    }
}