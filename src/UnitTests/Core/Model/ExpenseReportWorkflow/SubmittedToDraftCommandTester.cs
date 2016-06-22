using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;
using NUnit.Framework;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Model.ExpenseReportWorkflow
{
    public class SubmittedToDraftCommandTester : StateCommandBaseTester
    {
        protected override StateCommandBase GetStateCommand(ExpenseReport order, Employee employee)
        {
            return new SubmittedToDraftCommand();
        }

        [Test]

        public void ShouldBeValid()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Submitted;

            var employee = new Employee();
            report.Submitter = employee;

            var command = new SubmittedToDraftCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.True);

        }

        [Test]
        public void ShouldNotBeValidInWrongStatus()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Approver = employee;

            var command = new SubmittedToDraftCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.False);
        }

        
    }
}
