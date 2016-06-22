using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearMeasure.Bootcamp.Core.Features.Workflow;

namespace ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow
{
    public class WithdrawToDraft : StateCommandBase
    {
        public WithdrawToDraft()
            : base()
        {
        }

        public override string TransitionVerbPresentTense
        {
            get { return "Draft"; }
        }

        public override string TransitionVerbPastTense
        {
            get { return "Draft"; }
        }

        public override ExpenseReportStatus GetBeginStatus()
        {
            return ExpenseReportStatus.Submitted;
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
