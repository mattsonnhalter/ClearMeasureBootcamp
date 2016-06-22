using System.Configuration;
using ClearMeasure.Bootcamp.Core.Features.Workflow;

namespace ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow
{
    public class ApprovedToWithdrawnCommand : StateCommandBase
    {
        public ApprovedToWithdrawnCommand()
            : base()
        {
        }

        public override string TransitionVerbPresentTense
        {
            get { return "Withdraw"; }
        }

        public override string TransitionVerbPastTense
        {
            get { return "Withdrawn"; }
        }

        public override ExpenseReportStatus GetBeginStatus()
        {
            return ExpenseReportStatus.Approved;
        }

        protected override ExpenseReportStatus GetEndStatus()
        {
            return ExpenseReportStatus.Draft;
        }

        protected override bool userCanExecute(Employee currentUser, ExpenseReport report)
        {
            if (report.Approver == null) return false;
            return report.Approver.CanActOnBehalf(currentUser);
        }

        protected override void preExecute(ExecuteTransitionCommand transitionCommand)
        {
            transitionCommand.Report.LastApproved = transitionCommand.CurrentDate;
        }
    }
}