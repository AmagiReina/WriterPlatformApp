namespace WriterPlatformApp.WEB.Helpers
{
    public class AlertStatus
    {
        public enum Statuses {
            DANGER_LOGIN,
            DANGER_DELETED,
            SUCCESS,
            DONE,
            WARNING
        };

        private Statuses status;

        public string GetStatusLogin()
        {
            status = Statuses.DANGER_LOGIN;
            return status.ToString();
        }

        public string GetStatusDeleted()
        {
            status = Statuses.DANGER_DELETED;
            return status.ToString();
        }

        public string GetStatusSuccess()
        {
            status = Statuses.SUCCESS;
            return status.ToString();
        }

        public string GetStatusDone()
        {
            status = Statuses.DONE;
            return status.ToString();
        }

        public string GetStatusWarning()
        {
            status = Statuses.WARNING;
            return status.ToString();
        }


    }
}