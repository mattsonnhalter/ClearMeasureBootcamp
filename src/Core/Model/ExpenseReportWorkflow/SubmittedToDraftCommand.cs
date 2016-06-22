using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow
{
    public class SubmittedToDraftCommand : StateCommandBase
    {
        public override string TransitionVerbPastTense => "Withdrawn";
        public override ExpenseReportStatus GetBeginStatus()
        {
            return ExpenseReportStatus.Submitted;
        }

        public override string TransitionVerbPresentTense => "Withdraw";
        protected override ExpenseReportStatus GetEndStatus()
        {
            return ExpenseReportStatus.Draft;
        }

        protected override bool userCanExecute(Employee currentUser, ExpenseReport report)
        {
            return currentUser == report.Submitter;
        }
    }
}
